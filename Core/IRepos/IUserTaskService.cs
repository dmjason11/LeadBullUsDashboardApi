using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepos
{
    public interface IUserTaskService
    {
        Task<List<UserTask>> getUserTasks(string userId);
        Task AddUserTask(UserTask task);
    }
}
