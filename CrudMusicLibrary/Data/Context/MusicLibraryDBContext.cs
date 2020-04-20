using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MusicLibraryDBContext : DbContext
    {
        public MusicLibraryDBContext (DbContextOptions<MusicLibraryDBContext> options)
            : base(options)
        {
        }
        public DbSet<GroupEntity> MusicalGroups { get; set; }
        public DbSet<DiscographyEntity> discographies { get; set; }
    }
}
