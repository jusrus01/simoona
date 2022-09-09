using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemporaryDataLayer
{
    internal static class Extensions
    {
        private const string IsDeleted = nameof(IsDeleted); // TODO: save somewhere

        internal static PropertyBuilder AddSoftDelete<T>(this EntityTypeBuilder<T> builder) where T : class
        {
            return builder.Property(typeof(bool), IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
