using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.QueryParameters;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Repositories.Interfaces
{
    public interface IThreadRepositroy
    {
        public Thread Create(Thread thread);

        public Thread Update(Thread threadToUpdate, Thread updatedThread);

        public Thread Delete(int id);

        public List<Thread> GetAll();

        public Thread GetById(int id);

        public List<Thread> GetAllByUserId(int id);        
    }
}
