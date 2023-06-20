using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public class UserAutenticated: IEvent
    {
        public string Email { get; set; }

        protected UserAutenticated()
        {
                
        }

        public UserAutenticated(string email)
        {
            Email = email;
        }
    }
}
