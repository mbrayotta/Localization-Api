using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.Configuration;

namespace Persistence.Repository
{
    public class AddCoordinatesRepository : IAddCoordinatesRepository
    {
        private readonly string _connectionString;

        public AddCoordinatesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task InsertCoordinates(GeocodedAddress geocodedAddress)
        {
            using(SqlConnection sqlConnec = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = new SqlCommand("InsertGeocodedAddress",sqlConnec))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", geocodedAddress.Id));
                    cmd.Parameters.Add(new SqlParameter("@latitude", geocodedAddress.Latitude));
                    cmd.Parameters.Add(new SqlParameter("@longitude", geocodedAddress.Longitude));

                    await sqlConnec.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        
    }
}