using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models.ViewModels;
using ForumSystemTeamFour.Models.ViewModels.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static ForumSystemTeamFour.Models.Enums.VoteType;

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
            return new ReplyReadDto()
            {
                Id = reply.Id,
                ThreadId = (int)reply.ThreadId,
                Author = new AuthorDto() 
                { 
                    UserName = reply.Author.Username,
					TotalPosts = reply.Author.TotalPosts,
					Email = reply.Author.Email 
                },
                CreationDate = reply.CreationDate,
                ModificationDate = reply.ModificationDate,
                Content = reply.Content,
                Likes = reply.Votes.Count(v => v.VoteType == Like),
                Dislikes = reply.Votes.Count(v => v.VoteType == Dislike)
            };
        }

        public List<ReplyReadDto> Map(List<Reply> replies)
        {
            return replies.Select(reply => Map(reply)).ToList();
        }
        public ReplyViewModel MapViewModel(Reply reply)
        {
            return new ReplyViewModel()
            {
                Id = reply.Id,
                ThreadId = (int)reply.ThreadId,
                AuthorId = reply.AuthorId,
                Author = new AuthorDto() { UserName = reply.Author.Username, TotalPosts = reply.Author.TotalPosts, Email = reply.Author.Email },
                CreationDate = reply.CreationDate,
                ModificationDate = reply.ModificationDate,
                Content = reply.Content,
                Likes = reply.Votes.Where(v => v.VoteType == Like).ToList(),
                Dislikes = reply.Votes.Where(v => v.VoteType == Dislike).ToList()                
            };
        }

        // Update
        public Reply Map(Reply reply, ReplyUpdateDto replyUpdateDto)
        {
            reply.Content = replyUpdateDto.Content;
            reply.ModificationDate = DateTime.Now;
            
            return reply;
        }
        public Reply MapViewModel(Reply reply, ReplyViewModel replyUpdateViewModel)
        {
            reply.Content = replyUpdateViewModel.Content;
            reply.ModificationDate = DateTime.Now;

            return reply;
        }

        public ReplyQueryParameters MapViewQuery(ReplyQueryParametersVM queryVM)
        {
            return new ReplyQueryParameters()
            {
                ThreadId = queryVM.ThreadId,
                UserName = queryVM.UserName,
                CreatedAfter = queryVM.CreatedAfter,
                CreatedBefore = queryVM.CreatedBefore,
                SortBy = queryVM.SortBy,
                SortOrder = queryVM.SortOrder
            };
        }

		public List<ReplyViewModel> MapViewModelList(List<Reply> replies)
		{
			return replies.Select(this.MapViewModel).ToList();
		}

	}
}
