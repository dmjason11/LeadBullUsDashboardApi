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
    public class UserTaskRepo : IUserTaskService
    {
        private readonly AppDbContext _context;
        public UserTaskRepo(AppDbContext context) { _context = context; }
        public async Task AddUserTask(UserTask task)
        {
            await _context.UserTasks.AddAsync(task);
        }
        public async Task<List<UserTask>> getUserTasks(string userId)
        { 
            return await _context.UserTasks.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
