using System;
using System.Threading.Tasks;
using MyShop.Database;

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

        public async Task<CustomerInfo> FindCustomerByNameAsync(String firstName, String lastName)
        {
            return await _repository.FindCustomerByNameAsync(firstName, lastName);
        }

        public async Task<CustomerInfo> FindCustomerByIdAsync(Guid id)
        {
            return await _repository.FindCustomerByIdAsync(id);
        }
    }
}
