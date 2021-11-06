using System;

namespace TvShowsWebApp.Models
{
    public class TvShow
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Genre Genre { get; set; }

        public decimal Rating { get; set; }

        public string ImdbUrl { get; set; }
    }
}
