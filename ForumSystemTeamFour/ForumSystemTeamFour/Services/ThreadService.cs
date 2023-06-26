using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace ForumSystemTeamFour.Services
{
    public class ThreadService
    {
        private readonly IThreadRepositroy threadRepositroy;
        private readonly ISecurityServices forumSecurity;

        public ThreadService(IThreadRepositroy threadRepositroy, 
                                ISecurityServices securityServices)
        {
            this.threadRepositroy = threadRepositroy;
            this.forumSecurity = securityServices;
        }

    }
}
