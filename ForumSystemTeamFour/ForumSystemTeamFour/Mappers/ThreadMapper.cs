using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using ForumSystemTeamFour.Models.Enums;
using ForumSystemTeamFour.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;


namespace ForumSystemTeamFour.Mappers
{
    public class ThreadMapper : IThreadMapper
    {
        private readonly IReplyMapper ReplyMapper;
        private readonly ITagMapper TagMapper;
        private readonly IThreadVoteMapper ThreadVoteMapper;
        private readonly IUserMapper userMapper;
        public ThreadMapper(IReplyMapper replyMapper,
                            ITagMapper tagMapper,
                            IThreadVoteMapper ThreadVoteMapper,
                            IUserMapper userMapper) 
        { 
            this.ReplyMapper = replyMapper;
            this.TagMapper = tagMapper;
            this.ThreadVoteMapper = ThreadVoteMapper;
            this.userMapper = userMapper;
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

        public ThreadUpdateDto Map(ShortThreadResponseDto shortThreadResponseDto)
        {
            return new ThreadUpdateDto
            {
                Title = shortThreadResponseDto.Title,
                Content = shortThreadResponseDto.Content,
            };
        }
        public LargeThreadResponseDto MapLarge(Thread thread)
        {
            return new LargeThreadResponseDto
            {
                Id = thread.Id,
                Title = thread.Title,
                Content = thread.Content,
                isDeleted = thread.IsDeleted,
                CreationDate = thread.CreationDate,
                ModificationDate = thread.ModificationDate,
                Replies = ReplyMapper.Map(thread.Replies),
                Tags = TagMapper.Map(thread.Tags),
                Votes = ThreadVoteMapper.Map(thread.Votes),
                Author = userMapper.MapResponseDto(thread.Author),
            };
        }

        public Thread Map(Thread threadToUpdate, ThreadUpdateDto threadUpdateDto)
        {
            threadToUpdate.Title = threadUpdateDto.Title ?? threadToUpdate.Title;
            threadToUpdate.Content = threadUpdateDto.Content ?? threadToUpdate.Content;
            threadToUpdate.ModificationDate = DateTime.Now;
            return threadToUpdate;
        }

        public ShortThreadResponseDto Map(Thread thread)
        {
            return new ShortThreadResponseDto
            {
                Id = thread.Id,
                Title = thread.Title,
                Content = thread.Content,           
                CreationDate = thread.CreationDate,
                Replies = thread.Replies.Count,
                Tags = TagMapper.Map(thread.Tags),
                IsDeleted = thread.IsDeleted,
                AuthorUserName = thread.Author.Username,
                AuthorId = thread.Author.Id
            };
        }
               

        public List<ShortThreadResponseDto> Map(List<Thread> threads)
        {
            return threads.Select(this.Map).ToList();
        }

        public List<LargeThreadResponseDto> MapLargeList(List<Thread> threads)
        {
            return threads.Select(this.MapLarge).ToList();
        }

        public PaginatedList<ShortThreadResponseDto> Map(PaginatedList<Thread> threads)
        {
            var mappedThreads = threads.Select(this.Map).ToList();
            return new PaginatedList<ShortThreadResponseDto>(mappedThreads, 
                                                            threads.TotalPages, 
                                                            threads.PageNumber);
        }

        
    }
}
