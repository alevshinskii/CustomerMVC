using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using CustomerWebMVC.Controllers;
using Moq;
using System.Web.Mvc;

namespace CustomerWebMVC.Test
{
    public class CustomerControllerTest
    {
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
            var result = controller.Index();
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

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Create(customer);

            customerRepoMock.Verify(x => x.Create(customer), Times.Once);

            var viewResult = result as RedirectToRouteResult;
            Assert.NotNull(viewResult);
            if (viewResult?.RouteValues.Values != null) Assert.Contains("Index", viewResult.RouteValues.Values);
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

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Edit(customer.Id);
            var resultView = result as ViewResult;

            Assert.NotNull(resultView);
        }

        [Fact]
        public void ShouldBeAbleToEditCustomer()
        {
            Customer customer = new Customer()
            {
                LastName = "LastName"
            };

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Update(customer)).Returns(true);

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Edit(customer);

            customerRepoMock.Verify(x => x.Update(customer), Times.Once);

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

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Delete(customer.Id);
            var resultView = result as ViewResult;

            Assert.NotNull(resultView);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            Customer customer = new Customer()
            {
                Id=1,
                LastName = "LastName"
            };

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Delete(customer.Id)).Returns(true);

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Delete(customer);

            customerRepoMock.Verify(x => x.Delete(customer.Id), Times.Once);

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

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Details(customer.Id);
            var resultView = result as ViewResult;

            Assert.NotNull(resultView);
        }
    }
}