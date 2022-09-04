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
        public void ShouldDisplayMessageIfCanNotCreateCustomer()
        {
            Customer? customer = new Customer()
            {
                LastName = "LastName"
            };

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Create(customer)).Returns(customer = null);

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Create(customer);

            customerRepoMock.Verify(x => x.Create(customer), Times.Once);

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
            Assert.Equal("Can't add new customer to database", viewResult?.ViewBag.Message);
        }

        /*        [Fact]
                public void ShouldNotBeAbleToCreateCustomerIfModelIsInvalid()
                {
                    Customer? customer = new Customer()
                    {
                        FirstName = new Faker().Random.String(51),
                        LastName = new Faker().Random.String(51)
                    };

                    var customerRepoMock = new Mock<IRepository<Customer>>();
                    customerRepoMock.Setup(x => x.Create(customer)).Returns(customer);

                    var controller = new CustomerController(customerRepoMock.Object);
                    controller.ViewData.Model = customer;
                    var result = controller.Create(customer);
                    var viewResult = result as ViewResult;
                    Assert.True(controller.ModelState.IsValid);

                    customerRepoMock.Verify(x => x.Create(customer), Times.Never);

                    Assert.NotNull(viewResult);

                    Assert.Equal("Customer entity is not valid", viewResult?.ViewBag.Message);
                }*/

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
                Id = 1,
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

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Edit(customer.Id);
            var notFoundResult = result as HttpNotFoundResult;

            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void ShouldShowMessageIfCanNotUpdateCustomer()
        {
            Customer customer = new Customer()
            {
                Id = 1,
                LastName = "LastName"
            };
            Customer? customerNull = null;

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Update(customer)).Returns(false);

            var controller = new CustomerController(customerRepoMock.Object);
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

            var controller = new CustomerController(customerRepoMock.Object);
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

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Delete(customer.Id);
            var notFoundResult = result as HttpNotFoundResult;

            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void ShouldShowMessageIfCanNotDeleteCustomer()
        {
            Customer customer = new Customer()
            {
                Id = 1,
                LastName = "LastName"
            };
            Customer? customerNull = null;

            var customerRepoMock = new Mock<IRepository<Customer>>();
            customerRepoMock.Setup(x => x.Delete(customer.Id)).Returns(false);

            var controller = new CustomerController(customerRepoMock.Object);
            var result = controller.Delete(customer);

            var view = result as ViewResult;
            Assert.NotNull(view);

            Assert.Equal("Can't delete customer from database", view?.ViewBag.Message);
        }
    }
}