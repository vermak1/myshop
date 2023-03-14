using System;

namespace MyShop
{
    internal interface ICustomerRepository
    {
        CustomerInfo FindCustomerByName(String name);
        CustomerInfo FindCustomerById(Guid id);
    }
}
