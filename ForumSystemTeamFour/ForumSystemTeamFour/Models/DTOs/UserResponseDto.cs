﻿using ForumSystemTeamFour.Mappers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class UserResponseDto
    {
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }  
        
        public List<UserThreadResponseDto> Threads { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set; }
        

        public override bool Equals(object otherDto)
        {
            var comparedDto = (UserResponseDto)otherDto;
            if (this.FirstName == comparedDto.FirstName 
                && this.LastName == comparedDto.LastName
                && this.Username == comparedDto.Username
                && this.Email == comparedDto.Email
                && this.IsBlocked == comparedDto.IsBlocked)
            {
                return true;
            }
            return false;
        }
    }
}
