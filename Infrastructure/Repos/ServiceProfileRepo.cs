using Core;
using Core.IRepos;
using Google.GData.Client;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class ServiceProfileRepo : IServiceProfile
    {
        private readonly AppDbContext _context;

        public ServiceProfileRepo(AppDbContext context)
        {
            _context = context; 
        }

        public async Task AddServiceProfile(ServiceProfile profile)
        {
            await _context.ServiceProfile.AddAsync(profile);
        }

        public async Task<ServiceProfile> GetServiceProfile(int id)
        {
            return await _context.ServiceProfile.FirstOrDefaultAsync(x=> x.Id == id);
        }
        public async Task<ServiceProfile> GetServiceProfileWithLead(int id)
        {
            return await _context.ServiceProfile.Include(x=>x.Leads).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<ServiceProfile>> GetUserProfiles(string userId)
        {
            return await _context.ServiceProfile.Where(x=>x.UserId == userId).ToListAsync();
        }

        public async Task<bool> IsServiceExists(string serviceName)
        {
            return await _context.ServiceProfile.AnyAsync(x=>x.ServiceName == serviceName);
        }

        public async Task<bool> IsServiceExists(int id)
        {
            return await _context.ServiceProfile.AnyAsync(x => x.Id == id);
        }
    }
}
