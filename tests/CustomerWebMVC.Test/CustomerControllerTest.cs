using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using CustomerWebMVC.Controllers;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CustomerManagement.Services;
using CustomerManagement.Test.Services.CustomerService;

namespace CustomerWebMVC.Test
{
    public class CustomerControllerTest
    {
        private readonly CustomerServiceFixture _fixture = new();
        [Fact]
        public void ShouldBeAbleToCreateCustomerController()
        {
            var controller = new CustomerController();
            Assert.NotNull(controller);
        }

        [Fact]
        public void ShouldBeAbleToGetCustomersList()
        {
            var controller = new CustomerController();
            var result = controller.Index(1);
            var resultView = result as ViewResult;
            var model = resultView?.Model as List<Customer>;

            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public void ShouldBeAbleToGetCustomersListWithInvalidPageNum()
        {
            var controller = new CustomerController();
            var result = controller.Index(-1);
            var resultView = result as ViewResult;
            var model = resultView?.Model as List<Customer>;

            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public void ShouldBeAbleToCallCustomerCreatePage()
        {
            var controller = new CustomerController();
            var result = controller.Create();
            var resultView = result as ViewResult;

            Assert.NotNull(resultView);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            Customer customer = new Customer()
            {
                LastName = "LastName"
            };

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Create(customer)).Returns(customer);

            var service = new CustomerService(customerRepoMock.Object);

            var controller = new CustomerController(service);
            var result = controller.Create(customer);

            var viewResult = result as RedirectToRouteResult;
            Assert.NotNull(viewResult);
            if (viewResult?.RouteValues.Values != null) Assert.Contains("Index", viewResult.RouteValues.Values);
        }

        [Fact]
        public void ShouldDisplayMessageIfCanNotCreateCustomer()
        {
            var customerService = new Mock<IService<Customer>>();
            customerService.Setup(x => x.Create(It.IsAny<Customer>())).Returns(_fixture.GetCustomerNull);
            var service = customerService.Object;

            var customer = new Customer();

            var controller = new CustomerController(service);

            var result = controller.Create(customer);

            customerService.Verify(x => x.Create(It.IsAny<Customer>()), Times.Once);

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
            Assert.Equal("Can't add new customer to database", viewResult?.ViewBag.Message);
        }

        [Fact]
        public void ShouldBeAbleToCallCustomerEditPage()
        {
            Customer customer = new Customer()
            {
                Id = 1,
                LastName = "LastName"
            };

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Read(customer.Id)).Returns(customer);

            var service = new CustomerService(customerRepoMock.Object);

            var controller = new CustomerController(service);
            var result = controller.Edit(customer.Id);
            var resultView = result as ViewResult;

            Assert.NotNull(resultView);
        }

        [Fact]
        public void ShouldBeAbleToEditCustomer()
        {
            var customerService = new Mock<IService<Customer>>();
            customerService.Setup(x => x.Update(It.IsAny<Customer>())).Returns(true);
            var service = customerService.Object;

            var customer = new Customer();

            var controller = new CustomerController(service);
            var result = controller.Edit(customer);

            customerService.Verify(x => x.Update(customer), Times.Once);

            var viewResult = result as RedirectToRouteResult;
            Assert.NotNull(viewResult);
            if (viewResult?.RouteValues.Values != null) Assert.Contains("Index", viewResult.RouteValues.Values);
        }


        [Fact]
        public void ShouldBeAbleToCallCustomerDeletePage()
        {
            Customer customer = new Customer()
            {
                Id = 1,
                LastName = "LastName"
            };

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Read(customer.Id)).Returns(customer);

            var service = new CustomerService(customerRepoMock.Object);

            var controller = new CustomerController(service);
            var result = controller.Delete(customer.Id);
            var resultView = result as ViewResult;

            Assert.NotNull(resultView);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            var customer = _fixture.GetCustomer();

            var customerService = new Mock<IService<Customer>>();
            customerService.Setup(x => x.Delete(It.IsAny<int>())).Returns(true);
            var service = customerService.Object;

            var controller = new CustomerController(service);
            var result = controller.Delete(customer);

            customerService.Verify(x => x.Delete(customer.Id), Times.Once);

            var viewResult = result as RedirectToRouteResult;
            Assert.NotNull(viewResult);
            if (viewResult?.RouteValues.Values != null) Assert.Contains("Index", viewResult.RouteValues.Values);
        }

