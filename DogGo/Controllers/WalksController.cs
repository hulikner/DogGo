using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using System.Security.Claims;
using System;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        public WalksController(
            IWalkRepository walkRepository,
            IOwnerRepository ownerRepository,
            IDogRepository dogRepository,
            IWalkerRepository walkerRepository,
            INeighborhoodRepository neighborhoodRepository)
        {
            _walkRepo = walkRepository;
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }
        // GET: WalksController
        public ActionResult Index()
        {
            List<Walk> walks = _walkRepo.GetAllWalks();

            return View(walks);
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalksController/Create
        public ActionResult Create()
        {
            List<Walker> walkers = _walkerRepo.GetAllWalkers();
            List<Dog> dogs = _dogRepo.GetAllDogs();
            WalkFormViewModel wfvm = new WalkFormViewModel
            {
                Walkers = walkers,
                Dogs = dogs,
                Walk = new Walk(),
                DogIds = new List<int>()
            };
            return View(wfvm); 
        }

        // POST: WalksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WalkFormViewModel wfvm)
        {
            try
            {
                foreach (int dogId in wfvm.DogIds)
                {
                    Walk walk = new Walk
                    {
                        Date = wfvm.Walk.Date,
                        Duration = wfvm.Walk.Duration,
                        WalkerId = wfvm.Walk.WalkerId,
                        DogId = dogId
                    };
                    _walkRepo.AddWalk(walk);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksController/Edit/5
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

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalksController/Delete/5
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
    }
}
