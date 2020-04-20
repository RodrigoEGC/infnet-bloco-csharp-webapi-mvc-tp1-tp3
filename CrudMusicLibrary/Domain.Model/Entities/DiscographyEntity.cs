using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model.Entities
{
    public class DiscographyEntity
    {
        [Key]
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int GroupId { get; set; }
    }
}