        [Fact]
        public void ShouldBeAbleToCallCustomerDetailsPage()
        {
            Customer customer = new Customer()
            {
                Id = 1,
                LastName = "LastName"
            };

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Read(customer.Id)).Returns(customer);

            var service = new CustomerService(customerRepoMock.Object);

            var controller = new CustomerController(service);
            var result = controller.Details(customer.Id);
            var resultView = result as ViewResult;

            Assert.NotNull(resultView);
        }

        [Fact]
        public void ShouldBeAbleToRedirectFromEditToNotFoundPage()
        {
            Customer customer = new Customer()
            {
                Id = 1,
                LastName = "LastName"
            };
            Customer? customerNull = null;

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Read(customer.Id)).Returns(customerNull);

            var service = new CustomerService(customerRepoMock.Object);

            var controller = new CustomerController(service);
            var result = controller.Edit(customer.Id);
            var notFoundResult = result as HttpNotFoundResult;

            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void ShouldShowMessageIfCanNotUpdateCustomer()
        {
            var customer = _fixture.GetCustomer();

            var customerService = new Mock<IService<Customer>>();
            customerService.Setup(x => x.Update(It.IsAny<Customer>())).Returns(false);
            var service = customerService.Object;

            var controller = new CustomerController(service);
            var result = controller.Edit(customer);

            var view = result as ViewResult;
            Assert.NotNull(view);

            Assert.Equal("Can't update customer in database", view?.ViewBag.Message);
        }

        [Fact]
        public void ShouldBeAbleToRedirectFromDetailsToNotFoundPage()
        {
            Customer customer = new Customer()
            {
                Id = 1,
                LastName = "LastName"
            };
            Customer? customerNull = null;

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Read(customer.Id)).Returns(customerNull);

            var service = new CustomerService(customerRepoMock.Object);

            var controller = new CustomerController(service);
            var result = controller.Details(customer.Id);
            var notFoundResult = result as HttpNotFoundResult;

            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void ShouldBeAbleToRedirectFromDeleteToNotFoundPage()
        {
            Customer customer = new Customer()
            {
                Id = 1,
                LastName = "LastName"
            };
            Customer? customerNull = null;

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Read(customer.Id)).Returns(customerNull);

            var service = new CustomerService(customerRepoMock.Object);

            var controller = new CustomerController(service);
            var result = controller.Delete(customer.Id);
            var notFoundResult = result as HttpNotFoundResult;

            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void ShouldShowMessageIfCanNotDeleteCustomer()
        {
            var customer = _fixture.GetCustomer();

            var customerService = new Mock<IService<Customer>>();
            customerService.Setup(x => x.Delete(It.IsAny<int>())).Returns(false);

            var service = customerService.Object;

            var controller = new CustomerController(service);
            var result = controller.Delete(customer);

            var view = result as ViewResult;
            Assert.NotNull(view);

            Assert.Equal("Can't delete customer from database", view?.ViewBag.Message);
        }

        [Fact]
        public void ShouldCreateReturnViewIfModelIsNotValid()
        {
            var controller = new CustomerController();
            controller.ModelState.AddModelError("key", "message");

            var result = controller.Create(new Customer());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ShouldCreateReturnMessageIfModelIsNotValid()
        {
            var controller = new CustomerController();
            controller.ModelState.AddModelError("key", "message");

            controller.Create(new Customer());

            Assert.Equal("Customer entity is not valid", controller.ViewBag.Message);
        }

        [Fact]
        public void ShouldValidateCustomerModel()
        {
            var customer = new Customer()
            {
                LastName = "LastName"
            };
            var context = new ValidationContext(customer, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(customer, context, results, true);

            Assert.True(isModelStateValid);
        }

        [Fact]
        public void ShouldNotValidateInvalidCustomerModel()
        {
            var customer = new Customer();
            var context = new ValidationContext(customer, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(customer, context, results, true);

            Assert.False(isModelStateValid);
        }

        [Fact]
        public void ShouldEditReturnViewIfModelIsNotValid()
        {
            var controller = new CustomerController();
            controller.ModelState.AddModelError("key", "message");

            var result = controller.Edit(new Customer());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ShouldEditReturnMessageIfModelIsNotValid()
        {
            var controller = new CustomerController();
            controller.ModelState.AddModelError("key", "message");

            controller.Edit(new Customer());

            Assert.Equal("Customer entity is not valid", controller.ViewBag.Message);
        }
    }
}