using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

[Route("api/auth")]
public class AuthController: ControllerBase 
{
    [HttpPost("")]
    public IActionResult Auth(string login, string pass){
        Guid? userId = UserService.Auth(login, pass);

        if (userId == null)
            return BadRequest(new { message = "Wrong login or pass!" });

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("Любой секретный ключ, единый для инфраструктуры, приложения, роли или пользователя, смотря на сколько вы суровы");
        
        var claim = new Claim("Name", userId.ToString());
        claim.Properties.Add("userId", userId.ToString());

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new [] { claim }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new {
            Id = userId,
            Username = "Админ",
            Token = tokenString
        });

        // read more here http://jasonwatmore.com/post/2018/06/26/aspnet-core-21-simple-api-for-authentication-registration-and-user-management
    }

    [Authorize]
    [HttpGet("test")]
    public object Test(){
        var claim = ((ClaimsIdentity)User.Identity);

        return User.Identity;// $"Very good, {claim.Name}!";
    }
}