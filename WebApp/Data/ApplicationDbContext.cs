
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<Box> Boxes => Set<Box>();
    public DbSet<BoxItem> BoxItems => Set<BoxItem>();
    public DbSet<Checklist> Checklists => Set<Checklist>();
    public DbSet<ChecklistItem> ChecklistItems => Set<ChecklistItem>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Instrument>().HasIndex(i => i.Nombre);
        b.Entity<Instrument>().HasIndex(i => i.Tipo);
        b.Entity<Instrument>().HasIndex(i => i.CodigoInterno).IsUnique();

        b.Entity<BoxItem>().HasOne(x => x.Box).WithMany(x => x.Items).HasForeignKey(x => x.BoxId);
        b.Entity<BoxItem>().HasOne(x => x.Instrument).WithMany(x => x.Boxes).HasForeignKey(x => x.InstrumentId);

        b.Entity<Checklist>().HasOne(x => x.Box).WithMany(x => x.Checklists).HasForeignKey(x => x.BoxId);
        b.Entity<ChecklistItem>().HasOne(x => x.Checklist).WithMany(x => x.Items).HasForeignKey(x => x.ChecklistId);
        b.Entity<ChecklistItem>().HasOne(x => x.Instrument).WithMany(x => x.ChecklistItems).HasForeignKey(x => x.InstrumentId);
    }
}
