using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using MediBook.Data.Entities;

namespace MediBook.Data.EntityFramework.SqlServer
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Attachment>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.ToTable("Attachments");
      }
      );

      modelBuilder.Entity<City>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.ToTable("Cities");
      }
      );

      modelBuilder.Entity<Consultation>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.ToTable("Consultations");
      }
      );

      modelBuilder.Entity<Doctor>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.ToTable("Doctors");
        }
      );

      modelBuilder.Entity<DoctorSpecialization>(etb =>
        {
          etb.HasKey(e => new { e.DoctorId, e.SpecializationId });
          etb.ToTable("DoctorSpecializations");
        }
      );

      modelBuilder.Entity<EmailTemplate>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.ToTable("EmailTemplates");
      }
      );

      modelBuilder.Entity<Patient>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.ToTable("Patients");
      }
      );

      modelBuilder.Entity<Specialization>(etb =>
      {
        etb.HasKey(e => e.Id);
        etb.ToTable("Specializations");
      }
      );

      modelBuilder.Entity<Region>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.ToTable("Regions");
        }
      );

      modelBuilder.Entity<City>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.ToTable("Cities");
        }
      );

      modelBuilder.Entity<Organization>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.ToTable("Organizations");
        }
      );

      modelBuilder.Entity<Email>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Receiver).IsRequired().HasMaxLength(64);
          etb.Property(e => e.Subject).IsRequired().HasMaxLength(64);
          etb.Property(e => e.Text).IsRequired();
          etb.ToTable("Emails");
        }
      );

      modelBuilder.Entity<RestorePasswordToken>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ToTable("RestorePasswordTokens");
        }
      );
    }
  }
}