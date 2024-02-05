using ElectricStationMap.Models.EF;
using ElectricStationMap.Services.Guid;
using System.ComponentModel.DataAnnotations;

namespace ElectricStationMap.Models.EntityFramework
{
    public class RequestInfo : BaseEntity
    {
        public Guid UserId { get; set; } 

        public DateTime CreationDateTime { get; set; }

        public virtual ICollection<RequirementInfo> Requirements { get; set; }  

        public virtual ApplicationUser User { get; set; }
    }
}
