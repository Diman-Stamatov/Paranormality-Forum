﻿using ForumSystemTeamFour.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using ForumSystemTeamFour.Models.Enums;
using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using ForumSystemTeamFour.Models.ViewModels.Thread;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IThreadMapper
    {
        public Thread Map(ThreadCreateDto threadDto, User author);

        public ThreadVM MapVM(Thread thread);

        public Thread Map(Thread threadToUpdate, ThreadUpdateDto threadUpdateDto);

        public ShortThreadResponseDto Map(Thread thread);

        public PaginatedList<ShortThreadResponseDto> Map(PaginatedList<Thread> threads);

        List<ThreadVM> MapVMList(List<Thread> threads);

        public List<ShortThreadResponseDto> Map(List<Thread> thread);
        public ThreadIndexVM MapVM(List<ShortThreadResponseDto> list);



    }
}

