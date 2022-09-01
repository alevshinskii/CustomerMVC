using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CustomerManagement.Repositories
{

    public class AddressRepository : BaseRepository, IRepository<Address>
    {
        public Address Create(Address entity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [Address] (AddressLine,AddressLine2,AddressType,City,Country,CustomerId,PostalCode,State) VALUES" +
                                             "(@AddressLine,@AddressLine2,@AddressType,@City,@Country,@CustomerId,@PostalCode,@State)", connection);

                var idParameter = new SqlParameter("@AddressId", SqlDbType.Int)
                {
                    Value = entity.AddressId
                };
                var addressLineParameter = new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100)
                {
                    Value = entity.AddressLine
                };
                var addressLine2Parameter = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100)
                {
                    Value = entity.AddressLine2
                };
                var addressTypeParameter = new SqlParameter("@AddressType", SqlDbType.NVarChar, 20)
                {
                    Value = entity.AddressType
                };
                var cityParameter = new SqlParameter("@City", SqlDbType.NVarChar, 50)
                {
                    Value = entity.City
                };
                var countryParameter = new SqlParameter("@Country", SqlDbType.NVarChar, 100)
                {
                    Value = entity.Country
                };
                var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int)
                {
                    Value = entity.CustomerId
                };
                var postalCodeParameter = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 6)
                {
                    Value = entity.PostalCode
                };
                var stateParameter = new SqlParameter("@State", SqlDbType.NVarChar, 20)
                {
                    Value = entity.State
                };

                command.Parameters.Add(idParameter);
                command.Parameters.Add(addressLineParameter);
                command.Parameters.Add(addressLine2Parameter);
                command.Parameters.Add(addressTypeParameter);
                command.Parameters.Add(cityParameter);
                command.Parameters.Add(countryParameter);
                command.Parameters.Add(customerIdParameter);
                command.Parameters.Add(postalCodeParameter);
                command.Parameters.Add(stateParameter);

                command.ExecuteNonQuery();

                var commandScope = new SqlCommand("SELECT IDENT_CURRENT('Address') as Id", connection);
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

        public Address Read(int entityId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Address WHERE AddressId = @Id", connection);

                var idParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = entityId
                };

                command.Parameters.Add(idParam);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Address()
                        {
                            AddressId = int.Parse(reader["AddressId"].ToString()),
                            AddressLine = reader["AddressLine"].ToString(),
                            AddressLine2 = reader["AddressLine2"].ToString(),
                            AddressType = reader["AddressType"].ToString(),
                            CustomerId = int.Parse(reader["CustomerId"].ToString()),
                            City = reader["City"].ToString(),
                            Country = reader["Country"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            State = reader["State"].ToString(),
                        };
                    }
                    return null;

                }

            }

        }

        public List<Address> ReadAll()
        {
            var addressList = new List<Address>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Address", connection);
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var address = new Address();

                        address.AddressId = int.Parse(reader["AddressId"].ToString());
                        address.AddressLine = reader["AddressLine"].ToString();
                        address.AddressLine2 = reader["AddressLine2"].ToString();
                        address.AddressType = reader["AddressType"].ToString();
                        address.CustomerId = int.Parse(reader["CustomerId"].ToString());
                        address.City = reader["City"].ToString();
                        address.Country = reader["Country"].ToString();
                        address.PostalCode = reader["PostalCode"].ToString();
                        address.State = reader["State"].ToString();

                        addressList.Add(address);
                    }
                }

            }
            return addressList;
        }

        public List<Address> ReadAll(int customerId)
        {
            var addressList = new List<Address>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Address WHERE CustomerId=@CustomerId", connection);

                var idParam = new SqlParameter("@CustomerId", SqlDbType.Int)
                {
                    Value = customerId
                };

                command.Parameters.Add(idParam);
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var address = new Address();

                        address.AddressId = int.Parse(reader["AddressId"].ToString());
                        address.AddressLine = reader["AddressLine"].ToString();
                        address.AddressLine2 = reader["AddressLine2"].ToString();
                        address.AddressType = reader["AddressType"].ToString();
                        address.CustomerId = int.Parse(reader["CustomerId"].ToString());
                        address.City = reader["City"].ToString();
                        address.Country = reader["Country"].ToString();
                        address.PostalCode = reader["PostalCode"].ToString();
                        address.State = reader["State"].ToString();

                        addressList.Add(address);
                    }
                }

            }
            return addressList;
        }

        public bool Update(Address entity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Address SET CustomerId=@CustomerId, AddressLine=@AddressLine, " +
                                             "AddressLine2=@AddressLine2, AddressType=@AddressType, City=@City, " +
                                             "Country=@Country, PostalCode=@PostalCode, State=@State" +
                                             " WHERE AddressId = @AddressId", connection);

                var idParameter = new SqlParameter("@AddressId", SqlDbType.Int)
                {
                    Value = entity.AddressId
                };
                var addressLineParameter = new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100)
                {
                    Value = entity.AddressLine
                };
                var addressLine2Parameter = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100)
                {
                    Value = entity.AddressLine2
                };
                var addressTypeParameter = new SqlParameter("@AddressType", SqlDbType.NVarChar, 20)
                {
                    Value = entity.AddressType
                };
                var cityParameter = new SqlParameter("@City", SqlDbType.NVarChar, 50)
                {
                    Value = entity.City
                };
                var countryParameter = new SqlParameter("@Country", SqlDbType.NVarChar, 100)
                {
                    Value = entity.Country
                };
                var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int)
                {
                    Value = entity.CustomerId
                };
                var postalCodeParameter = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 6)
                {
                    Value = entity.PostalCode
                };
                var stateParameter = new SqlParameter("@State", SqlDbType.NVarChar, 20)
                {
                    Value = entity.State
                };

                command.Parameters.Add(idParameter);
                command.Parameters.Add(addressLineParameter);
                command.Parameters.Add(addressLine2Parameter);
                command.Parameters.Add(addressTypeParameter);
                command.Parameters.Add(cityParameter);
                command.Parameters.Add(countryParameter);
                command.Parameters.Add(customerIdParameter);
                command.Parameters.Add(postalCodeParameter);
                command.Parameters.Add(stateParameter);

                if (command.ExecuteNonQuery() > 0) return true;
                else return false;
            }

        }

        public bool Delete(int entityId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Address WHERE AddressId = @Id", connection);

                var idParameter = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = entityId
                };

                command.Parameters.Add(idParameter);

                if (command.ExecuteNonQuery() > 0) return true;
                else return false;
                
            }
        }
    }
}
