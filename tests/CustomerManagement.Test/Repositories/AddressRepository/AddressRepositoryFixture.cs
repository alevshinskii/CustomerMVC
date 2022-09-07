using System.Data.SqlClient;
using CustomerManagement.Entities;
using CustomerManagement.Test.CustomerRepository;

namespace CustomerManagement.Test.AddressRepository
{
    public class AddressRepositoryFixture
    {
        private readonly CustomerRepositoryFixture customerFixture = new CustomerRepositoryFixture();
        public void DeleteAll()
        {
            customerFixture.DeleteAll();
        }

        public Address GetAddress()
        {
            var customerRepository = customerFixture.GetCustomerRepository();
            var customer = customerRepository.Create(customerFixture.GetCustomer());

            return new Address()
            {
                AddressId = 1,
                AddressLine = "AddressLine",
                AddressLine2 = "AddressLine2",
                AddressType = "Shipping",
                City = "New York",
                Country = "United States",
                CustomerId = customer.Id,
                PostalCode = "123456",
                State = "New York"
            };
        }

        public AddressTestRepository GetAddressRepository()
        {
            return new AddressTestRepository();
        }
    }
}

