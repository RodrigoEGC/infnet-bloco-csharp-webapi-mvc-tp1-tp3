using System.Threading.Tasks;
using Domain.Model.Entities;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MusicLibraryApplication.Controllers
{
    public class MusicalGroupController : Controller
    {
        private readonly IGroupService _groupService;

        public MusicalGroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: MusicalGroup
        public async Task<IActionResult> Index()
        {
            var group = await _groupService.GetAllAsync();
            return View(group);
        }

        // GET: MusicalGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _groupService.GetByIdAsync(id.Value);

            if (groupEntity == null)
            {
                return NotFound();
            }

            return View(groupEntity);
        }

        // GET: MusicalGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MusicalGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,GroupName,MusicalGenre,Beginnings,City,Nation,BandMascot")] GroupEntity groupEntity)
        {
            if (ModelState.IsValid)
            {
                try
                { 
                    await _groupService.InsertAsync(groupEntity);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(groupEntity);
        }

        // GET: MusicalGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _groupService.GetByIdAsync(id.Value);
            if (groupEntity == null)
            {
                return NotFound();
            }
            return View(groupEntity);
        }

        // POST: MusicalGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,GroupName,MusicalGenre,Beginnings,City,Nation,BandMascot")] GroupEntity grouEntity)
        {
            if (id != grouEntity.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _groupService.UpdateAsync(grouEntity);
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                    return View(grouEntity);
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (await _groupService.GetByIdAsync(id) == null)
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
            return View(grouEntity);
        }

        // GET: MusicalGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _groupService.GetByIdAsync(id.Value);
            if (groupEntity == null)
            {
                return NotFound();
            }

            return View(groupEntity);
        }

        // POST: MusicalGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _groupService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckMascot(string mascot, int id)
        {
            if (await _groupService.CheckMascotAsync(mascot, id))
            {
                return Json($"Band Mascot {mascot} já existe!");
            }

            return Json(true);
        }
    }
}
