
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin.Services;

[Authorize]
public class MoviesController : Controller
{
    private readonly MoviesAdminContext _context;
    private readonly AzureStorageService _blobService;
    public MoviesController(MoviesAdminContext context, AzureStorageService blobService)
    {
        _context = context;
        _blobService = blobService;
    }



    // GET: MOVIES
    public async Task<IActionResult> Index(string sortOrder)
    {
        //take str from click event & determine if asc/desc
        ViewData["TitleSort"] = sortOrder == "title" ? "title_desc" : "title";
        ViewData["ReleaseDateSort"] = sortOrder == "releaseDate" ? "releaseDate_desc" : "releaseDate";
        ViewData["CreatedDateSort"] = sortOrder == "createdDate" ? "createdDate_desc" : "createdDate";

        var movies = from m in _context.Movie
                     select m;

        //switchcases are the devils elif
        movies = sortOrder switch
        {
            "title" => movies.OrderBy(m => m.Title),
            "title_desc" => movies.OrderByDescending(m => m.Title),
            "releaseDate" => movies.OrderBy(m => m.ReleaseDate),
            "releaseDate_desc" => movies.OrderByDescending(m => m.ReleaseDate),
            "createdDate" => movies.OrderBy(m => m.CreatedDate),
            "createdDate_desc" => movies.OrderByDescending(m => m.CreatedDate),
            _ => movies.OrderBy(m => m.ReleaseDate),
        };
        return View(await movies
            .Include(m => m.Genre) // this was BEFORE the return statement in his code
            .Include(m => m.AudienceRating)
            .AsNoTracking()
            .ToListAsync()
            );
    }




    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound(); // return 404 if if null
        }

        // retrieve movie with specified id from db
        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    [Authorize(Roles="Admin")]
    public IActionResult Create()
    {
        var genres = _context.Genre.ToList();
        var ratings = _context.AudienceRating.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");
        ViewBag.AudienceRatingId = new SelectList(ratings, "Id", "Title");
        return View();
    }

    /*
     TO-DO: Handle image being required
     */

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,GenreId,AudienceRatingId,Runtime,ReleaseDate,CreatedDate")] Movie movie, IFormFile? imageFile)
    {
        // validate if image file is uploaded
        if (imageFile == null)
        {
            ModelState.AddModelError("imageFile", "The image file upload is required.");
        }

        //check if model state is valid before adding movie to db
        if (ModelState.IsValid)
        {
            // generate unique file name to prevent overwriting
            var extension = Path.GetExtension(imageFile!.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";

            // save file in azure
            string imageUrl = await _blobService.UploadFileAsync(imageFile, imageFile.FileName);
            // save image url in 
            movie.ImageUrl = imageUrl;

            // save record in database
            _context.Add(movie);
            await _context.SaveChangesAsync();

            // Redirect to the index action after successfully creating movie
            return RedirectToAction(nameof(Index));
        }

        var genres = _context.Genre.ToList();
        var ratings = _context.AudienceRating.ToList();

        ViewBag.GenreId = new SelectList(genres, "Id", "Title");
        ViewBag.AudienceRatingId = new SelectList(ratings, "Id", "Title");

        return View(movie);
    }

    // GET: MOVIES/Edit/5
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        // This could be wrong
        var genres = _context.Genre.ToList();
        var ratings = _context.AudienceRating.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");
        ViewBag.AudienceRatingId = new SelectList(ratings, "Id", "Title");

        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Description,GenreId,AudienceRatingId,Runtime,ReleaseDate,CreatedDate,ImageUrl")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
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
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}
