using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvShowsWebApp.Models
{
    public class TvShow
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Genre Genre { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; }

        public string ImdbUrl { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        // CreatedBy 
        // CreatedAt
        // UpdatedBy
        // UpdatedAt

    }
}
