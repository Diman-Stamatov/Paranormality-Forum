﻿using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models;
using System.Collections.Generic;
using System;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IThreadMapper
    {
        public Thread Map(ThreadCreateDto threadDto, User author);

        public ThreadResponseDto Map(Thread thread);

        public Thread Map(Thread threadToUpdate, ThreadUpdateDto threadUpdateDto);

        public List<ThreadResponseDto> Map(List<Thread> threads);

        List<UserThreadResponseDto> MapForUser(List<Thread> threads);
    }
}