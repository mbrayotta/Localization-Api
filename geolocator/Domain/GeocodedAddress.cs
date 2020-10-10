namespace Domain
{
    public class GeocodedAddress
    {
        public int Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Estado { get; set; }
    }
}