using Microsoft.AspNetCore.Identity;

namespace MyFruits.Areas.Identity.Data
{
    public class MyFruitsUser : IdentityUser
    {
        public bool EmailConfirmed { get; set; }
    }
}
