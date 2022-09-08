using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace CustomerManagement.Repositories
{
    public class CustomerRepository : BaseRepository, IRepository<Customer>
    {
        public Customer? Create(Customer entity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [Customer] (FirstName,LastName,PhoneNumber,Email,TotalPurchasesAmount) VALUES" +
                                             "(@FirstName, @LastName, @PhoneNumber, @Email, @TotalPurchasesAmount)", connection);

                var firstNameParameter = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
                {
                    Value = entity.FirstName ?? SqlString.Null,
                    IsNullable = true
                };
                var lastNameParameter = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
                {
                    Value = entity.LastName
                };
                var phoneNumberParameter = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15)
                {
                    Value = entity.PhoneNumber ?? SqlString.Null,
                    IsNullable = true
                };
                var emailParameter = new SqlParameter("@Email", SqlDbType.NVarChar, 255)
                {
                    Value = entity.Email ?? SqlString.Null,
                    IsNullable = true
                };
                var totalPurchasesAmountParameter = new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money)
                {
                    Value = entity.TotalPurchasesAmount ?? SqlMoney.Null,
                    IsNullable = true
                };

                command.Parameters.Add(firstNameParameter);
                command.Parameters.Add(lastNameParameter);
                command.Parameters.Add(phoneNumberParameter);
                command.Parameters.Add(emailParameter);
                command.Parameters.Add(totalPurchasesAmountParameter);

                command.ExecuteNonQuery();

                var commandScope = new SqlCommand("SELECT IDENT_CURRENT('Customer') as Id", connection);
                using (var reader = commandScope.ExecuteReader())
                {
                    int idOfCreatedEntity = 0;
                    if (reader.Read())
                    {
                        idOfCreatedEntity = int.Parse(reader["Id"].ToString() ?? string.Empty);
                    }
                    return Read(idOfCreatedEntity);
                }

            }

        }

        public Customer? Read(int entityId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = @Id", connection);

                var idParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = entityId
                };

                command.Parameters.Add(idParam);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Customer customer= new Customer();

                        customer.Id = int.Parse(reader["CustomerId"]?.ToString());
                        customer.FirstName = reader["FirstName"]?.ToString();
                        customer.LastName = reader["LastName"].ToString();
                        customer.Email = reader["Email"]?.ToString();
                        customer.PhoneNumber = reader["PhoneNumber"]?.ToString();
                        if(decimal.TryParse(reader["TotalPurchasesAmount"]?.ToString(), out decimal total))
                            customer.TotalPurchasesAmount = total;

                        return customer;
                    }
                    return null;

                }
            }

        }

        public List<Customer> ReadAll()
        {
            var customerList = new List<Customer>();
            using var connection = GetConnection();
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Customer", connection);


            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var customer = new Customer();

                    customer.Id = int.Parse(reader["CustomerId"].ToString());
                    customer.FirstName = reader["FirstName"]?.ToString();
                    customer.LastName = reader["LastName"].ToString();
                    customer.Email = reader["Email"]?.ToString();
                    customer.PhoneNumber = reader["PhoneNumber"]?.ToString();
                    if (decimal.TryParse(reader["TotalPurchasesAmount"].ToString(), out decimal totalPurchasesAmount))
                        customer.TotalPurchasesAmount = totalPurchasesAmount;
                    else customer.TotalPurchasesAmount = null;

                    customerList.Add(customer);
                }

            }

            return customerList;

        }

        public List<Customer> ReadAll(int entityId)
        {
            throw new InvalidOperationException();
        }

        public bool Update(Customer entity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Customer SET FirstName=@FirstName, LastName=@LastName, Email=@Email, PhoneNumber=@PhoneNumber, TotalPurchasesAmount=@TotalPurchasesAmount" +
                                             " WHERE CustomerId = @Id", connection);

                var idParameter = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = entity.Id
                };
                var firstNameParameter = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
                {
                    Value = entity.FirstName ?? SqlString.Null
                };
                var lastNameParameter = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
                {
                    Value = entity.LastName 
                };
                var phoneNumberParameter = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15)
                {
                    Value = entity.PhoneNumber ?? SqlString.Null
                };
                var emailParameter = new SqlParameter("@Email", SqlDbType.NVarChar, 255)
                {
                    Value = entity.Email?? SqlString.Null
                };
                var totalPurchasesAmountParameter = new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money)
                {
                    Value = entity.TotalPurchasesAmount?? SqlMoney.Null
                };

                command.Parameters.Add(idParameter);
                command.Parameters.Add(firstNameParameter);
                command.Parameters.Add(lastNameParameter);
                command.Parameters.Add(phoneNumberParameter);
                command.Parameters.Add(emailParameter);
                command.Parameters.Add(totalPurchasesAmountParameter);


                if (command.ExecuteNonQuery() > 0) return true;
                else return false;
            }

        }

        public bool Delete(int entityId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var commandDeleteAddresses = new SqlCommand("DELETE FROM Address WHERE CustomerId = @Id", connection);
                var idParameter = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = entityId
                };
                commandDeleteAddresses.Parameters.Add(idParameter);
                commandDeleteAddresses.ExecuteNonQuery();

                var commandDeleteNotes = new SqlCommand("DELETE FROM Notes WHERE CustomerId = @Id", connection);
                idParameter = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = entityId
                };
                commandDeleteNotes.Parameters.Add(idParameter);
                commandDeleteNotes.ExecuteNonQuery();

                var commandDeleteCustomer = new SqlCommand("DELETE FROM Customer WHERE CustomerId = @Id", connection);
                idParameter = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = entityId
                };
                commandDeleteCustomer.Parameters.Add(idParameter);
                if (commandDeleteCustomer.ExecuteNonQuery() > 0) return true;
                else return false;
                
            }

        }

    }

}

