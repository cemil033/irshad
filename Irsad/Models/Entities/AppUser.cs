using Microsoft.AspNetCore.Identity;

namespace Irsad.Models.Entities
{
    public class AppUser:IdentityUser
    {
        
        public double Balans { get; set; }
        public virtual IEnumerable<UserProduct>? Products { get; set; }
    }
}
