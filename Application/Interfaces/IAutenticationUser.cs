using Application.Models.Requests;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAutenticationUser
    {
        string Autenticar(User user);
        string encodeSHA256(string text);
        User? ValidateUser(UserLoginRequest userRequest);
    }
}
