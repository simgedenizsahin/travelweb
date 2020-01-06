using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travel.Entities.Messages
{
    public enum ErrorMessageCode
    {
        usernameAlreadyExists=101,
        EmailALreadyExists=102,
        UserIsNotActive=151,
        UsernameOrPassWrong=152,
        CheckYourEmail=153,
        UserAlreadyActive=154,
        ActivateIdDoesNotExists=155,
        UserNotFound = 156

    }
}
