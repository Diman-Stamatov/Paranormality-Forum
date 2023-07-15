using System.Collections.Generic;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using ForumSystemTeamFour.Models.QueryParameters;



namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IThreadService
    {
        PaginatedList<ShortThreadResponseDto> FilterBy(int loggedUserId, ThreadQueryParameters filterParameters);
        public ShortThreadResponseDto Create(ThreadCreateDto threadCreateDto, int loggedUserId);

        public ShortThreadResponseDto Update(int id, ThreadUpdateDto threadUpdateDto, int loggedUserId);

        public ShortThreadResponseDto Delete(int id, int loggedUserId);

        public List<ShortThreadResponseDto> GetAll();

        List<LargeThreadResponseDto> GetAllLarge();

        public LargeThreadResponseDto Details(int id);
    }
}
