using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;

namespace ForumSystemTeamFour.Mappers
{
    public class UserMapper
    {
        /*private readonly EmailService emailService;*/
        /*private readonly UsernameService usernameService;*/

        /*public UserMapper(EmailService emailService, UsernameService usernameService) 
        { 
            this.emailService = emailService;
            this.usernameService = usernameService;
        }*/

        public User Map(UserDto userDto) 
        {
            int usernameId /*= usernameService.Crate(userDto.Username)*/;
            int emailId /*= emailService.Crate(userDto.Username)*/;
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                /*EmailId =emailId,*/
                /*UsernameId = usernameId,*/
                Password = userDto.Password
            };
        }
    }
}
