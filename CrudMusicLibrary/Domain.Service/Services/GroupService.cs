using Domain.Model.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Entities;

namespace Domain.Service.Services
{
    public class GroupService: IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _groupRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GroupEntity>> GetAllAsync()
        {
           return await _groupRepository.GetAllAsync();
        }

        public async Task<GroupEntity> GetByIdAsync(int id)
        {
            return await _groupRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(GroupEntity insertedModel)
        {
            await _groupRepository.InsertAsync(insertedModel);
        }

        public async Task UpdateAsync(GroupEntity updatedModel)
        {
            await _groupRepository.UpdateAsync(updatedModel);
        }
    }
}
