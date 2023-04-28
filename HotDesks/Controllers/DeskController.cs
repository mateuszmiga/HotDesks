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
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDesks()
        {
            try
            {
                var desks = await _unitOfWork.Desks.GetAll(null, null, new List<string>(){"Owner" , "Room"});
                var desksDto = _mapper.Map<IEnumerable<DeskDto>>(desks);
                return Ok(desksDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something wrong in the {nameof(GetAllDesks)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("Get/{id:int}", Name = "GetDesk")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDesk(int id)
        {
            try
            {
                var desk = await _unitOfWork.Desks.GetByIdAsync(id, new List<string>() { "Owner", "Room" });
                if (desk == null)
                {
                    _logger.LogError($"There is no desk with specified Id: {id}");
                    return BadRequest();
                }
                var deskDto = _mapper.Map<DeskDto>(desk);
                return Ok(deskDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something wrong in the {nameof(GetDesk)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                _logger.LogInformation($"New Desk Created. Id: {desk.Id} ; Description: {desk.Description}");

                return StatusCode(201, "Desk created.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something wrong in the {nameof(CreateDesk)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpPut("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDesk(int id, [FromBody] UpdateDeskDto dto)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(UpdateDesk)}");
                return BadRequest(ModelState);
            }
            try
            {
                var desk = await _unitOfWork.Desks.GetByIdAsync(id);
                if(desk == null)
                {
                    _logger.LogError("User specified wrong id, when tried to UpdateDesk");
                    return BadRequest($"Submitted data is invalid. There is no desk with id: {id} ");
                }

                _mapper.Map(dto, desk);
                await _unitOfWork.CommitChanges(); //entity is tracked, so it can be directly saved. No need to use Update() method.

                return StatusCode(200, "Desk updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something wrong in the {nameof(UpdateDesk)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize(Roles = "Administrator")]        
        [HttpDelete("Delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDesk(int id)
        {
            try
            {
                var desk = await _unitOfWork.Desks.GetByIdAsync(id);
                if (desk == null)
                {
                    _logger.LogError($"Unable to delete desk, cause: null value");
                    return BadRequest();
                }
                await _unitOfWork.Desks.Delete(desk);
                await _unitOfWork.CommitChanges();
                _logger.LogInformation($"Desk Deleted. Id: {desk.Id} ; Description: {desk.Description}");

                return StatusCode(200, "Desk deleted succesfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something wrong in the {nameof(DeleteDesk)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
