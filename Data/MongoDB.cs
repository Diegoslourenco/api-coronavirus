using System;
using api_coronavirus.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace api_coronavirus.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; }

        // Everything that references to configuration is added here
        public MongoDB(IConfiguration configuration)
        {
            // try to connect with MongoDB
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["BankName"]); // coronavirus
                MapClasses();

            }
            catch (Exception exception)
            {
                throw new MongoException("It was not possible to connect to MongoDB", exception);
            }
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infected)))
            {
                BsonClassMap.RegisterClassMap<Infected>(i =>
                {
                    i.AutoMap(); // All proprierties can have the same name and type inside the bank
                    i.SetIgnoreExtraElements(true); // ignore extra elements
                });
            }
        }
    }
}