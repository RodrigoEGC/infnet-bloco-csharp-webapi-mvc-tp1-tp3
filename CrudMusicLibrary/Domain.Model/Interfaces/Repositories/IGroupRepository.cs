using Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<GroupEntity>> GetAllAsync();

        Task<GroupEntity> GetByIdAsync(int id);

        Task DeleteAsync(int id);

        Task InsertAsync(GroupEntity insertedModel);

        Task UpdateAsync(GroupEntity updatedModel);

    }
}
