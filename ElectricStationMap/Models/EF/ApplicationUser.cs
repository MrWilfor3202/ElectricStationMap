using ElectricStationMap.Models.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace ElectricStationMap.Models.EF
{
    public class ApplicationUser : IdentityUser<int>
    {
        public virtual ICollection<RequestInfo> Requests { get; set; }
    }
}
