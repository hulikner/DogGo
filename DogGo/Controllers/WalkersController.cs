using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Models.ViewModels;
using System.Collections.Generic;
using DogGo.Models;
using DogGo.Repositories;
using System.Security.Claims;



namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
       
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(
            IWalkerRepository walkerRepository,
            IWalkRepository walkRepository,
            INeighborhoodRepository neighborhoodRepo)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
            _neighborhoodRepo = neighborhoodRepo;
        }
        // GET: Walkers
        public ActionResult Index()
        {
            if (GetCurrentUserId() != -1)
            {
                Walker walker = _walkerRepo.GetWalkerById(GetCurrentUserId());
                List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(walker.NeighborhoodId);
                return View(walkers);
            }
            else
            {
                List<Walker> walkers = _walkerRepo.GetAllWalkers();
                return View(walkers);
            }

        }

        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walk> walks = _walkRepo.GetWalksByWalkerId(walker.Id);

            if (walker == null)
            {
                return NotFound();
            }

            WalkerProfileViewModel wpvm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks
            };

            return View(wpvm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            WalkerFormViewModel vm = new WalkerFormViewModel()
            {
                Walker = new Walker(),
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walker walker)
        {
            try
            {
                _walkerRepo.AddWalker(walker);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(walker);
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return -1;
            }
            else
            {
                return int.Parse(id);
            }
        }
    }
}
