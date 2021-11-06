using TvShowsWebApp.Data;

namespace TvShowsWebApp.Repository
{
    public class BaseRepository<T> where T: class // Should be restricted
    {
        private readonly ApplicationDbContext _dbcontext;

        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _dbcontext = applicationDbContext;
        }

        // Add Methods here
        
    }
}
