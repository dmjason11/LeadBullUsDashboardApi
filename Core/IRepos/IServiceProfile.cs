using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepos
{
    public interface IServiceProfile
    {
        Task<ServiceProfile> GetServiceProfile(int id);
        Task<ServiceProfile> GetServiceProfileWithLead(int id);
        Task<bool> IsServiceExists(int id);
        Task<bool> IsServiceExists(string serviceName);
        Task<IEnumerable<ServiceProfile>> GetUserProfiles(string userId);
        Task AddServiceProfile(ServiceProfile profile);

    }
}
