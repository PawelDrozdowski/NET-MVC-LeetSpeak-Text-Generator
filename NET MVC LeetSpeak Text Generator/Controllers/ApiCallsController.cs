using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NET_MVC_LeetSpeak_Text_Generator.Data;
using NET_MVC_LeetSpeak_Text_Generator.Models;
using NET_MVC_LeetSpeak_Text_Generator.Models.DTO;

namespace NET_MVC_LeetSpeak_Text_Generator.Controllers
{
    public class ApiCallsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApiCallsController> _logger;

        public ApiCallsController(IMapper mapper, ApplicationDbContext context, ILogger<ApiCallsController> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        // GET: ApiCalls
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApiCalls.ToListAsync());
        }


        // GET: ApiCalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApiCalls/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApiCallDtoCreate apiCallDto)
        {
            _logger.LogInformation($"Api call for {apiCallDto.Request}");

            var apiCall = _mapper.Map<ApiCall>(apiCallDto);
            if (ModelState.IsValid)
            {
                _context.Add(apiCall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(apiCall);
        }


        private bool ApiCallExists(int id)
        {
            return _context.ApiCalls.Any(e => e.ID == id);
        }
    }
}
