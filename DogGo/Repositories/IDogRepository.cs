using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        Dog GetDogById(int id);
        List<Dog> GetDogsByOwnerId(int ownerId);
        void AddDog(Dog owner);
        void UpdateDog(Dog owner);
        void DeleteDog(int id);
    }
}
