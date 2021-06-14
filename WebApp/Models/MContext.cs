using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApp.Models
{
    public partial class MContext : DbContext
    {
        public MContext()
            : base("name=MContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceProp> DeviceProps { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Properties)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("CategoryProp").MapLeftKey("C_Id").MapRightKey("P_Id"));

            modelBuilder.Entity<Device>()
                .Property(e => e.DeviceName)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.DeviceProps)
                .WithRequired(e => e.Device)
                .HasForeignKey(e => e.D_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeviceProp>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<Property>()
                .Property(e => e.Descraption)
                .IsUnicode(false);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.DeviceProps)
                .WithRequired(e => e.Property)
                .HasForeignKey(e => e.P_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
