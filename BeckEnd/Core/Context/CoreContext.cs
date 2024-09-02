using BeckEnd.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeckEnd.Core.Context;

public class CoreContext : DbContext
{
    public CoreContext(DbContextOptions<CoreContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Candidate> Candidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Job>()
            .HasOne(Job => Job.Company)
            .WithMany(Company => Company.Jobs)
            .HasForeignKey(Job => Job.CompanyId);

        modelBuilder.Entity<Candidate>()
            .HasOne(Candidate => Candidate.Job)
            .WithMany(Job => Job.Candidates)
            .HasForeignKey(Candidate => Candidate.JobId);

        modelBuilder.Entity<Company>()
            .Property(company => company.Size)
            .HasConversion<string>();

        modelBuilder.Entity<Job>()
            .Property(job => job.Level)
            .HasConversion<string>();
    }


}
