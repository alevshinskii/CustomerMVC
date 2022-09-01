using System.Data.SqlClient;

namespace CustomerManagement.Repositories
{
    public abstract class BaseRepository
    {
        public virtual SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectonString());
        }

        public virtual string GetConnectonString()
        {
            return "Server=localhost\\sqlexpress;Database=CustomerLib_Levshinskii;Trusted_Connection=true;";
        }
    }
}

