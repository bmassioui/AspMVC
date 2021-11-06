using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TvShowsWebApp.Data;
using TvShowsWebApp.Models;
using TvShowsWebApp.Utils;

namespace TvShowsWebApp.Controllers
{
    public class TvShowsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TvShowsController> _logger;
        private readonly IWebHostEnvironment _environment;

        public TvShowsController(ApplicationDbContext context, ILogger<TvShowsController> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        // GET: TvShows
        public async Task<IActionResult> Index()
        {
            return View(await _context.TvShow.ToListAsync());
        }

        // GET: TvShows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // GET: TvShows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TvShows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Rating,ImdbUrl")] TvShow tvShow, IFormFile tvShowImg)
        {
            if (!IsTvShowImgValid(tvShowImg?.FileName)) return View(tvShow); // Show a message

            tvShow.ImageUrl = Url.Content(GetTVShowImgPath(tvShowImg));

            if (!ModelState.IsValid) return View(tvShow);

            _context.Add(tvShow);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: TvShows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShow.FindAsync(id);
            if (tvShow == null)
            {
                return NotFound();
            }
            return View(tvShow);
        }

        // POST: TvShows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Genre,Rating,ImdbUrl,IsDeleted,ImageUrl")] TvShow tvShow)
        {
            if (id != tvShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tvShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TvShowExists(tvShow.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        // GET: TvShows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // POST: TvShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tvShow = await _context.TvShow.FindAsync(id);
            tvShow.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region private methods
        private bool TvShowExists(Guid id)
        {
            return _context.TvShow.Any(e => e.Id == id);
        }

        private static bool IsTvShowImgValid(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;

            var extension = Path.GetExtension(fileName);
            extension = extension[1].ToString().ToUpper() + extension[2..].ToString();

            return Enum.IsDefined(typeof(Enums.ImgExtentions), extension);
        }

        public string GetTVShowImgPath(IFormFile image)
        {
            if (image is null) return string.Empty;

            //Set Key Name
            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            //Get url To Save
            string SavePath = Path.Combine(_environment.WebRootPath, "/images/movies/", ImageName);

            using (var stream = new FileStream(SavePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return $"/images/movies/{ImageName}";
        }
        #endregion private methods
    }
}
