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
        public IEnumerable<WordColor> Get()
        {
            return _dataContext.WordColors.ToArray();
        }
        [HttpGet("{id}")]
        public WordColor Get(int id)
        {
            return _dataContext.WordColors.FirstOrDefault(wc => wc.Id == id);
        }
        [HttpGet("after/{id}"), ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<WordColor> GetNew(int id)
        {
            return _dataContext.WordColors.Where(w => w.Id > id).ToArray();
        }
        [HttpPost]
        public async Task<WordColor> Post([FromBody] WordColor wc) {
            WordColor wordColor = _dataContext.AddWord(new WordColor
            {
                Word = wc.Word,
                Color = wc.Color
            });
            // await _hubContext.Clients.All.SendAsync("ReceiveAddMessage", Convert.ToString(wordColor.Id));
            await _hubContext.Clients.All.SendAsync("ReceiveAddMessage", wordColor);
            return wordColor;
        } 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id){
            WordColor wc = _dataContext.WordColors.Find(id);
            if (wc == null){
                return NotFound();
            }
            _dataContext.DeleteWord(id);
            await _hubContext.Clients.All.SendAsync("ReceiveDeleteMessage", Convert.ToString(id));
            return NoContent();
        } 
    }
}
