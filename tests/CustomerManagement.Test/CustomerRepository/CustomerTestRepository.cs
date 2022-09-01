using System.Data.SqlClient;

namespace CustomerManagement.Test.CustomerRepository;

public class CustomerTestRepository:Repositories.CustomerRepository
{
    public override SqlConnection GetConnection()
    {
        return new SqlConnection(GetConnectonString());
    }
    public override string GetConnectonString()
    {
        return "Server=localhost\\sqlexpress;Database=CustomerLib_Levshinskii_Test;Trusted_Connection=true;";
    }
}