using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystemTeamFour.Controllers
{
    [Route("api/replies")]
    [ApiController]
    public class RepliesApiController : ControllerBase
    {
        private readonly IReplyService replyService;
        private readonly ReplyMapper replyMapper;

        public RepliesApiController(IReplyService replyService, ReplyMapper replyMapper)
        {
            this.replyService = replyService;
            this.replyMapper = replyMapper;
        }

        // GetById
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                // TODO: Authenticate the user
                Reply reply = replyService.GetById(id);
                ReplyReadDto replyDto = replyMapper.Map(reply);
                return StatusCode(StatusCodes.Status200OK, replyDto);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            // TODO: Catch "Auth" exceptions
        }
        // Query replies by threadId, ???DateTime???, username
        [HttpGet("")]
        public IActionResult FilterBy([FromQuery] ReplyQueryParameters filterParameters)
        {
            try
            {
                // TODO: Authenticate the user
                List<Reply> replies = replyService.FilterBy(filterParameters);

                List<ReplyReadDto> repliesDtoList = replies.Select(reply => replyMapper.Map(reply)).ToList();

                return StatusCode(StatusCodes.Status200OK, repliesDtoList);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            // TODO: Catch "Auth" exceptions
        }
        // Create
        // Update
        // Delete
        // UpVote
        // DownVote
    }
}
