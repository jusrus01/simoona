using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shrooms.DataLayer.EntityModels.ModelsCore;

namespace Shrooms.DataLayer.DAL
{
    public class ShroomsDbContextCore : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
    }
}
