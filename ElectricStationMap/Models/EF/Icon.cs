using ElectricStationMap.Models.EntityFramework;

namespace ElectricStationMap.Models.EF
{
    public class Icon
    {
        public int Id { get; set; }

        public string URL { get; set; }

        public virtual List<RequirementInfo> Requirements { get; set; }

        public string BuildingType { get; set; } = String.Empty;
    }
}
