using ElectricStationMap.Models.EF;
using ElectricStationMap.Services.Guid;

namespace ElectricStationMap.Models.EntityFramework
{
    public class RequirementInfo : BaseEntity
    {
        public int Distance { get; set; } 

        public Guid RequestInfoId { get; set; }

        public Guid IconId { get; set; }

        public string Description { get; set; } = "";

        public virtual RequestInfo RequestInfo { get; set; }

        public virtual Icon Icon { get; set; }

    }
}
