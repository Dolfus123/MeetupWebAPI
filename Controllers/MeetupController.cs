using AutoMapper;
using Entities.Models;
using MeetupWebAPI.Contracts;
using MeetupWebAPI.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Controllers
{
    [Route("api/meetup")]
    [ApiController]
    public class MeetupController : ControllerBase
    {
        private readonly ILogger _logger;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public MeetupController(ILogger logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Returned all meetups from database.
        /// </summary>
        /// <response code="201">Meetups received</response>

        [HttpGet]
        public async Task<IActionResult> GetAllMeetups()
        {
            try
            {
                var meetups = await _unitOfWork.Meetup.GetAllMeetupsAsync();
                _logger.LogInformation($"Returned all meetups from database.");

                var meetupResult = _mapper.Map<IEnumerable<MeetupDto>>(meetups);

                return Ok(meetupResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllMeetups action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Returned meetup by id.
        /// </summary>
        /// <response code="201">Returned meetup by id</response>
        /// <response code="400">If the item is null</response>

        [HttpGet("{id}", Name = "MeetupById")]
        public async Task<IActionResult> GetMeetupById(Guid id)
        {
            try
            {
                var meetup = await _unitOfWork.Meetup.GetMeetupByIdAsync(id);


                if (meetup is null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned meetup with id: {id}");

                    var meetupResult = _mapper.Map<MeetupDto>(meetup);
                    return Ok(meetupResult);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMeetupById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Returned meetup with details for id.
        /// </summary>
        /// <response code="201">Returned meetup with details for id</response>
        /// <response code="400">If the item is null</response>

        [HttpGet("{id}/account")]
        public async Task<IActionResult> GetMeetupWithDetails(Guid id)
        {
            try
            {
                var meetup = await _unitOfWork.Meetup.GetMeetupWithDetailsAsync(id);
                if (meetup == null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned meetup with details for id: {id}");

                    var meetupResult = _mapper.Map<MeetupDto>(meetup);
                    return Ok(meetupResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMeetupWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Creates a Meetup.
        /// </summary>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>

        [HttpPost]
        public async Task<IActionResult> CreateMeetup([FromBody] MeetupForCreationDto meetup)
        {
            try
            {
                if (meetup is null)
                {
                    _logger.LogError("Meetup object sent from client is null.");
                    return BadRequest("Meetup object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid meetup object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var meetupEntity = _mapper.Map<Meetup>(meetup);
                _unitOfWork.Meetup.CreateMeetup(meetupEntity);
                await _unitOfWork.SaveAsync();
                var createdMeetup = _mapper.Map<MeetupDto>(meetupEntity);
                return CreatedAtRoute("MeetupById", new { id = createdMeetup.Id }, createdMeetup);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateMeetup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Сhange a Meetup.
        /// </summary>
        /// <response code="201">Meetup changed</response>
        /// <response code="400">If the item is null</response>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeetup(Guid id, [FromBody] MeetupForUpdateDto meetup)
        {
            try
            {
                if (meetup is null)
                {
                    _logger.LogError("Meetup object sent from client is null.");
                    return BadRequest("Meetup object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid meetup object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var meetupEntity = await _unitOfWork.Meetup.GetMeetupByIdAsync(id);
                if (meetupEntity is null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(meetup, meetupEntity);
                _unitOfWork.Meetup.UpdateMeetup(meetupEntity);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateMeetup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a specific Meetup.
        /// </summary>
        /// <response code="201">Meetup deleted</response>
        /// <response code="400">If the item is null</response>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetup(Guid id)
        {
            try
            {
                var meetup = await _unitOfWork.Meetup.GetMeetupByIdAsync(id);
                if (meetup == null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                if (_unitOfWork.User.UsersByMeetup(id).Any())
                {
                    _logger.LogError($"Cannot delete meetup with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete meetup. It has related accounts. Delete those accounts first");
                }
                _unitOfWork.Meetup.DeleteMeetup(meetup);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteMeetup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
