using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyShop
{
    internal interface ICarRepository
    {
        Task<List<CarInfo>> ListCarsAsync();
    }
}
