using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using ForumSystemTeamFour.Models.Enums;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IThreadMapper
    {
        public Thread Map(ThreadCreateDto threadDto, User author);

        public LargeThreadResponseDto MapLarge(Thread thread);

        public Thread Map(Thread threadToUpdate, ThreadUpdateDto threadUpdateDto);

        public ShortThreadResponseDto Map(Thread thread);

        public PaginatedList<ShortThreadResponseDto> Map(PaginatedList<Thread> threads);

        public List<ShortThreadResponseDto> Map(List<Thread> thread);

       
    }
}

