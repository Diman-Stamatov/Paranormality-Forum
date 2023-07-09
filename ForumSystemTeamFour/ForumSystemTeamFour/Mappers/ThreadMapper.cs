using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
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

        public LargeThreadResponseDto MapLarge(Thread thread)
        {
            return new LargeThreadResponseDto
            {
                Title = thread.Title,
                Content = thread.Content,
                isDeleted = thread.IsDeleted,
                CreationDate = thread.CreationDate,
                ModificationDate = thread.ModificationDate,
                Replies = ReplyMapper.Map(thread.Replies),
                Tags = TagMapper.Map(thread.Tags),
                Votes = ThreadVoteMapper.Map(thread.Votes),
                Author = userMapper.Map(thread.Author),
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
                Title = thread.Title,
                Content = thread.Content,
                Likes = thread.Votes.Count(v => v.VoteType == VoteType.Like),
                Dislikes = thread.Votes.Count(v => v.VoteType == VoteType.Dislike),
                CreationDate = DateTime.Now,
                Replies = thread.Replies.Count,
                Tags = TagMapper.Map(thread.Tags),    
            };
        }
        public List<ShortThreadResponseDto> Map(List<Thread> threads)
        {
            return threads.Select(this.Map).ToList();
        }

        public PaginatedList<ShortThreadResponseDto> Map(PaginatedList<Thread> threads)
        {
            var mappedThreads = threads.Select(this.Map).ToList();
            return new PaginatedList<ShortThreadResponseDto>(mappedThreads, 
                                                            threads.TotalPages, 
                                                            threads.PageNumber);
        }

        public UserThreadResponseDto MapForUser(Thread thread)
        {
            return new UserThreadResponseDto
            {
                Title = thread.Title,
                CreationDate = thread.CreationDate.ToString(),
                Author = thread.Author.Username,
                NumberOfReplies = thread.Replies.Count,
                Tags = TagMapper.Map(thread.Tags)
            };
        }

        public List<UserThreadResponseDto> MapForUser(List<Thread> threads)
        {
            var x = threads.Select(this.MapForUser).ToList();
            return x;
        }
    }
}
