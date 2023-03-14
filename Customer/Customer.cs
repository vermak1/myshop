using MyShop.Database;
using System;

namespace MyShop
{
    internal class Customer
    {
        private readonly ICustomerRepository _repository;

        private Customer(ICustomerRepository repo)
        {
            _repository = repo; 
        }

        public static Customer CreateForSQL()
        {
            SQLCustomer sqlCustomer = new SQLCustomer();
            return new Customer(sqlCustomer);
        }

        public CustomerInfo FindCustomerByName(String name)
        {
            return _repository.FindCustomerByName(name);
        }

        public CustomerInfo FindCustomerById(Guid id)
        {
            return _repository.FindCustomerById(id);
        }
    }
}
