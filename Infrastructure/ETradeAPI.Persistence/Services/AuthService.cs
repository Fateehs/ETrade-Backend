using ETradeAPI.Application.Abstractions.Services;
using ETradeAPI.Application.Abstractions.Token;
using ETradeAPI.Application.DTOs;
using ETradeAPI.Application.Exceptions;
using ETradeAPI.Application.Features.Commands.AppUser.LoginUser;
using ETradeAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;

        public AuthService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenHandler tokenHandler,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByNameAsync(usernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 30);
                return token;
            }

            throw new AuthenticationErrorException();
        }
    }
}
