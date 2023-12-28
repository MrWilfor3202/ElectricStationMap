using System.ComponentModel.DataAnnotations;

namespace ElectricStationMap.Models.EntityFramework
{
    public class RequestInfo
    {
        public int Id { get; set; }

        public DateTime CreationDateTime { get; set; }

        public virtual List<RequirementInfo> Requirements { get; set; }  
    }
}
