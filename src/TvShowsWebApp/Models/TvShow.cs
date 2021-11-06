using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvShowsWebApp.Models
{
    public class TvShow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Imdb Link")]
        public string ImdbUrl { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Poster")]
        public string ImageUrl { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; } // To deal with it later

        // CreatedBy 
        // CreatedAt
        // UpdatedBy
        // UpdatedAt

    }
}
