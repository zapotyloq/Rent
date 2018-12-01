using Microsoft.AspNetCore.Identity;

namespace RentOfPremises.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
