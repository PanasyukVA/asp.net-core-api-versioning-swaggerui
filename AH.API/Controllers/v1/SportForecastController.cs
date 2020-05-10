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
        private static readonly List<Tuple<int, string>> Summaries = new List<Tuple<int, string>>
        {
            new Tuple<int, string>(1, "Win"),
            new Tuple<int, string>(2, "Draw"),
            new Tuple<int, string>(3, "Loss")
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
            return new SportForecast() { Id = summary.Item1, Summary = summary.Item2 };
        }

        [HttpGet]
        public ICollection<SportForecast> Get()
        {
            var forecasts = Summaries.Select(summary => new SportForecast() { Id = summary.Item1, Summary = summary.Item2 });
            return forecasts.ToArray();
        }

        [HttpPost]
        public void Create(SportForecast forecast)
        {
            var summary = new Tuple<int, string>(forecast.Id, forecast.Summary);
            Summaries.Add(summary);
        }

        [HttpPut]
        public void Update(SportForecast forecast)
        {
            var summary = new Tuple<int, string>(forecast.Id, forecast.Summary);
            Summaries.Remove(summary);
            Summaries.Add(summary);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Summaries.RemoveAll(summary => summary.Item1 == id);
        }
    }
}
