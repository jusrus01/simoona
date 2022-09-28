using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shrooms.DataLayer.DAL;
using Shrooms.DataLayer.EntityModels.Models.Committees;

namespace Shrooms.DataLayer.EntityTypeConfigurations.Committees
{
    public class CommitteeSuggestionIDEntityConfiguration : IEntityTypeConfiguration<CommitteeSuggestionID>
    {
        public void Configure(EntityTypeBuilder<CommitteeSuggestionID> builder)
        {
            builder.ToTable("CommitteeSuggestionsIDs");

            builder.ConfigureManyToMany(
                model => new { model.CommitteeId, model.CommitteeSuggestionId },
                "CommitteeSuggestionsIDs",
                model => model.CommitteeSuggestionIds,
                model => model.CommitteeSuggestionId,
                model => model.CommitteeSuggestion,
                "CommitteeSuggestions",
                "FK_dbo.CommitteeSuggestionsIDs_dbo.CommitteeSuggestions_CommitteeSuggestions_Id",
                model => model.CommitteeSuggestionIds,
                model => model.CommitteeId,
                model => model.Committee,
                "Committees",
                "FK_dbo.CommitteeSuggestionsIDs_dbo.Committees_Committees_Id");
        }
    }
}
