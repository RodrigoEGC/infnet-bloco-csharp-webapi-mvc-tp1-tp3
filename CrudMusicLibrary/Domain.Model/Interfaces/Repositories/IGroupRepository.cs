using Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<GroupEntity>> GetAllAsync();
        Task<GroupEntity> GetByIdAsync(int id);
        Task InsertAsync(GroupEntity insertedEntity);
        Task UpdateAsync(GroupEntity updateEntity);
        Task DeleteAsync(int id);
        Task <bool> CheckMascotAsync(string mascot, int id = -1);
        Task<GroupEntity> GetByMascotAsync(string mascot);
    }
}
