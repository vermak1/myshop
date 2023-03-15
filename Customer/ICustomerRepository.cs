using System;
using System.Threading.Tasks;

namespace MyShop
{
    internal interface ICustomerRepository
    {
        Task<CustomerInfo> FindCustomerByNameAsync(String firstName, String lastName);
        Task<CustomerInfo> FindCustomerByIdAsync(Guid id);
    }
}
