using Studenti.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Studenti.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Studente> Studenti { get; set; }
    }
}
