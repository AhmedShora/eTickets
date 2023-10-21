using Microsoft.AspNetCore.Identity;

namespace eTickets.Models
{
    public class AppUser:IdentityUser
    {
        [Display(Name="Full Name")]
        public string FullName { get; set; }

    }
}
