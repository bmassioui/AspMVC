using System.ComponentModel.DataAnnotations;

namespace TvShowsWebApp.Models
{
    public enum Genre : ushort
    {
        Drama = 0,
        Comedy = 1,
        Romance = 2,
        [Display(Name = "Romantic Comedy")]
        RomCom = 3,
        Crime = 4,
        Mystery = 5
    }
}
