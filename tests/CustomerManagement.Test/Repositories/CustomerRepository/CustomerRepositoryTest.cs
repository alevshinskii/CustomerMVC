using System;
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

        [Fact]
        public void ShouldBeAbleToReadAllCustomers()
        {
            Fixture.DeleteAll();

            Repositories.CustomerRepository repository = Fixture.GetCustomerRepository();

            repository.Create(Fixture.GetCustomer());
            repository.Create(Fixture.GetCustomer());

            var customers = repository.ReadAll();

            Assert.NotEmpty(customers);
            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public void ShouldNotBeAbleToReadAllCustomersById()
        {
            Fixture.DeleteAll();

            Repositories.CustomerRepository repository = Fixture.GetCustomerRepository();

            repository.Create(Fixture.GetCustomer());
            repository.Create(Fixture.GetCustomer());

            Assert.Throws<InvalidOperationException>(() => repository.ReadAll(1));
        }

        [Fact]
        public void ShouldUpdateReturnsFalseIfNoLinesAffected()
        {
            Fixture.DeleteAll();

            Repositories.CustomerRepository repository = Fixture.GetCustomerRepository();

            var customer = Fixture.GetCustomer();

            repository.Create(customer);

            customer.FirstName = "new first name";

            Assert.False(repository.Update(customer));
        }

        [Fact]
        public void ShouldDeleteReturnsFalseIfNoLinesAffected()
        {
            Fixture.DeleteAll();

            Repositories.CustomerRepository repository = Fixture.GetCustomerRepository();

            var customer = Fixture.GetCustomer();

            repository.Create(customer);

            customer.FirstName = "new first name";

            Assert.False(repository.Delete(customer.Id));
        }
    }
}