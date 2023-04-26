using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using HotDesks.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotDesks.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeskController> _logger;
        private readonly IMapper _mapper;

        public DeskController(IUnitOfWork unitOfWork, ILogger<DeskController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDesks()
        {
            try
            {
                var desks = await _unitOfWork.Desks.GetAll(null, null, new List<string>(){"Owner" , "Room"});
                var desksDto = _mapper.Map<IEnumerable<DeskDto>>(desks);
                return Ok(desksDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something wrong in the {nameof(GetDesks)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id:int}", Name = "GetDesk")]        
        public async Task<IActionResult> GetDesk(int id)
        {
            try
            {
                var desk = await _unitOfWork.Desks.GetByIdAsync(id, new List<string>() { "Owner", "Room" });
                var deskDto = _mapper.Map<DeskDto>(desk);
                return Ok(deskDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something wrong in the {nameof(GetDesk)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDesk([FromBody] CreateDeskDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST request in {nameof(CreateDesk)}");
                return BadRequest(ModelState);
            }
            try
            {
                var desk = _mapper.Map<Desk>(dto);
                await _unitOfWork.Desks.Create(desk);
                await _unitOfWork.CommitChanges();

                return CreatedAtRoute("GetDesk", new {Id = desk.Id}, desk );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something wrong in the {nameof(CreateDesk)}");
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}
