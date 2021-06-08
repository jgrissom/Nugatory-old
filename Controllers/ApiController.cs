using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WordApi.Models;

namespace WordApi.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private DataContext _dataContext;

        public ApiController(ILogger<ApiController> logger, DataContext db)
        {
            _dataContext = db;
            _logger = logger;
        }
        [HttpGet, Route("[controller]/wordcolor")]
        public IEnumerable<WordColor> Get()
        {
            return _dataContext.WordColors.OrderBy(c => c.TS).ToArray();
        }
    }
}
