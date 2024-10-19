using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Add(User user)
        {
            return _userRepository.Add(user);

        }

        public int Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public List<User> Get()
        {
            return _userRepository.Get();

        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);

        }
    }
}
