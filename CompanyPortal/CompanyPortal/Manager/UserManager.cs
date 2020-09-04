using CompanyPortal.Context;
using CompanyPortal.Models;
using CompanyPortal.Services;
using CompanyPortal.ViewModel;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPortal.Manager
{
    public class UserManager
    {
        private readonly CompanyContext _context;
        private readonly Token _token;

        public UserManager(CompanyContext context, IOptions<Token> token)
        {
            _context = context;
            _token = token.Value;
        }

        /// <summary>
        /// Registers user
        /// </summary>
        /// <returns> user</returns>
        public async Task<User> RegisterAsync(User user)
        {
            try
            {
                if (!UserExists(user.Email))
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return user;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Validates user
        /// </summary>
        /// <returns> user</returns>
        public User LoginAsync(string userId, string password)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(user => user.Email == userId && user.Password == password && user.IsVerified);

                if (user != null )
                {
                    return user;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets user
        /// </summary>
        /// <returns> user</returns>
        public User GetUserByEmail(string email)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(user => user.Email == email);

                if (user != null)
                {
                    return user;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Resets the password
        /// </summary>
        /// <returns> true or false </returns>
        public async Task<bool> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            User user = GetUserByEmail(resetPassword.Email);
            if (user != null && user.Password == resetPassword.OldPassword)
            {
                try
                {
                    user.Password = resetPassword.NewPassword;
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifies user
        /// </summary>
        /// <returns> user</returns>
        public async Task<bool> VerifyUser(User user)
        {
            try
            {
                user.IsVerified = true;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// generates Jwt token
        /// </summary>
        /// <returns>token</returns>
        public string GenerateJwtToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.UserId.ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(_token.TokenExpiryInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        /// <summary>
        /// Check if user exist
        /// </summary>
        /// <returns> user</returns>
        private bool UserExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }
    }
}
