//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace TemporaryDataLayer.EntityConfigurations
//{
//    public class BaseModelEntityConfiguration : IEntityTypeConfiguration<BaseModel>
//    {
//        public void Configure(EntityTypeBuilder<BaseModel> builder)
//        {
//            builder.HasKey(model => model.Id);
//            builder.HasDiscriminator(model => model.Id);

//            builder.Property(model => model.Created)
//                .HasColumnType("datetime")
//                .HasMaxLength(69);
//        }
//    }
//}
