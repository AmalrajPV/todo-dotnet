using Microsoft.EntityFrameworkCore;
using ToDoList.Models.Domain;

namespace ToDoList.Models
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions<MVCDemoDbContext> options)
            : base(options)
        {
            
        }



        // DbSet properties for your entities
     public DbSet<Tasks> Tasks { get; set; }

    }
}
