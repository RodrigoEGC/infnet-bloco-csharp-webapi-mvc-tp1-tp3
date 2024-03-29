﻿using Data.Context.Configuration;
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
        public DbSet<DiscographyEntity> Discographies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
