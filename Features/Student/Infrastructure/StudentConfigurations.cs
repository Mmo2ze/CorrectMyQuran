using CorectMyQuran.Features.Auth.Domain.RefreshTokens;
using CorectMyQuran.Features.Student.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorectMyQuran.Features.Student.Infrastructure;

public class StudentConfigurations : IEntityTypeConfiguration<CorectMyQuran.Features.Student.Domain.Student>
{




    public void Configure(EntityTypeBuilder<Student.Domain.Student> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => StudentId.Create(x));
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        // relations 
        builder.HasMany<RefreshToken>()
            .WithOne()
            .HasForeignKey(x => x.StudentId);

        //index
        builder.HasIndex(x => x.Phone).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();    }
}