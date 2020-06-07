using ProjectAPI.Data;
using ProjectAPI.DTOs;
using ProjectAPI.Models;
using ProjectAPI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NationalParkRepository(ApplicationDbContext db)
        {
            _dbContext = db;
        }


        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _dbContext.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _dbContext.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _dbContext.NationalParks.FirstOrDefault(a => a.id == nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _dbContext.NationalParks.OrderBy(a => a.name).ToList();
        }

        public bool NationalParkExsists(string name)
        {
            bool value = _dbContext.NationalParks.Any(a => a.name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExsists(int id)
        {
            return _dbContext.NationalParks.Any(a => a.id == id);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _dbContext.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
