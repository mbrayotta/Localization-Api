using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Persistence
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectionString;
        public AddressRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Insert(Address addres){
            
            using(SqlConnection sqlConnec = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = new SqlCommand("InsertAddress",sqlConnec))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@calle", addres.Calle));
                    cmd.Parameters.Add(new SqlParameter("@numero", addres.Numero));
                    cmd.Parameters.Add(new SqlParameter("@ciudad", addres.Ciudad));
                    cmd.Parameters.Add(new SqlParameter("@codigo_postal", addres.CodigoPostal));
                    cmd.Parameters.Add(new SqlParameter("@provincia", addres.Provincia));
                    cmd.Parameters.Add(new SqlParameter("@pais", addres.Pais));

                    await sqlConnec.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    }
}