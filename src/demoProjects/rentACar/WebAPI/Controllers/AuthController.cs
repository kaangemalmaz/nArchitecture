using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new RegisterCommand()
            {
                IpAdress = GetIpAdress(),
                UserForRegisterDto = userForRegisterDto
            };

            RegisteredDto registeredDto = await Mediator.Send(registerCommand);
            SetRefreshTokenToCookie(registeredDto.RefreshToken);
            return Created("", registeredDto.AccessToken);
        }




        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) }; //cookie ayarı yapmak için.
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions); // refresh token isminde, valuesi token olan ve cookie ayaları şu şekilde bunu cookielere ekle demektir.
        }
    }
}
