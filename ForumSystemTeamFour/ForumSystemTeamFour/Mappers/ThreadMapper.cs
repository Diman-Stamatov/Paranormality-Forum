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

        public Models.DTOs.ThreadResponseDto Map(Thread thread)
        {
            return new Models.DTOs.ThreadResponseDto
            {
                Id = thread.Id,
                Title = thread.Title,
                CreationDate = DateTime.Now,
                ModificationDate = thread.ModificationDate,
                Content = thread.Content,
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

        public List<Models.DTOs.ThreadResponseDto> Map (List<Thread> threads)
        {
            var mappedThreads = new List<Models.DTOs.ThreadResponseDto>();
            foreach (var thread in threads)
            {
                var newThread = this.Map(thread);
                mappedThreads.Add(newThread);
            }
            return mappedThreads;
        }
    }
}
