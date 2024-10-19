using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AutenticationService : IAutenticationUser
    {
        private readonly IUserRepository _userRepository ;
        private readonly AutenticationServiceOptions _options;

        public AutenticationService(IUserRepository userRepository, IOptions<AutenticationServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }
        public string encodeSHA256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                StringBuilder builder = new StringBuilder();
                for (int i =0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
                
            }
        }

        public User? ValidateUser(UserLoginRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            User? user = _userRepository.GetByEmail(authenticationRequest.Email);

            if (user == null) return null;

            string passwordHashed = encodeSHA256(authenticationRequest.Password);
            if (user.Rol == authenticationRequest.Rol && user.Password == passwordHashed) return user;
            

            return null;

        }
        public string Autenticar (User user)
        {
            //crear info del usuario para el token
          

               var userClaims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Rol),
                };
                var seccurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
                var credentials = new SigningCredentials(seccurityKey, SecurityAlgorithms.HmacSha256);

                //Crear el dettale del token
                var jwtConfig = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: userClaims,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    credentials);

                return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
            
          
        
    }

    public class AutenticationServiceOptions
    {
        public const string AutenticacionService = "AutenticacionService";

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretForKey { get; set; }
    }
}
}
