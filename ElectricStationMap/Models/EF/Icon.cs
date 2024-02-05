using ElectricStationMap.Models.EntityFramework;
using ElectricStationMap.Services.Guid;

namespace ElectricStationMap.Models.EF
{
    public class Icon : BaseEntity
    {
        public string URL { get; set; }

        public virtual ICollection<RequirementInfo> Requirements { get; set; }

        public string BuildingType { get; set; } = String.Empty;
    }
}
