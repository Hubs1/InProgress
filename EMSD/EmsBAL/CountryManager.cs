using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmsDAL;
using EmsEntities;

namespace EmsBAL
{
    public class CountryManager
    {
        private UnitOfWork unitOfWork;
        public CountryManager()
        {
            unitOfWork = new UnitOfWork();
        }
        public IEnumerable<CountryEntity> Countries()
        {
            List<CountryEntity> countryEntities = new List<CountryEntity>();
            List<Country> listCountries = unitOfWork.CountryRepository.GetAll().ToList();

            foreach (Country c in listCountries)
            {
                CountryEntity countryEntity = new CountryEntity();
                countryEntity.Id = c.Id;
                countryEntity.Name = c.Name;
                countryEntities.Add(countryEntity);
            }
            return countryEntities;
        }
    }
}
