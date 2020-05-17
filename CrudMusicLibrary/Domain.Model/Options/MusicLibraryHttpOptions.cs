using System;

namespace Domain.Model.Options
{
    public class MusicLibraryHttpOptions
    {
        public string Name { get; set; }
        public Uri ApiBaseUrl { get; set; }
        public string  GroupPath { get; set; }
        public int Timeout { get; set; }
    }
}
