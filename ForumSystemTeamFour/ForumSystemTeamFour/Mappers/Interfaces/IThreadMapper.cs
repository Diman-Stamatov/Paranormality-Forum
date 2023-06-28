using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models;
using System.Collections.Generic;
using System;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IThreadMapper
    {
        public Thread Map(ThreadCreateDto threadDto, User author);

        public ThreadResponcseDto Map(Thread thread);

        public Thread Map(Thread threadToUpdate, ThreadUpdateDto threadUpdateDto);

        public List<ThreadResponcseDto> Map(List<Thread> threads);
    }
}
