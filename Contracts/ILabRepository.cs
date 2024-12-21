using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ILabRepository
    {
        Task<IEnumerable<Lab>> GetAllLabsAsync(bool trackChanges);
        Task<Lab> GetLabAsync(int labId, bool trackChanges);
        Task<IEnumerable<Lab>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteLab(Lab Lab);

        void CreateLabForDepartment(Lab Lab);


    }
}