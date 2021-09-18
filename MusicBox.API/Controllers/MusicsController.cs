using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using MusicBox.Business.Abstract;
using MusicBox.Model.Concrete;

namespace MusicBox.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MusicsController : ControllerBase
    {
        private readonly IMusicAfferentService _afferentService;
        public MusicsController(IMusicAfferentService afferentService)
        {
            _afferentService = afferentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMusic([FromQuery] SearchModel model,string nextKey)
        {
            var results =await _afferentService.BringMusic(model,nextKey);
            return results == null
                ? (IActionResult) BadRequest("Aradığınız sonuç bulunamadı")
                : Ok(results);
        }
    }
}
