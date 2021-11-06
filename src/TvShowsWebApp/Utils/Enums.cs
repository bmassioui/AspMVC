using System.ComponentModel;

namespace TvShowsWebApp.Utils
{
    public class Enums
    {
        /// <summary>
        /// Allowed extensions
        /// </summary>
        public enum ImgExtentions : ushort
        {
            [Description(".jpg")]
            Jpg = 0,
            [Description(".jpeg")]
            Jpeg = 1,
            [Description(".png")]
            Png = 2
        }
    }
}
