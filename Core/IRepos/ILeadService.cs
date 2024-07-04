using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepos
{
    public interface ILeadService
    {
        Task<Lead> GetLeadById(int id);
        Task<Lead> GetLeadByServiceProfile(int serviceId);
        Task<bool> IsSheetIdExists(string sheetIdentifier);
        Task AddLead(Lead lead);
        void UpdateLead(Lead lead);

    }
}
