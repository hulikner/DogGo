using System.Collections.Generic;
using System.Linq;
namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walk> Walks { get; set; }
        public string TotalWalkTime
        {
            get
            {
                int totalMins = Walks.Select(w => w.Duration).Sum() / 60;
                int hrs = totalMins / 60;
                int mins = totalMins % 60;
                return $"{hrs}hr {mins}mins";
            }
        }
    }
}
