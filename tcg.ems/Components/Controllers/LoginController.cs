//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Data;
//using System.Linq.Expressions;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Threading.Tasks;
//using TCG.Application.Dtos;
//using TCG.Application.Services;
//using TCG.Domain.Entities;

//namespace TCG.EMS.Components.Controllers
//{
//    public class LoginController : Controller
//    {
//        private readonly LoginAuthService _loginAuthService;
//        private readonly GenericDbService<Staff, StaffDto> _staffService;

//        public LoginController(
//            LoginAuthService loginAuthService,
//            GenericDbService<Staff, StaffDto> staffService
//        )
//        {
//            _loginAuthService = loginAuthService;
//            _staffService = staffService;
//        }

//        public async Task<IActionResult> Login(string email, string password)
//        {
//            if (!await _loginAuthService.VerifyLoginAsync(email, password))
//            {
//                return Unauthorized("Invalid email or password");
//            }

//            StaffDto user = await _staffService.GetByAsync(e => e.StaffEmail == email);
//            if (user == null)
//                return Unauthorized("User not found");

//            string userRole = user.StaffRoleHead ? "head" :
//                              user.StaffRoleManagement ? "management" :
//                              "staff";

//            var claims = new[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.StaffId.ToString()),
//                new Claim(ClaimTypes.Email, user.StaffEmail),
//                new Claim(ClaimTypes.Role, userRole)
//            };

//            var identity = new ClaimsIdentity(claims, "default");
//            var principal = new ClaimsPrincipal(identity);

//            await HttpContext.SignInAsync("default", principal);

//            return Redirect("/home");
//        }
//    }
//}
