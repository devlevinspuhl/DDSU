using DDSU_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace DDSU_API.Controllers
{
    public class DDSUController : ControllerBase
    {
        private readonly IDDSUService _service;

        public DDSUController(IDDSUService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/[controller]/GetGrid")]
        public ActionResult GetGrid()
        {
            var result = _service.GetGridValues(0x2200);
            if (result == null)
            {
                return StatusCode(500, "Timeout");
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("api/[controller]/GetMeter")]
        public ActionResult GetMeter()
        {
            var result = _service.GetGridValues(0x2000);
            if (result == null)
            {
                return StatusCode(500, "Timeout");
            }
            return Ok(result);
        }
    }
}
