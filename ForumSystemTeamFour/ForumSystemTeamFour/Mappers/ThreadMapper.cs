using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Enums;
using ForumSystemTeamFour.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystemTeamFour.Mappers
{
    public class ThreadMapper : IThreadMapper
    {
        private readonly IReplyMapper ReplyMapper;
        public ThreadMapper(IReplyMapper replyMapper) 
        { 
            this.ReplyMapper = replyMapper;
        }

        public Thread Map(ThreadCreateDto threadDto, User author)
        {
            return new Thread
            {
                CreationDate = DateTime.Now,
                Title = threadDto.Title,
                AuthorId = author.Id,
                Author = author,
                Content = threadDto.Content
            };
        }


        public ThreadResponseDto Map(Thread thread)
        {
            return new ThreadResponseDto
            {

                Title = thread.Title,
                CreationDate = DateTime.Now,
                ModificationDate = thread.ModificationDate,
                Author = new UserResponseDto
                {
                    FirstName = thread.Author.FirstName,
                    LastName = thread.Author.LastName,
                    Username = thread.Author.Username,
                    Email = thread.Author.Email,
                },
                Content = thread.Content,
                Replies = ReplyMapper.Map(thread.Replies),
                Likes = thread.Votes.Count(v => v.VoteType == VoteType.Like),
                Dislikes = thread.Votes.Count(v => v.VoteType == VoteType.Dislike)
            };
        }

        public Thread Map(Thread threadToUpdate, ThreadUpdateDto threadUpdateDto)
        {
            threadToUpdate.Title = threadUpdateDto.Title ?? threadToUpdate.Title;
            threadToUpdate.Content = threadUpdateDto.Content ?? threadToUpdate.Content;
            threadToUpdate.ModificationDate = DateTime.Now;
            return threadToUpdate;
        }

        public List<ThreadResponseDto> Map (List<Thread> threads)
        {
            var mappedThreads = new List<ThreadResponseDto>();
            foreach (var thread in threads)
            {
                var newThread = this.Map(thread);
                mappedThreads.Add(newThread);
            }
            return mappedThreads;
        }

        public List<UserThreadResponseDto> MapForUser(List<Thread> threads)
        {
            var mappedThreads = new List<UserThreadResponseDto>();
            foreach (var thread in threads)
            {
                mappedThreads.Add(new UserThreadResponseDto
                {
                    Title = thread.Title,
                    CreationDate = thread.CreationDate.ToString(),
                    Author = thread.Author.Username,
                    NumberOfReplies = thread.Replies.Count,
                    Tags = TagMapper.Map(thread.Tags)
                });
            }
            return mappedThreads;
        }
    }
}
