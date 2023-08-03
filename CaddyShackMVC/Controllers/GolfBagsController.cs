using CaddyShackMVC.DataAccess;
using CaddyShackMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaddyShackMVC.Controllers
{
    public class GolfBagsController : Controller
    {
        private readonly CaddyShackContext _context;

        public GolfBagsController(CaddyShackContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var golfBag = _context.GolfBags.AsEnumerable();
            return View(golfBag);
        }

        [Route("golfbags/{id:int}")]
        public IActionResult Show(int id)
        {
            var golfBag = _context.GolfBags
                .Where(g => g.Id == id)
                .Include(g => g.Clubs)
                .FirstOrDefault();

            return View(golfBag);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(GolfBag golfBag)
        {
            _context.GolfBags.Add(golfBag);
            _context.SaveChanges();

            var golfBagId = golfBag.Id;

            return RedirectToAction("show", new { id = golfBagId });
        }

        [Route("golfbags/{id:int}/edit")]
        public IActionResult Edit(int id)
        {
            var golfBag = _context.GolfBags.Find(id);
            return View(golfBag);
        }

        [HttpPost]
        [Route("golfbags/{id:int}")]
        public IActionResult Update(int id, GolfBag golfBag)
        {
            golfBag.Id = id;
            _context.GolfBags.Update(golfBag);
            _context.SaveChanges();

            return RedirectToAction("show", new { id = golfBag.Id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var golfBag = _context.GolfBags.Find(id);
            _context.GolfBags.Remove(golfBag);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
