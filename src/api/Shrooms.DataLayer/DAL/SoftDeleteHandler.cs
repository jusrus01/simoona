using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shrooms.Contracts.DataTransferObjects;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.DataLayer.EntityModels.Models.Events;

namespace Shrooms.DataLayer.DAL
{
    // TODO: Refactor
    // TODO: Ask for feedback
    public class SoftDeleteHandler
    {
        public void Execute(IEnumerable<EntityEntry> entries, ShroomsDbContext ctx)
        {
            ApplySoftDeleteAsync(entries, ctx, sync: true).RunSynchronously();
        }

        public async Task ExecuteAsync(IEnumerable<EntityEntry> entries, ShroomsDbContext ctx)
        {
            await ApplySoftDeleteAsync(entries, ctx, sync: false);
        }

        private async Task ApplySoftDeleteAsync(
            IEnumerable<EntityEntry> entries,
            ShroomsDbContext ctx,
            bool sync)
        {
            var deletedItems = entries.Where(p => p.State == EntityState.Deleted && p.Entity is ISoftDelete)
                .ToList();

            foreach (var entry in deletedItems)
            {
                var e = entry.Entity;
                var id = string.Empty;

                if (e is IdentityUser || e is ApplicationRole)
                {
                    id = ((IdentityUser)e).Id;
                }
                else if (e is BaseModel model)
                {
                    id = model.Id.ToString();
                }
                else if (e is Event @event)
                {
                    id = @event.Id.ToString();
                }

                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentException("Id not found in SoftDelete() method", id);
                }

                var tableName = GetTableName(e.GetType(), ctx);
                var query = $"UPDATE {tableName} SET IsDeleted = 1 WHERE ID = @id";
                var param = new SqlParameter("id", id);

                if (sync)
                {
                    ctx.Database.ExecuteSqlCommand(query, param);
                }
                else
                {
                    await ctx.Database.ExecuteSqlCommandAsync(query, param);
                }

                // Marking it Unchanged prevents the hard delete - entry.State = EntityState.Unchanged;
                // So does setting it to Detached and that is what EF does when it deletes an item: http://msdn.microsoft.com/en-us/data/jj592676.aspx
                entry.State = EntityState.Detached;
            }
        }

        private string GetTableName(Type type, ShroomsDbContext context)
        {
            var typeAnnotations = context.Model.FindEntityType(type).Relational();

            return $"[{typeAnnotations.Schema}].[{typeAnnotations.TableName}]";
        }        
    }
}
