using CustomerManagement.Entities;
using Xunit;

namespace CustomerManagement.Test.CustomerRepository
{
    public class CustomerRepositoryTest
    {
        private readonly CustomerRepositoryFixture Fixture = new CustomerRepositoryFixture();

        [Fact]
        public void ShouldBeAbleToCreateCustomerRepo()
        {
            Repositories.CustomerRepository repository = Fixture.GetCustomerRepository();
            Assert.NotNull(repository);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            Fixture.DeleteAll();

            Repositories.CustomerRepository repository = Fixture.GetCustomerRepository();
            Customer customer = Fixture.GetCustomer();

            var createdCustomer = repository.Create(customer);

            Assert.NotNull(createdCustomer);
        }

        [Fact]
        public void ShouldBeAbleToReadCustomer()
        {
            Fixture.DeleteAll();

            Repositories.CustomerRepository repository = Fixture.GetCustomerRepository();
            Customer customer = Fixture.GetCustomer();

            var createdCustomer = repository.Create(customer);

            var readedCustomer = repository.Read(createdCustomer.Id);

            Assert.Equal(createdCustomer.Id, readedCustomer.Id);
            Assert.Equal(createdCustomer.FirstName, readedCustomer.FirstName);
            Assert.Equal(createdCustomer.LastName, readedCustomer.LastName);
            Assert.Equal(createdCustomer.Email, readedCustomer.Email);
            Assert.Equal(createdCustomer.PhoneNumber, readedCustomer.PhoneNumber);
            Assert.Equal(createdCustomer.TotalPurchasesAmount, readedCustomer.TotalPurchasesAmount);
        }

        [Fact]
        public void ShouldBeAbleToUpdateCustomer()
        {
            Fixture.DeleteAll();

            Repositories.CustomerRepository repository = Fixture.GetCustomerRepository();
            Customer customer = Fixture.GetCustomer();

            var createdCustomer = repository.Create(customer);
            createdCustomer.Email = "newemail@email.com";

            repository.Update(createdCustomer);

            var updatedCustomer = repository.Read(createdCustomer.Id);

            Assert.NotEqual(customer.Email, updatedCustomer.Email);
            Assert.Equal(createdCustomer.Email, updatedCustomer.Email);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            Fixture.DeleteAll();

            Repositories.CustomerRepository repository = Fixture.GetCustomerRepository();
            Customer customer = Fixture.GetCustomer();

            var createdCustomer = repository.Create(customer);

            repository.Delete(createdCustomer.Id);

            var readedCustomer = repository.Read(createdCustomer.Id);

            Assert.Null(readedCustomer);
        }
    }
}