using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.DataLayer.DAL
{
    public class ShroomsDbContextCore : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
    }
}
