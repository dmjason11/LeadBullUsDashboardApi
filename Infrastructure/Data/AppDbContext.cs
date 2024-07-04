

using Core;
using Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>opt):base(opt)
        {
            
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<Lead> leads { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentHistory> paymentHistories { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserLogger> UserLoggers { get; set; }
        public DbSet<ServiceProfile> ServiceProfile { get; set; }
        
    }
}
