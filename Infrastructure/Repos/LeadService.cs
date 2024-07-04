using Core;
using Core.IRepos;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class LeadService : ILeadService
    {
        private readonly AppDbContext _context;
        public LeadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLead(Lead lead)
        {
            await _context.leads.AddAsync(lead);
        }

        public async Task<Lead> GetLeadById(int id)
        {
            return await _context.leads.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Lead> GetLeadByServiceProfile(int serviceId)
        {
            return await _context.leads.FirstOrDefaultAsync(x => x.ServiceProfileId == serviceId);
        }

        public async Task<bool> IsSheetIdExists(string sheetIdentifier)
        {
            return await _context.leads.AnyAsync(x => x.sheetIdentifier == sheetIdentifier);
        }

        public void UpdateLead(Lead lead)
        {
           _context.leads.Update(lead);
        }
    }
}
