using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Files.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Files.Filter;
using Microsoft.AspNetCore.Http;
using Files.Helpers;

namespace Files.Controllers
{
    [ServiceFilter(typeof(VerificationSession))]
    public class ArchivesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArchivesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Archives
        public async Task<IActionResult> Index()
        {
            User user;
            int userId;

            userId = (int)HttpContext.Session.GetInt32(SessionUser.Id);
            user = await _context.User.Where(x => x.Id == userId && x.IsActive).FirstOrDefaultAsync();
            if (user.RoleId == SessionRole.Administrador)
                return View(await _context.Archive.Include(x => x.Folder).Where(x => x.IsActive).ToListAsync());
            else
                return View(await _context.Archive.Include(x => x.Folder).Where(x => x.UserId == userId && x.IsActive).ToListAsync());
        }

        // GET: Archives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archive = await _context.Archive.Include(x => x.Folder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archive == null)
            {
                return NotFound();
            }

            return View(archive);
        }

        // GET: Archives/Create
        public IActionResult Create(int? folderId)
        {
            int userId;

            userId = (int)HttpContext.Session.GetInt32(SessionUser.Id);
            ViewData["ListFolders"] = new SelectList(_context.Folder.Where(x => x.UserId == userId && x.IsActive), "Id", "Name", folderId);
            ViewBag.FolderId = folderId;

            return View();
        }

        // POST: Archives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Base64,Extension,FolderId,OnlyRead,Id,IsActive,DateRegistration,FormFile")] Archive archive)
        {
            if (ModelState.IsValid)
            {                
                string filePath;
                FileStream fileStream;
                Guid guid;
                string fileName;

                guid = Guid.NewGuid();
                fileName = $"{guid}_{archive.FormFile.FileName}";
                filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "files", fileName);
                fileStream = new FileStream(filePath, FileMode.Create);
                await archive.FormFile.CopyToAsync(fileStream);
                FileInfo fileInfo = new FileInfo(filePath);
                Archive fileEntity = new Archive
                {
                    Base64 = $@"/files/{fileName}",
                    DateRegistration = DateTime.Now,
                    IsActive = true,
                    FolderId = archive.FolderId,
                    OnlyRead = true,
                    Extension = fileInfo.Extension,
                    UserId = (int)HttpContext.Session.GetInt32(SessionUser.Id),
                    Name = archive.FormFile.FileName
                };

                _context.Add(fileEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(archive);
        }

        // GET: Archives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archive = await _context.Archive.FindAsync(id);
            if (archive == null)
            {
                return NotFound();
            }
            int userId;

            userId = (int)HttpContext.Session.GetInt32(SessionUser.Id);
            ViewData["ListFolders"] = new SelectList(_context.Folder.Where(x => x.UserId == userId && x.IsActive), "Id", "Name");
            return View(archive);
        }

        // POST: Archives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Base64,Extension,FolderId,OnlyRead,Id,IsActive,DateRegistration")] Archive archive)
        {
            if (id != archive.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(archive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArchiveExists(archive.Id))
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
            return View(archive);
        }

        // GET: Archives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archive = await _context.Archive.Include(x => x.Folder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archive == null)
            {
                return NotFound();
            }

            return View(archive);
        }

        // POST: Archives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var archive = await _context.Archive.FindAsync(id);
            //_context.Archive.Remove(archive);
            archive.IsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArchiveExists(int id)
        {
            return _context.Archive.Any(e => e.Id == id);
        }
    }
}
