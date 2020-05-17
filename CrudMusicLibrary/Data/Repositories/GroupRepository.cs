using Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities;
using Microsoft.Extensions.Options;
using Domain.Model.Options;
using Domain.Model.Exceptions;

namespace Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly MusicLibraryDBContext _musicLibraryDBContext;
        private readonly IOptionsMonitor<TestOption> _testOption;
        public GroupRepository(
            MusicLibraryDBContext musicLibraryDBContext,
            IOptionsMonitor<TestOption> testOption)
        {
            _musicLibraryDBContext = musicLibraryDBContext;
            _testOption = testOption;
        }
        public async Task<IEnumerable<GroupEntity>> GetAllAsync()
        {
            return await _musicLibraryDBContext.MusicalGroups.ToListAsync();
        }

        public async Task<GroupEntity> GetByIdAsync(int id)
        {
            return await _musicLibraryDBContext.MusicalGroups.FirstOrDefaultAsync(x => x.GroupId == id);
        }

        public async Task InsertAsync(GroupEntity insertedEntity)
        {
            _musicLibraryDBContext.Add(insertedEntity);
            await _musicLibraryDBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(GroupEntity updatedEntity)
        {
            try
            {
                _musicLibraryDBContext.Update(updatedEntity);
                await _musicLibraryDBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetByIdAsync(updatedEntity.GroupId) == null)
                {
                    throw new RepositoryException("Group not found!");
                }
                else
                {
                    throw;
                }
            }
            
        }
        public async Task DeleteAsync(int id)
        {
            var groupModel = await _musicLibraryDBContext.MusicalGroups.FindAsync(id);
            _musicLibraryDBContext.MusicalGroups.Remove(groupModel);
            await _musicLibraryDBContext.SaveChangesAsync();
        }
        public async Task<GroupEntity> GetByMascotAsync(string mascot)
        {
            return await _musicLibraryDBContext.MusicalGroups.SingleOrDefaultAsync(x =>
                x.BandMascot == mascot);
        }

        public async Task<bool> CheckMascotAsync(string mascot, int id = 0)
        {
            var mascotExists = await _musicLibraryDBContext.MusicalGroups.AnyAsync(x =>
                x.BandMascot == mascot && x.GroupId != id);
            return mascotExists;
        }
    }
}
