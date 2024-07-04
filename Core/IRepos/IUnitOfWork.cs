using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepos
{
    public interface IUnitOfWork
    {
        ILeadService _leadService {get; }
        IServiceProfile _serviceProfile { get; }
        IUserTaskService _userTaskService { get; }

        Task<bool> saveChanges();

    }
}
