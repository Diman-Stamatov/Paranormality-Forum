using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Services;
using System;

namespace ForumSystemTeamFour.Mappers
{
    public class ReplyMapper : IReplyMapper
    {
        // Create
        public Reply Map(ReplyCreateDto replyCreateDto, User author)
        {
            return new Reply
            {
                CreationDate = DateTime.Now,
                AuthorId = author.Id,
                Author = author,
                ThreadId = (int)replyCreateDto.ThreadId,
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
                Author = new AuthorDto() { UserName = reply.Author.Username, Email = reply.Author.Email },
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
