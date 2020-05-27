using Domain.Model.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Entities;
using Domain.Model.Exceptions;

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

        public async Task InsertAsync(GroupEntity insertedEntity)
        {
            var mascotExists = await _groupRepository.CheckMascotAsync(insertedEntity.BandMascot);
            if (mascotExists)
            {
                throw new EntityValidationException(nameof(GroupEntity.BandMascot), "Mascot pertence a outra banda!");
            }
            await _groupRepository.InsertAsync(insertedEntity);
        }

        public async Task UpdateAsync(GroupEntity updatedEntity)
        {
            var mascotExists = await _groupRepository.CheckMascotAsync(updatedEntity.BandMascot, updatedEntity.GroupId);
            if (mascotExists)
            {
                throw new EntityValidationException(nameof(GroupEntity.BandMascot), "Mascot pertence a outra banda!");
            }
            await _groupRepository.UpdateAsync(updatedEntity);
        }

        public async Task<bool> CheckMascotAsync(string mascot, int id)
        {
            return await _groupRepository.CheckMascotAsync(mascot, id);
        }
    }
}
