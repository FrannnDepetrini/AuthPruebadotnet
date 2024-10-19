using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(User user)
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
            return 1;

        }

        public int Delete(int id)
        {
            User? userFound = _context.Set<User>().Find(id);
            if (userFound != null)
            {
                _context.Set<User>().Remove(userFound);
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public List<User> Get()
        {
            return _context.Set<User>().ToList();

        }

        public User GetById(int id)
        {
            User? userFound = _context.Set<User>().Find(id);
            if (userFound != null)
            {
                
                return userFound;
            }
            return userFound;

        }

        public User? GetByEmail(string email)
        {
            User? userFound = _context.Set<User>().FirstOrDefault(user => user.Email == email);
            if (userFound != null)
            {

                return userFound;
            }
            return null;
        }

    }
}
