using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.Core.Resource;
using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using CustomerWebMVC.Controllers;
using Moq;

namespace CustomerWebMVC.Test
{
    public class AddressControllerTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressController()
        {
            var controller = new AddressController();
            Assert.NotNull(controller);
        }

        [Fact]
        public void ShouldBeAbleToGetAddressList()
        {
            int customerId = 0;
            var addressList = new List<Address>() { new Address(), new Address() };

            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.ReadAll(customerId)).Returns(addressList);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Index(0);
            var view = result as ViewResult;
            Assert.NotNull(view);   

            addressRepoMock.Verify(x=>x.ReadAll(It.IsAny<int>()));

            var model = view?.Model as List<Address>;
            Assert.Equal(addressList.Count,model?.Count);
        }

        [Fact]
        public void ShouldBeAbleToCallAddressCreatePage()
        { 
            int customerId = 0;

            var controller = new AddressController();
            var result = controller.Create(customerId);
            var view = result as ViewResult;

            Assert.NotNull(view);

            var model = view?.Model as Address;

            Assert.NotNull(model);

            Assert.Equal(customerId,model?.CustomerId);
        }

        
        [Fact]
        public void ShouldBeAbleToCallAddressEditPage()
        {
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};
            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Read(address.AddressId)).Returns(address);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Edit(address.AddressId);
            var view = result as ViewResult;

            Assert.NotNull(view);

            var model = view?.Model as Address;

            Assert.NotNull(model);

            Assert.Equal(address.AddressId,model?.AddressId);
        }  
        
        [Fact]
        public void ShouldBeAbleToCallAddressDeletePage()
        { 
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};
            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Read(address.AddressId)).Returns(address);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Delete(address.AddressId);
            var view = result as ViewResult;

            Assert.NotNull(view);

            var model = view?.Model as Address;

            Assert.NotNull(model);

            Assert.Equal(address.AddressId,model?.AddressId);
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        { 
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};
            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Create(address)).Returns(address);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Create(address);

            addressRepoMock.Verify(x=>x.Create(address),Times.Once);

            var redirect = result as RedirectToRouteResult;

            Assert.NotNull(redirect);
            if (redirect != null) Assert.Contains("Index", redirect.RouteValues.Values);
        }

        [Fact]
        public void ShouldBeAbleToEditAddress()
        { 
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};
            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Update(address)).Returns(true);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Edit(address);

            addressRepoMock.Verify(x=>x.Update(address),Times.Once);

            var redirect = result as RedirectToRouteResult;

            Assert.NotNull(redirect);
            if (redirect != null) Assert.Contains("Index", redirect.RouteValues.Values);
        }

        [Fact]
        public void ShouldBeAbleToDeleteAddress()
        { 
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};

            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Delete(address.AddressId)).Returns(true);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Delete(address);

            addressRepoMock.Verify(x=>x.Delete(address.AddressId),Times.Once);

            var redirect = result as RedirectToRouteResult;

            Assert.NotNull(redirect);
            if (redirect != null) Assert.Contains("Index", redirect.RouteValues.Values);
        }

        [Fact]
        public void ShouldNotBeAbleToCreateAddressIfRepoReturnsNull()
        { 
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};
            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Create(address)).Returns(address=null);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Create(address);

            addressRepoMock.Verify(x=>x.Create(address),Times.Once);

            var view = result as ViewResult;
            Assert.NotNull(view);

            var model = view?.Model as Address;
            Assert.Equal(address,model);
        }

        [Fact]
        public void ShouldNotBeAbleToEditAddressIfRepoReturnsFalse()
        { 
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};
            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Update(address)).Returns(false);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Edit(address);

            addressRepoMock.Verify(x=>x.Update(address),Times.Once);

            var view = result as ViewResult;
            Assert.NotNull(view);

            var model = view?.Model as Address;
            Assert.Equal(address,model);
        }

        [Fact]
        public void ShouldNotBeAbleToDeleteAddressIfRepoReturnsFalse()
        { 
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};
            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Delete(address.AddressId)).Returns(false);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Delete(address);

            addressRepoMock.Verify(x=>x.Delete(address.AddressId),Times.Once);

            var view = result as ViewResult;
            Assert.NotNull(view);

            var model = view?.Model as Address;
            Assert.Equal(address,model);
        }

        [Fact]
        public void ShouldNotBeAbleToRedirectFromEditToNotFound()
        { 
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};
            Address? addressNull = null;
            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Read(address.AddressId)).Returns(addressNull);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Edit(address.AddressId);

            addressRepoMock.Verify(x=>x.Read(address.AddressId),Times.Once);

            var view = result as HttpNotFoundResult;
            Assert.NotNull(view);
        }

        [Fact]
        public void ShouldNotBeAbleToRedirectFromDeleteToNotFound()
        { 
            var address = new Address() { AddressId = 0 , AddressLine = "AddressLine"};
            Address? addressNull = null;
            var addressRepoMock = new Mock<IRepository<Address>>();
            addressRepoMock.Setup(x => x.Read(address.AddressId)).Returns(addressNull);

            var controller = new AddressController(addressRepoMock.Object);
            var result = controller.Delete(address.AddressId);

            addressRepoMock.Verify(x=>x.Read(address.AddressId),Times.Once);

            var view = result as HttpNotFoundResult;
            Assert.NotNull(view);
        }

        [Fact]
        public void ShouldValidateAddressModel()
        {
            var address = new Address()
            {
                AddressLine = "AddressLine"
            };
            var context = new ValidationContext(address, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(address, context, results, true);

            Assert.True(isModelStateValid);
        }

        [Fact]
        public void ShouldNotValidateInvalidAddressModel()
        {
            var address = new Address();
            var context = new ValidationContext(address, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(address, context, results, true);

            Assert.False(isModelStateValid);
        }

        [Fact]
        public void ShouldCreateReturnViewIfModelIsNotValid()
        {
            var controller = new AddressController();
            controller.ModelState.AddModelError("key", "message");

            var result = controller.Create(new Address());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ShouldCreateReturnMessageIfModelIsNotValid()
        {
            var controller = new AddressController();
            controller.ModelState.AddModelError("key", "message");

            controller.Create(new Address());

            Assert.Equal("Address is not valid", controller.ViewBag.Message);
        }

        [Fact]
        public void ShouldEditReturnViewIfModelIsNotValid()
        {
            var controller = new AddressController();
            controller.ModelState.AddModelError("key", "message");

            var result = controller.Edit(new Address());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ShouldEditReturnMessageIfModelIsNotValid()
        {
            var controller = new AddressController();
            controller.ModelState.AddModelError("key", "message");

            controller.Edit(new Address());

            Assert.Equal("Address is not valid", controller.ViewBag.Message);
        }
    }
}
