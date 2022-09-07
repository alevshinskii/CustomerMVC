using System.Data.SqlClient;
using CustomerManagement.Entities;

namespace CustomerManagement.Test.CustomerRepository
{
    
    public class CustomerRepositoryFixture
    {
        public void DeleteAll()
        {
        
            var repository = GetCustomerRepository();

            using (var connection = repository.GetConnection())
            {
                connection.Open();

                var commandClearAddress = new SqlCommand("DELETE FROM Address", connection);
                commandClearAddress.ExecuteNonQuery();

                var commandClearNotes = new SqlCommand("DELETE FROM Notes", connection);
                commandClearNotes.ExecuteNonQuery();

                var commandClearCustomer = new SqlCommand("DELETE FROM Customer", connection);
                commandClearCustomer.ExecuteNonQuery();


                connection.Close();
            }

        }

        public Customer GetCustomer()
        {
            return new Customer()
            {
                Id = 0,
                LastName = "LastName",
                FirstName = "FirstName",
                PhoneNumber = "42738947298347",
                Email = "email@email.com",
                TotalPurchasesAmount = 1000
            };
        }

        public CustomerTestRepository GetCustomerRepository()
        {
            return new CustomerTestRepository();
        }
    }
}
