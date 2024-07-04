using Core.IRepos;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ILeadService _leadService => new LeadService(_context);

        public IServiceProfile _serviceProfile => new ServiceProfileRepo(_context);

        public IUserTaskService _userTaskService => new UserTaskRepo(_context);

        public async Task<bool> saveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
