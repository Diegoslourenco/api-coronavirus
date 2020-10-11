using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace api_coronavirus.Data.Collections
{
    public class Infected
    {
        public Infected(DateTime birthDate, string gender, double latitude, double longitude)
        {
            this.BirthDate = birthDate;
            this.Gender = gender;
            this.Localization = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }

        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

        // Representacao da classe no banco do Mongo
        public GeoJson2DGeographicCoordinates Localization { get; set; }
    }
}