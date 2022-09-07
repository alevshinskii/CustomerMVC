using CustomerManagement.Entities;
using Moq;
using System;
using Xunit;

namespace CustomerManagement.Test.Services.CustomerService;

public class CustomerServiceTest
{
    private readonly CustomerServiceFixture _fixture = new();

    [Fact]
    public void ShouldBeAbleToCreateService()
    {
        var service = new CustomerManagement.Services.CustomerService();
        Assert.NotNull(service);
    }

    [Fact]
    public void ShouldBeAbleToGetCustomer()
    {
        var customer = _fixture.GetCustomer();

        var service = _fixture.GetCustomerService();

        var createdCustomer = service.Get(customer.Id);

        Assert.NotNull(createdCustomer);
    }

    [Fact]
    public void ShouldBeAbleToCreateCustomer()
    {
        var customer = _fixture.GetCustomer();

        var service = _fixture.GetCustomerService();

        var createdCustomer = service.Create(customer);

        Assert.NotNull(createdCustomer);
    }

    [Fact]
    public void ShouldBeAbleToUpdateCustomer()
    {
        var customer = _fixture.GetCustomer();

        var service = _fixture.GetCustomerService();

        Assert.True(service.Update(customer));
    }

    [Fact]
    public void ShouldBeAbleToDeleteCustomer()
    {
        var customer = _fixture.GetCustomer();

        var service = _fixture.GetCustomerService();

        Assert.True(service.Delete(customer.Id));
    }

    [Fact]
    public void ShouldBeAbleToGetAllCustomers()
    {
        var customer = _fixture.GetCustomer();

        var service = _fixture.GetCustomerService();

        var customers = service.GetAll();

        Assert.NotEmpty(customers);
    }

    [Fact]
    public void ShouldThrowExceptionWhenArgumentOutOfRangeInGetCustomer()
    {
        var service = _fixture.GetCustomerService();

        Assert.Throws<ArgumentOutOfRangeException>(() => service.Get(-1));
    }

    [Fact]
    public void ShouldGetActionReturnNullIfCanNotFindCustomerInRepo()
    {
        var customerRepoMock = _fixture.GetCustomerRepositoryMock();
        Customer? customerNull = null;
        customerRepoMock.Setup(x => x.Read(It.IsAny<int>())).Returns(customerNull);

        var service = _fixture.GetCustomerService(customerRepoMock.Object);

        Assert.Null(service.Get(1));
    }

    [Fact]
    public void ShouldServiceThrowExceptionIfThereNoCustomerToUpdate()
    {
        var customerRepoMock = _fixture.GetCustomerRepositoryMock();
        Customer? customerNull = null;
        customerRepoMock.Setup(x => x.Read(It.IsAny<int>())).Returns(customerNull);

        var service = _fixture.GetCustomerService(customerRepoMock.Object);

        Assert.Throws<InvalidOperationException>(() => service.Update(_fixture.GetCustomer()));
    }

    [Fact]
    public void ShouldThrowExceptionWhenCanNotUpdateAddress()
    {
        var addressRepositoryMock = _fixture.GetAddressRepositoryMock();
        addressRepositoryMock.Setup(x => x.Update(It.IsAny<Address>())).Returns(false);

        var service = _fixture.GetCustomerService(null, addressRepositoryMock.Object);

        Assert.Throws<InvalidOperationException>(() => service.Update(_fixture.GetCustomer()));
    }

    [Fact]
    public void ShouldThrowExceptionWhenCanNotUpdateNote()
    {
        var noteRepositoryMock = _fixture.GetNoteRepositoryMock();
        noteRepositoryMock.Setup(x => x.Update(It.IsAny<Note>())).Returns(false);

        var service = _fixture.GetCustomerService(null, null, noteRepositoryMock.Object);

        Assert.Throws<InvalidOperationException>(() => service.Update(_fixture.GetCustomer()));
    }

    [Fact]
    public void ShouldServiceThrowExceptionIfThereNoCustomerToDelete()
    {
        var customerRepoMock = _fixture.GetCustomerRepositoryMock();
        Customer? customerNull = null;
        customerRepoMock.Setup(x => x.Read(It.IsAny<int>())).Returns(customerNull);

        var service = _fixture.GetCustomerService(customerRepoMock.Object);

        Assert.Throws<InvalidOperationException>(() => service.Delete(_fixture.GetCustomer().Id));
    }
}
