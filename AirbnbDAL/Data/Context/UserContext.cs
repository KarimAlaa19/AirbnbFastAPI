using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;

public class AirbnbContext : IdentityDbContext<User>
{
    public AirbnbContext(DbContextOptions<AirbnbContext> options) : base(options)
    {
    }
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<GuestReviewHost> GuestsReviewHost => Set<GuestReviewHost>();
    public DbSet<GuestReviewProperty> GuestsPropertiesReviews => Set<GuestReviewProperty>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<PropertyRule> PropertyRules => Set<PropertyRule>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<PropertyAmenity> PropertyAmenities => Set<PropertyAmenity>();
    public DbSet<Rule> Rules => Set<Rule>();
    public DbSet<Amenity> Amenities => Set<Amenity>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<City> Cities => Set<City>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //User Entity
        //modelBuilder.Entity<User>().HasKey(k => k.UserId);
        //modelBuilder.Entity<User>().Property(p => p.Email).IsRequired();
        //-------------------------------------------------------------------
        //Property Entity
        modelBuilder.Entity<Property>().HasKey(k => k.PropertyId);
        modelBuilder.Entity<Property>().HasOne(p => p.User).WithMany(u => u.Properties).
            HasForeignKey(fk => fk.HostId);

        //-------------------------------------------------------------------
        //Reservation Entity
        modelBuilder.Entity<Reservation>().HasKey(k => new { k.PropertyId, k.CheckInDate });
        modelBuilder.Entity<Reservation>().HasOne(r => r.User).WithMany(u => u.Reservations).
            HasForeignKey(fk => fk.GuestId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Reservation>().HasOne(r => r.Property).WithMany(p => p.Reservations).
            HasForeignKey(fk => fk.PropertyId).OnDelete(DeleteBehavior.NoAction);
        //-------------------------------------------------------------------
        //GuestReviewProperty
        modelBuilder.Entity<GuestReviewProperty>().HasKey(k => new { k.PropertyId, k.GuestId });
        modelBuilder.Entity<GuestReviewProperty>().HasOne(g => g.User).WithMany(u => u.Reviews).
            HasForeignKey(fk => fk.GuestId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<GuestReviewProperty>().HasOne(g => g.Property).WithMany(p => p.Reviews).
            HasForeignKey(fk => fk.PropertyId)
            .OnDelete(DeleteBehavior.NoAction);
        //-------------------------------------------------------------------
        //PropertyRule
        modelBuilder.Entity<PropertyRule>().HasKey(k => new { k.PropertyId, k.RuleId });
        modelBuilder.Entity<PropertyRule>().HasOne(p => p.Property).WithMany(p => p.PropertyRules).
            HasForeignKey(fk => fk.PropertyId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<PropertyRule>().HasOne(p => p.Rule).WithMany(p => p.PropertyRules).
            HasForeignKey(fk => fk.RuleId).OnDelete(DeleteBehavior.NoAction);
        //-------------------------------------------------------------------
        //RoomSpec
        modelBuilder.Entity<PropertyAmenity>().HasKey(k => new { k.AmenityId, k.PropertyId });
        modelBuilder.Entity<PropertyAmenity>().HasOne(rs => rs.Property).WithMany(r => r.PropertyAmenities).
            HasForeignKey(fk => fk.PropertyId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<PropertyAmenity>().HasOne(rs => rs.Spec).WithMany(r => r.PropertyAmenities).
            HasForeignKey(fk => fk.AmenityId).OnDelete(DeleteBehavior.NoAction);
        //-------------------------------------------------------------------
        //Images
        modelBuilder.Entity<Image>().HasKey(k => k.ImageId);
        modelBuilder.Entity<Image>().HasOne(i => i.Property).WithMany(r => r.Images).
            HasForeignKey(fk => fk.PropertyId);
        //-------------------------------------------------------------------
        //GuestReviewHost
        modelBuilder.Entity<GuestReviewHost>().HasKey(k => new { k.HostId, k.GuestId });
        modelBuilder.Entity<GuestReviewHost>().HasOne(g => g.Host).WithMany(h => h.HostReviews).
            HasForeignKey(fk => fk.HostId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<GuestReviewHost>().HasOne(g => g.Guest).WithMany(h => h.GuestReviews).
            HasForeignKey(fk => fk.GuestId).OnDelete(DeleteBehavior.NoAction);

        //spec
        modelBuilder.Entity<Amenity>().HasKey(k => k.AmenityId);
        //Rule
        modelBuilder.Entity<Rule>().HasKey(k => k.RuleId);



        base.OnModelCreating(modelBuilder);
    }


    public override int SaveChanges()
    {
        var entities = from e in ChangeTracker.Entries()
                       where e.State == EntityState.Added
                           || e.State == EntityState.Modified
                       select e.Entity;
        foreach (var entity in entities)
        {
            var validationContext = new ValidationContext(entity);
            Validator.ValidateObject(entity, validationContext, true);
        }

        return base.SaveChanges();
    }

}
