using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {

        int Add(User user);
        int Delete(int id);
        List<User> Get();
        User GetById(int id);
        User? GetByEmail(string email);

    }
}
