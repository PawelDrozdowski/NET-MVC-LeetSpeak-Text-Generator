using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NET_MVC_LeetSpeak_Text_Generator.Contracts;
using NET_MVC_LeetSpeak_Text_Generator.Data;
using NET_MVC_LeetSpeak_Text_Generator.Helpers;
using NET_MVC_LeetSpeak_Text_Generator.Models;
using NET_MVC_LeetSpeak_Text_Generator.Models.DTO;
using NuGet.Common;
using System.Drawing.Drawing2D;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NET_MVC_LeetSpeak_Text_Generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCallsControllerApi : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApiCallsController> _logger;
        private readonly ITranslator _translator;

        public ApiCallsControllerApi(IMapper mapper, ApplicationDbContext context, ILogger<ApiCallsController> logger, ITranslator translator)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _translator = translator;
        }
        // GET api/ApiCallsControllerApi
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            if(User.Identity.IsAuthenticated)
                return Ok(await _context.ApiCalls.ToListAsync());
            return Unauthorized();
        }

        // POST api/ApiCallsControllerApi
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ApiCallDtoCreate apiCallDto)
        {
            _logger.LogInformation($"Api call for {apiCallDto.Request}");
            var apiCall = _mapper.Map<ApiCall>(apiCallDto);
            if (ModelState.IsValid)
            {
                try
                {
                    TranslatorDtoResponse response = await _translator.TranslateText(apiCallDto.Request);
                    apiCall.Response = response.contents.translated;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Api call failure {apiCallDto.Request}, {ex.Message}");
                    return Problem("API Error");
                }

                _context.Add(apiCall);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Api call success {apiCallDto.Request}");
                return Ok(apiCall.Response);
            }
            return BadRequest();
        }
    }
}
