using System;
using System.Threading.Tasks;
using MyShop.CommonLib;

namespace MyShop
{
    internal interface ICustomerRepository
    {
        Task<CustomerInfo> FindCustomerByNameAsync(String firstName, String lastName);
        Task<CustomerInfo> FindCustomerByIdAsync(Guid id);
        Task<Int32> CreateCustomerAsync(CustomerInfo customer);
    }
}
