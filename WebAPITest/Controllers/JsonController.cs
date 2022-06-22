using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPITest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JsonController : ControllerBase
    {

        private readonly ILogger<JsonController> _logger;

        public JsonController(ILogger<JsonController> logger)
        {
            _logger = logger;
        }

        //yyyy-MM-dd HH:mm:ss.fffzzz

        [HttpGet]
        [Route("[action]")]
        public dynamic UtcNow()
        {
            return DateTime.UtcNow;
            //"2022-06-22T05:53:35.3942878Z"
        }

        [HttpGet]
        [Route("[action]")]
        public dynamic Now()
        {
            return DateTime.Now;
            //"2022-06-22T13:54:23.2843059+08:00"
        }

        [HttpGet]
        [Route("[action]")]
        public dynamic OffsetUtcNow()
        {
            return DateTimeOffset.UtcNow;
            //"2022-06-22T05:55:57.4313761+00:00" old
            //"2022-06-22 07:12:35.446+00:00" custom coverter
        }

        [HttpGet]
        [Route("[action]")]
        public dynamic ToStr()
        {
            return DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffzzz");
            //"2022-06-22 06:28:55.207+00:00"
        }

        [HttpGet]
        [Route("[action]")]
        public dynamic Parse()
        {
            return DateTime.Parse(DateTimeOffset.UtcNow.ToString());
            //"2022-06-22T14:38:16+08:00"
        }

        [HttpGet]
        [Route("[action]")]
        public dynamic Custom()
        {
            var options = new JsonSerializerOptions() { WriteIndented = true };
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            options.Converters.Add(new CustomDateTimeOffsetConverter("yyyy-MM-dd HH:mm:ss.fffzzz"));
            return JsonSerializer.Serialize(DateTimeOffset.UtcNow, options);
            //"2022-06-22 07:05:11.191+00:00"
        }
    }
}
