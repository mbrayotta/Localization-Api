using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Persistence
{
    public class GeocodedAddressRepository : IGeocodedAddress
    {
        private readonly string _connectionString;
        public GeocodedAddressRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<GeocodedAddress> GetById(int id)
        {
            using(SqlConnection sqlConnec = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = new SqlCommand("GetAddressById", sqlConnec))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    GeocodedAddress address = null;
                    await sqlConnec.OpenAsync();

                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            address = MapToGeocodedAddress(reader);
                        }
                        else
                        {
                            address = new GeocodedAddress()
                            {
                                State = "PROCESANDO"
                            };
                        }
                    }

                    return address;
                }
            }
        }

        private GeocodedAddress MapToGeocodedAddress(SqlDataReader reader)
        {
            return new GeocodedAddress()
            {
                Id = (int)reader["Id"],
                Longitude = (decimal)reader["Longitude"],
                Latitude = (decimal)reader["Latitude"],
                State = "TERMINADO"
            };
        }
    }
}