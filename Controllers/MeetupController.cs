﻿using AutoMapper;
using Entities.Models;
using MeetupWebAPI.Contracts;
using MeetupWebAPI.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetupWebAPI.Controllers
{
    [Route("api/meetup")]
    [ApiController]
    public class MeetupController : ControllerBase
    {
        private ILoggerManager _logger;
        private IUnitOfWork _repository;
        private IMapper _mapper;
        public MeetupController(ILoggerManager logger, IUnitOfWork repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returned all meetups from database.
        /// </summary>
        /// <response code="201">Meetups received</response>

        [HttpGet]
        public IActionResult GetAllMeetups()
        {
            try
            {
                var meetups = _repository.Meetup.GetAllMeetupsAsync();
                _logger.LogInfo($"Returned all meetups from database.");

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
        public IActionResult GetMeetupById(Guid id)
        {
            try
            {
                var meetup = _repository.Meetup.GetMeetupByIdAsync(id);


                if (meetup is null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned meetup with id: {id}");

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
        public IActionResult GetMeetupWithDetails(Guid id)
        {
            try
            {
                var meetup = _repository.Meetup.GetMeetupWithDetailsAsync(id);
                if (meetup == null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned meetup with details for id: {id}");

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
        public IActionResult CreateMeetup([FromBody] MeetupForCreationDto meetup)
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
                _repository.Meetup.CreateMeetup(meetupEntity);
                _repository.CompleteAsync();
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
        public IActionResult UpdateMeetup(Guid id, [FromBody] MeetupForUpdateDto meetup)
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
                var meetupEntity = _repository.Meetup.GetMeetupByIdAsync(id);
                if (meetupEntity is null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(meetup, meetupEntity);
                _repository.Meetup.UpdateMeetup(meetupEntity);
                _repository.CompleteAsync();
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
        public IActionResult DeleteMeetup(Guid id)
        {
            try
            {
                var meetup = _repository.Meetup.GetMeetupByIdAsync(id);
                if (meetup == null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                if (_repository.User.UsersByMeetup(id).Any())
                {
                    _logger.LogError($"Cannot delete meetup with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete meetup. It has related accounts. Delete those accounts first");
                }
                _repository.Meetup.DeleteMeetup(meetup);
                _repository.CompleteAsync();
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
