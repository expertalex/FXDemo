using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FXDemo.Data;
using FXDemo.Models;
using FXDemo.Contracts;
using FXDemo.Models.Http;

namespace FXDemo.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _service;

        public StatisticsController(IStatisticService service)
        {
            _service = service;
        }

        // GET: api/Statistics/yellowcards
        [HttpGet("yellowcards")]
        public async Task<ActionResult<IEnumerable<CardResponse>>> GetGeneralStatisiticYellowCardsResponce()
        {
            var responce = await _service.GetYellowCardsAsync();

            return Ok(responce);
        }

        // GET: api/Statistics/redcards
        [HttpGet("redcards")]
        public async Task<ActionResult<IEnumerable<CardResponse>>> GetGeneralStatisiticRedCardsResponce()
        {
            var responce = await _service.GetRedCardsAsync();

            return Ok(responce);
        }

        // GET: api/Statistics/minutes
        [HttpGet("minutes")]
        public async Task<ActionResult<IEnumerable<MinuteResponse>>> GetGeneralStatisiticMinutesResponce()
        {
            var responce = await _service.GetMinutesPlayAsync();

            return Ok(responce);
        }

    }
}
