using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace EF_PostgreSQL.Model
{
    public class student
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        public int age { get; set; }
    }
    public class studentDbContext : DbContext
    {
        public DbSet<student> students { get; set; }

        public studentDbContext(DbContextOptions<studentDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    builder.UseNpgsql("Host=localhost;Username=postgres;Password=123456;Database=test2");
        //}
    }
}
