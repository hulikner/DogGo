using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        public List<Walk> GetAllWalks();
        public void AddWalk(Walk walk);
        public void DeleteWalk(int walkId);
    }
}
