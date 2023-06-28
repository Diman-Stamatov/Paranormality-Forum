using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ForumSystemTeamFour.Controllers.API
{
    [Route("api/replies")]
    [ApiController]
    public class RepliesApiController : ControllerBase
    {
        private readonly IReplyService replyService;
        private readonly ISecurityServices securityServices;
        private readonly IUserServices userServices;

        public RepliesApiController(IReplyService replyService, ISecurityServices securityServices, IUserServices userServices)
        {
            this.replyService = replyService;
            this.securityServices = securityServices;
            this.userServices = userServices;
        }

        // GetById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                ReplyReadDto replyDto = replyService.GetById(id);
                return StatusCode(StatusCodes.Status200OK, replyDto);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }
        // Query replies by threadId, ???DateTime???, username
        [AllowAnonymous]
        [HttpGet("")]
        public IActionResult FilterBy([FromQuery] ReplyQueryParameters filterParameters)
        {
            try
            {
                List<ReplyReadDto> repliesDtoList = replyService.FilterBy(filterParameters);

                return StatusCode(StatusCodes.Status200OK, repliesDtoList);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }
        // Create
        [Authorize]
        [HttpPost("")]
        public IActionResult Create([FromBody] ReplyCreateDto replyCreateDto)
        {

            try
            {
                int loggedUserId = LoggedUserIdFromClaim();

                ReplyReadDto replyDto = replyService.Create(replyCreateDto, loggedUserId);
                return StatusCode(StatusCodes.Status200OK, replyDto);

                throw new BadHttpRequestException("");
            }
            catch (BadHttpRequestException exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            // TODO: Catch additional types of exceptions.
        }
        // Update
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReplyUpdateDto replyUpdateDto)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();

                ReplyReadDto replyDto = replyService.Update(id, replyUpdateDto, loggedUserId);
                return StatusCode(StatusCodes.Status200OK, replyDto);

                throw new BadHttpRequestException("");
            }
            catch (BadHttpRequestException exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
            // TODO: Catch additional types of exceptions.
        }
        // Delete
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();

                ReplyReadDto replyDto = replyService.Delete(id, loggedUserId);
                return StatusCode(StatusCodes.Status200OK, replyDto);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
            // TODO: Catch additional types of exceptions.
        }
        // UpVote
        [Authorize]
        [HttpPut("upvote/{id}")]
        public IActionResult UpVote(int id)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();

                ReplyReadDto replyDto = replyService.UpVote(id, loggedUserId);
                return StatusCode(StatusCodes.Status200OK, replyDto);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
            catch (DuplicateEntityException exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
            // TODO: Catch additional types of exceptions.
        }
        // DownVote
        [Authorize]
        [HttpPut("downvote/{id}")]
        public IActionResult DownVote(int id)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();

                ReplyReadDto replyDto = replyService.DownVote(id, loggedUserId);
                return StatusCode(StatusCodes.Status200OK, replyDto);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
            catch (DuplicateEntityException exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
            // TODO: Catch additional types of exceptions.
        }
        private int LoggedUserIdFromClaim()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = identity.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value;

            return int.Parse(userIdClaim);
        }
    }
}
