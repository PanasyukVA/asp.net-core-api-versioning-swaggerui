using AH.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AH.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/sportforecasts")]
    public class SportForecastController : ControllerBase
    {
        private static readonly List<Tuple<int, string, int>> Summaries = new List<Tuple<int, string, int>>
        {
            new Tuple<int, string, int>(1, "Win", 3),
            new Tuple<int, string, int>(2, "Draw", 1),
            new Tuple<int, string, int>(3, "Loss", 0)
        };

        private readonly ILogger<SportForecastController> _logger;

        public SportForecastController(ILogger<SportForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public SportForecast Get(int id)
        {
            var summary = Summaries.FirstOrDefault(summary => summary.Item1 == id);
            return new SportForecast() { Id = summary.Item1, Summary = summary.Item2, Points = summary.Item3 };
        }

        [HttpGet]
        public ICollection<SportForecast> Get(int? points)
        {
            var forecasts = Summaries
                .Where(summary => summary.Item3 == (points ?? summary.Item3))
                .Select(summary => new SportForecast() 
            { 
                Id = summary.Item1, 
                Summary = summary.Item2,
                Points = summary.Item3
            });
            return forecasts.ToArray();
        }

        [HttpPost]
        public void Create(SportForecast forecast)
        {
            var summary = new Tuple<int, string, int>(forecast.Id, forecast.Summary, forecast.Points);
            Summaries.Add(summary);
        }

        [HttpPut("{id}")]
        public void Update(int id, SportForecast forecast)
        {
            Summaries.RemoveAll(summary => summary.Item1 == id);
            
            var summary = new Tuple<int, string, int>(forecast.Id, forecast.Summary, forecast.Points);
            Summaries.Add(summary);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Summaries.RemoveAll(summary => summary.Item1 == id);
        }
    }
}
