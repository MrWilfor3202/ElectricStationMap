using ElectricStationMap.Models.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricStationMap.Models.EntityFramework
{
    public class RequirementInfo
    {
        public int Id { get; set; }

        public int Distance { get; set; } 

        public int RequestInfoId { get; set; }

        public int IconId { get; set; }

        public string Description { get; set; } = "";

        public virtual RequestInfo RequestInfo { get; set; }

        public virtual Icon Icon { get; set; }

    }
}
