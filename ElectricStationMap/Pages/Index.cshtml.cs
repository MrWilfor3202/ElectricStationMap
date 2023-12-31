using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElectricStationMap.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ElectricStationMap.Repository;
using ElectricStationMap.Services;
using ElectricStationMap.Models.EF;
using Newtonsoft.Json;
using ElectricStationMap.Models.Ajax;
using Microsoft.AspNetCore.Authorization;

namespace ElectricStationMap.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IRequestRepositoryAsync _requestRepositoryAsync;
        private readonly IRequirementRepositoryAsync _requirementRepositoryAsync;
        private readonly IIconRepositoryAsync _iconRepositoryAsync;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRazorRenderService _renderService;
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<RequestInfo> Requests { get; set; }
        
        public IEnumerable<Icon> Icons { get; set; }

        public IEnumerable<RequirementInfo> Requirements { get; set; }


        public IndexModel(ILogger<IndexModel> logger, 
            IRequestRepositoryAsync requestRepository, IUnitOfWork unitOfWork, IRazorRenderService renderService,
            IRequirementRepositoryAsync requirementRepositoryAsync, IIconRepositoryAsync iconRepositoryAsync)
        {
            _logger = logger;
            _requestRepositoryAsync = requestRepository;
            _unitOfWork = unitOfWork;
            _renderService = renderService;
            _requirementRepositoryAsync = requirementRepositoryAsync;
            _iconRepositoryAsync = iconRepositoryAsync;
        }

        public async Task<PartialViewResult> OnGetViewAllRequests()
        {
            Requests = await _requestRepositoryAsync.GetAllAsync();

            return new PartialViewResult 
            {
                ViewName = "_RequestsView",
                ViewData = new ViewDataDictionary<IEnumerable<RequestInfo>>(ViewData, Requests)
            };
        }

        public async Task<JsonResult> OnPostDeleteRequestAsync(int id) 
        {
            var request = await _requestRepositoryAsync.GetByIdAsync(id);
            await _requestRepositoryAsync.DeleteAsync(request);
            await _unitOfWork.Commit();

            Requests = await _requestRepositoryAsync.GetAllAsync();

            var html = await _renderService.ToStringAsync("_RequestsView", Requests);
            return new JsonResult(new { IsValid = true, html = html }); 
        }

        public async Task<JsonResult> OnGetCreateOrEditRequestAsync(int id = 0)
        {
            if (id == 0)
                return new JsonResult(new
                {
                    isValid = true,
                    html = await _renderService.ToStringAsync("_CreateOrEditRequest", new RequestInfo())
                });
            else
            {
                var thisRequest = await _requestRepositoryAsync.GetByIdAsync(id);
                return new JsonResult(new {isValid = true,
                    html = await _renderService.ToStringAsync("_CreateOrEditRequest", thisRequest)
                });
            }
        }

        public async Task<JsonResult> OnPostCreateOrEditRequestAsync(int id, RequestInfo requestInfo) 
        {
            if (ModelState.IsValid)
            {
                var getData = HttpContext.Request.Form["requirements"];
                var requirements = JsonConvert.
                    DeserializeObject<List<JSONRequirementsModel>>(getData);

                requestInfo.CreationDateTime = DateTime.Now;

                if (id == 0)
                    await _requestRepositoryAsync.AddAsync(requestInfo);
                else
                    await _requestRepositoryAsync.UpdateAsync(requestInfo);

                await _unitOfWork.Commit();

                foreach (var requirement in requirements)
                {
                    RequirementInfo requirementInfo = new RequirementInfo
                    {
                        Id = int.Parse(requirement.Id),
                        Description = requirement.Description,
                        Distance = int.Parse(requirement.Distance),
                        RequestInfoId = requestInfo.Id,
                        IconId = 1
                    };

                    if (requirementInfo.Id == 0)
                        await _requirementRepositoryAsync.AddAsync(requirementInfo);
                    else
                        await _requirementRepositoryAsync.UpdateAsync(requirementInfo);
                }
                
                await _unitOfWork.Commit();
                Requests = await _requestRepositoryAsync.GetAllAsync();
                var html = await _renderService.ToStringAsync("_RequestsView", Requests);
                return new JsonResult(new { isValid = true, html = html });
            }
            else 
            {
                var html = await _renderService.ToStringAsync("_CreateOrEditRequest", requestInfo);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
    }
}
