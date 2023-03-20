using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyShop.CommonLib;

namespace MyShop
{
    internal interface ICarRepository
    {
        Task<List<CarInfo>> ListCarsAsync();
    }
}
