using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop
{
    internal class Car
    {
        private readonly ICarRepository _carRepository;

        private Car(ICarRepository repository)
        {
            _carRepository = repository;
        }

        public static Car CreateForSql()
        {
            SQLCars sql = new SQLCars();
            return new Car(sql);
        }

        public async Task<List<CarInfo>> ListCarsAsync()
        {
            return await _carRepository.ListCarsAsync();
        }
    }
}
