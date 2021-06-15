using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WordApi.Models;
using Microsoft.AspNetCore.SignalR;
using WordApi.Hubs;
using Swashbuckle.AspNetCore.Annotations;

namespace WordApi.Controllers
{
    [ApiController, Route("[controller]/word")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private DataContext _dataContext;
        private readonly IHubContext<NugatoryHub> _hubContext;
        public ApiController(ILogger<ApiController> logger, DataContext db, IHubContext<NugatoryHub> hubContext)
        {
            _dataContext = db;
            _logger = logger;
            _hubContext = hubContext;
        }
        [HttpGet]
        [SwaggerOperation(summary: "returns all words", null)]
        public IEnumerable<WordColor> Get()
        {
            return _dataContext.WordColors.ToArray();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(summary: "returns specific word", null)]
        public async Task<ActionResult<WordColor>> Get(int id)
        {
            var word = await _dataContext.WordColors.FindAsync(id);
           if (word == null)
           {
               return NotFound();
           }
           return word;
        }

        [HttpPost]
        [SwaggerOperation(summary: "add word to collection", null)]
        [ProducesResponseType(typeof(WordColor), 201), SwaggerResponse(201, "Created")]
        public async Task<ActionResult<WordColor>> Post([FromBody] WordColor wordColor) {
            _dataContext.Add(wordColor);
            await _dataContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveAddMessage", wordColor);
            this.HttpContext.Response.StatusCode = 201;
            return wordColor;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(summary: "delete score from collection", null)]
        [ProducesResponseType(typeof(WordColor), 204), SwaggerResponse(204, "No Content")]
        public async Task<ActionResult> Delete(int id){
            var wordColor = await _dataContext.WordColors.FindAsync(id);
            if (wordColor == null){
                return NotFound();
            }

            _dataContext.WordColors.Remove(wordColor);
            await _dataContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveDeleteMessage", Convert.ToString(id));
            
            return NoContent();
        } 
    }
}
