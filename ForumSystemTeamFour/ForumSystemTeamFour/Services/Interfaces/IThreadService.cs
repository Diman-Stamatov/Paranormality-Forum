using System.Collections.Generic;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;



namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IThreadService
    {
        public ThreadResponcseDto Create(ThreadCreateDto threadCreateDto, int loggedUserId);

        public ThreadResponcseDto Update(int id, ThreadUpdateDto threadUpdateDto, int loggedUserId);

        public ThreadResponcseDto Delete(int id, int loggedUserId);
    }
}
