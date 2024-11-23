using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NET_MVC_LeetSpeak_Text_Generator.Models;

namespace NET_MVC_LeetSpeak_Text_Generator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApiCall> ApiCalls { get; set; }
    }
}
