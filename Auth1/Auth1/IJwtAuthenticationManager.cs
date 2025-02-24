﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth1
{
   
        public class JwtAuthenticationManager : IJWTAuthenticationManager
        {
            IDictionary<string, string> users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" }
        };

            private readonly string tokenKey;

            public JwtAuthenticationManager(string tokenKey)
            {
                this.tokenKey = tokenKey;
            }

            public string Authenticate(string username, string password)
            {
                if (!users.Any(u => u.Key == username && u.Value == password))
                {
                    return null;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key =
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }

