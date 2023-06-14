using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Services;
using System;

namespace ForumSystemTeamFour.Mappers
{
    public class ReplyMapper
    {
        // Create
        public Reply Map(ReplyCreateDto replyCreateDto, User author)
        {
            return new Reply
            {
                CreationDate = DateTime.Now,
                Author = author,
                Content = replyCreateDto.Content
            };
        }
        // Read
        public ReplyReadDto Map(Reply reply)
        {
            return new ReplyReadDto
            {
                Id = reply.Id,
                ThreadId = (int)reply.ThreadId,
                CreationDate = reply.CreationDate,
                Author = reply.Author,
                Content = reply.Content,
                Likes = reply.Likes,
                Dislikes = reply.Dislikes
            };
        }
        // Update
        public Reply Map(Reply reply, ReplyUpdateDto replyUpdateDto)
        {
            reply.Content = replyUpdateDto.Content;
            reply.CreationDate = DateTime.Now;
            
            return reply;
        }
    }
}
