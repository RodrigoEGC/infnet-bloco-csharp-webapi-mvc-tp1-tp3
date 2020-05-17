using Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupEntity>> GetAllAsync();

        Task<GroupEntity> GetByIdAsync(int id);

        Task DeleteAsync(int id);

        Task UpdateAsync(GroupEntity updatedModel);

        Task InsertAsync(GroupEntity insertedModel);

        Task<bool> CheckMascotAsync(string mascot, int id = -1);
    }
}
