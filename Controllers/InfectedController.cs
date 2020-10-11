using api_coronavirus.Data.Collections;
using api_coronavirus.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api_coronavirus.Controllers
{
    [ApiController]             // Controller api
    [Route("[controller]")]     // route by controller name
    public class InfectedController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infected> _infectedsCollection;

        // dependency injection
        public InfectedController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectedsCollection = _mongoDB.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }

        // create an endpoint of type HTTP post (using to INSERT things in bank)
        // rest notation
        [HttpPost]
        public ActionResult SaveInfected([FromBody] InfectedDto dto)
        {
            var infected = new Infected(dto.BirthDate, dto.Gender, dto.Latitude, dto.Longitude);
           
            _infectedsCollection.InsertOne(infected);
            
            return StatusCode(201, "Infected sucessfully included");
            // http statuscode 201 created
        }

        // create an endpoint of type HTTP get (using to GET things in bank)
        // rest notation
        [HttpGet]
        public ActionResult GetInfected()
        {
            var infected = _infectedsCollection.Find(Builders<Infected>.Filter.Empty).ToList();
            
            return Ok(infected);
            // http statuscode 200 OK
        }
    }
}