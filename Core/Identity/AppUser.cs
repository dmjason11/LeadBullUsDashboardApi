using Microsoft.AspNetCore.Identity;
namespace Core.Identity
{
    public class AppUser:IdentityUser
    {
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public ICollection<ServiceProfile> profiles;
        public ICollection<Complain> Complains { get; set; }
        public ICollection<UserTask> Tasks { get; set; }
        public ICollection<Company> Companies { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<PaymentHistory> PaymentHistories { get; set; }
        public ICollection<UserLogger> UserLoggers { get; set; }
        public ICollection<ServiceProfile> ServiceProfiles { get; set; }
    }
}
