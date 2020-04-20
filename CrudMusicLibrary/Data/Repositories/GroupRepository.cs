using Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities;

namespace Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly MusicLibraryDBContext _musicLibraryDBContext;
        public GroupRepository(MusicLibraryDBContext musicLibraryDBContext)
        {
            _musicLibraryDBContext = musicLibraryDBContext;
        }

        public async Task DeleteAsync(int id)
        {
            var groupModel = await _musicLibraryDBContext.MusicalGroups.FindAsync(id);
            _musicLibraryDBContext.MusicalGroups.Remove(groupModel);
            await _musicLibraryDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupEntity>> GetAllAsync()
        {
            return await _musicLibraryDBContext.MusicalGroups.ToListAsync();
        }

        public async Task<GroupEntity> GetByIdAsync(int id)
        {
            return await _musicLibraryDBContext.MusicalGroups.FirstOrDefaultAsync(x => x.GroupId == id);
        }

        public async Task InsertAsync(GroupEntity insertedModel)
        {
            _musicLibraryDBContext.Add(insertedModel);
            await _musicLibraryDBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(GroupEntity updatedModel)
        {
            _musicLibraryDBContext.Update(updatedModel);
            await _musicLibraryDBContext.SaveChangesAsync();
        }
    }
}
