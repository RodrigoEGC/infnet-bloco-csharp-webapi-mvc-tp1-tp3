using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities
{
    public class GroupEntity
    {
        [Key]
        public int GroupId { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        
        public string GroupName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string MusicalGenre { get; set; }

        [Display(Name = "Beginnings")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Este campo é obrigatório")]

        public DateTime Beginnings { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo é obrigatório")]

        public string City { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo é obrigatório")]

        public string Nation { get; set; }

        public ICollection<DiscographyEntity> discographyEntities { get; set; }
    }
}
