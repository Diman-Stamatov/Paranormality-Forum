﻿using System;

namespace ForumSystemTeamFour.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException() : base()
        { }
        public EntityNotFoundException(string errorMessage) : base(errorMessage)
        { }
    }
}
