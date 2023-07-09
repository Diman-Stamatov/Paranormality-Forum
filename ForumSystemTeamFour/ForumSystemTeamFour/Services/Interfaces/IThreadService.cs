using System.Collections.Generic;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;



namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IThreadService
    {
        List<ThreadResponseDto> FilterBy(int loggedUserId, ThreadQueryParameters filterParameters);
        public ThreadResponseDto Create(ThreadCreateDto threadCreateDto, int loggedUserId);

        public ThreadResponseDto Update(int id, ThreadUpdateDto threadUpdateDto, int loggedUserId);

        public ThreadResponseDto Delete(int id, int loggedUserId);

        public List<ThreadResponseDto> GetAll();

        public ThreadResponseDto GetById(int id);

        public List<ThreadResponseDto> GetAllByUserId(int id);
    }
}
