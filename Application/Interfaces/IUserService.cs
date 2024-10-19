using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        int Add(User user);
        int Delete(int id);
        List<User> Get();
        User GetById(int id);
    }
}
