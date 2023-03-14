using System;
using System.Collections.Generic;

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

        public List<CarInfo> ListCars()
        {
            return _carRepository.ListCars();
        }
    }
}
