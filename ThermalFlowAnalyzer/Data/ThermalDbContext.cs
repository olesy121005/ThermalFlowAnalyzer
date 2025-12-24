using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ThermalFlowAnalyzer.Domain;

namespace ThermalFlowAnalyzer.Data
{
    public class ThermalDbContext : DbContext
    {
        public DbSet<AnalysisInput> Analyses { get; set; }
        public DbSet<AnalysisPoint> Points { get; set; }

        public ThermalDbContext(DbContextOptions<ThermalDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
