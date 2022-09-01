using CustomerManagement.Entities;
using Xunit;

namespace CustomerManagement.Test.AddressRepository
{
    
public class AddressRepositoryTest
{
    private readonly AddressRepositoryFixture Fixture = new AddressRepositoryFixture();

    [Fact]
    public void ShouldBeAbleToCreateAddressRepo()
    {
        Repositories.AddressRepository repository = Fixture.GetAddressRepository();
        Assert.NotNull(repository);
    }

    [Fact]
    public void ShouldBeAbleToCreateAddress()
    {
        Fixture.DeleteAll();

        Repositories.AddressRepository repository = Fixture.GetAddressRepository();
        Address address = Fixture.GetAddress();

        var createdAddress = repository.Create(address);

        Assert.NotNull(createdAddress);
    }

    [Fact]
    public void ShouldBeAbleToReadAddress()
    {
        Fixture.DeleteAll();

        Repositories.AddressRepository repository = Fixture.GetAddressRepository();
        Address address = Fixture.GetAddress();

        var createdAddress = repository.Create(address);

        var readedAddress = repository.Read(createdAddress.AddressId);

        Assert.Equal(createdAddress.AddressId, readedAddress.AddressId);
        Assert.Equal(createdAddress.AddressLine, readedAddress.AddressLine);
        Assert.Equal(createdAddress.AddressLine2, readedAddress.AddressLine2);
        Assert.Equal(createdAddress.AddressType, readedAddress.AddressType);
        Assert.Equal(createdAddress.CustomerId, readedAddress.CustomerId);
        Assert.Equal(createdAddress.City, readedAddress.City);
        Assert.Equal(createdAddress.Country, readedAddress.Country);
        Assert.Equal(createdAddress.PostalCode, readedAddress.PostalCode);
        Assert.Equal(createdAddress.State, readedAddress.State);
    }

    [Fact]
    public void ShouldBeAbleToUpdateAddress()
    {
        Fixture.DeleteAll();

        Repositories.AddressRepository repository = Fixture.GetAddressRepository();
        Address address = Fixture.GetAddress();

        var createdAddress = repository.Create(address);
        createdAddress.AddressLine = "New Address Line";

        repository.Update(createdAddress);

        var updatedAddress = repository.Read(createdAddress.AddressId);

        Assert.NotEqual(address.AddressLine, updatedAddress.AddressLine);
        Assert.Equal(createdAddress.AddressLine, updatedAddress.AddressLine);
    }

    [Fact]
    public void ShouldBeAbleToDeleteAddress()
    {
        Fixture.DeleteAll();

        Repositories.AddressRepository repository = Fixture.GetAddressRepository();
        Address address = Fixture.GetAddress();

        var createdAddress = repository.Create(address);

        repository.Delete(createdAddress.AddressId);

        var readedAddress = repository.Read(createdAddress.AddressId);

        Assert.Null(readedAddress);
    }
}
}
