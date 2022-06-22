using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkFormViewModel
    {
        public List<Walker> Walkers { get; set; }
        public List<Dog> Dogs { get; set; }
        public List<Walk> Walks { get; set; }
    }
}