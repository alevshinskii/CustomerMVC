using System.Data.SqlClient;
using CustomerManagement.Entities;
using CustomerManagement.Test.CustomerRepository;

namespace CustomerManagement.Test.NoteRepository
{
    public class NoteRepositoryFixture
    {
        private readonly CustomerRepositoryFixture customerFixture = new CustomerRepositoryFixture();
        public void DeleteAll()
        {
            customerFixture.DeleteAll();
        }

        public Note GetNote()
        {
            var customerRepository = customerFixture.GetCustomerRepository();
            var customer = customerRepository.Create(customerFixture.GetCustomer());

            return new Note()
            {
                Id = 1,
                CustomerId = customer.Id,
                Text = "Some text"
            };
        }

        public NoteTestRepository GetNoteRepository()
        {
            return new NoteTestRepository();
        }
    }
}

