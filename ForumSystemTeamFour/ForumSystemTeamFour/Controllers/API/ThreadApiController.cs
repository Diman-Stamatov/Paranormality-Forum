using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;

using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace ForumSystemTeamFour.Controllers.API
{
    [ApiController]
    [Route("api/threads")]
    public class ThreadApiController : ControllerBase
    {
        private readonly IThreadService threadService;
        private readonly ISecurityServices securityServices;

        public ThreadApiController(IThreadService threadService,
                                    ISecurityServices securityServices)
        {
            this.threadService = threadService;
            this.securityServices = securityServices;
        }
        [Authorize]
        [HttpPost("")]
        public IActionResult Create([FromBody] ThreadCreateDto threadCreateDto)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();
                ThreadResponseDto threadResponseDto = this.threadService.Create(threadCreateDto, loggedUserId);                
                return StatusCode(StatusCodes.Status201Created, threadResponseDto);
            }
            catch (BadHttpRequestException exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (DuplicateEntityException exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ThreadUpdateDto threadCreateDto)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();
                ThreadResponseDto threadResponseDto = this.threadService.Update(id, threadCreateDto, loggedUserId);
                return StatusCode(StatusCodes.Status201Created, threadResponseDto);
            }
            catch (BadHttpRequestException exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();
                ThreadResponseDto threadResponseDto = this.threadService.Delete(id, loggedUserId);
                return StatusCode(StatusCodes.Status201Created, threadResponseDto);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (UnauthorizedOperationException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpGet("")]
        public IActionResult GetAll() 
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();          
                List<ThreadResponseDto> threds = this.threadService.GetAll();
                return StatusCode(StatusCodes.Status200OK, threds);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();
                ThreadResponseDto thread = this.threadService.GetById(id);
                return StatusCode(StatusCodes.Status201Created, thread);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }

        [Authorize]
        [HttpGet("{userId}")]
        public IActionResult GetAllByUserId(int userId)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();
                List<ThreadResponseDto> threds = this.threadService.GetAllByUserId(userId);
                return StatusCode(StatusCodes.Status200OK, threds);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }

        private int LoggedUserIdFromClaim()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = identity.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value;

            return int.Parse(userIdClaim);
        }
    }
}
