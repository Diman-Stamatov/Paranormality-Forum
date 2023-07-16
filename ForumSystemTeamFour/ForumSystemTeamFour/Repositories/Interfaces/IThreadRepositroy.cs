﻿using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.QueryParameters;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Repositories.Interfaces
{
    public interface IThreadRepositroy
    {
        public Thread Create(Thread thread);

        PaginatedList<Thread> FilterBy(User loggedUser, ThreadQueryParameters filterParameters);

        public Thread Update(Thread threadToUpdate, Thread updatedThread);

        public Thread Delete(Thread thread);

        public List<Thread> GetAll();

        public Thread Details(int id);

        public List<Thread> GetAllByUserId(int id);

        int GetCount();

	}
}
