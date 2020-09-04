using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyPortal.Context;
using CompanyPortal.Models;
using CompanyPortal.Manager;
using CompanyPortal.ViewModel;
using CompanyPortal.Services;
using Microsoft.AspNetCore.Authorization;

namespace CompanyPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager _userManager;
        private readonly IMailService _mailService;

        public UsersController(UserManager userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }


        [HttpPost("register")]
        public async Task<ActionResult> RegisterUserAsync(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.RegisterAsync(user);
                if (result != null)
                {
                    await FormatAndSendEmailAsync(result);
                    return Ok(new ResponseModel { Status = "Ok", Message = "User registered ", List = result });
                }
                else
                {
                    return Conflict(new ResponseModel { Status = "Exist", Message = "User email already exist" });
                }
            }

            return BadRequest(new ResponseModel { Status = "Failed", Message = "Model not valid" });
        }

        [HttpGet("login")]
        public ActionResult LoginAsync(string email, string password)
        {
            if (email != null && password != null)
            {
                var result = _userManager.LoginAsync(email, password);
                if (result != null)
                {
                    string token = _userManager.GenerateJwtToken(result);
                    return Ok(new ResponseModel { Status = "Ok", Message = "Logged in successfully", List = token});
                }
            }

            return NotFound(new ResponseModel { Status = "Failed", Message = "Please verify before login" });
        }

        [HttpGet]
        public IActionResult GetUserByEmail(string email)
        {
            User result = _userManager.GetUserByEmail(email);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(new ResponseModel { Status = "Failed", Message = "User not found" });
        }

        [HttpPut("resetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (ModelState.IsValid)
            {
                var response = await _userManager.ResetPassword(resetPassword);
                if (response)
                {
                    return Ok(new ResponseModel { Status = "Ok", Message = "Password reset was successful" });
                }
            }

            return BadRequest(new ResponseModel { Status = "Failed", Message = "Couln't reset the password, Please check your credentials" });
        }

        [HttpGet("verifyUser")]
        public async Task<ActionResult> VerifyUser(string email)
        {
            if (email != null)
            {
                User result = _userManager.GetUserByEmail(email);
                if (result != null)
                {
                    bool response = await _userManager.VerifyUser(result);
                    if (response)
                    {
                        return Ok(new ResponseModel { Status = "Ok", Message = "User verified successfully" });
                    }
                }
            }

            return BadRequest(new ResponseModel { Status = "Failed", Message = "User not valid" });
        }

        private async Task FormatAndSendEmailAsync(User user)
        {
            string verifyUserLink = this.Url.ActionLink("verifyUser", "Users",
                                 new { email = user.Email }, Request.Scheme);
            EmailRequest request = new EmailRequest();
            request.Subject = "Verify user";
            request.ToEmail = user.Email;
            request.Body = verifyUserLink;
            try
            {
                await _mailService.SendEmailAsync(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
