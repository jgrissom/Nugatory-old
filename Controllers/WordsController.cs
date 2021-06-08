using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WordApi.Models;

namespace WordApi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private DataContext _dataContext;

        public ApiController(ILogger<ApiController> logger, DataContext db)
        {
            _dataContext = db;
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<WordColor> Get()
        {
            return _dataContext.WordColors.OrderBy(c => c.TS).ToArray();
        }
        [HttpPost]
        public WordColor Post([FromBody] WordColor wc) => _dataContext.AddWord(new WordColor
        {
            Word = wc.Word,
            Color = wc.Color
        });
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id){
            WordColor wc = _dataContext.WordColors.Find(id);
            if (wc == null){
                return NotFound();
            }
            _dataContext.DeleteWord(id);
            return NoContent();
        } 
    }
}
