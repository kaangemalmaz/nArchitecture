using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthServices;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommand : IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAdress { get; set; }


        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly IAuthService _authService;

            public RegisterCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IMapper mapper, IAuthService authService)
            {
                _authBusinessRules = authBusinessRules;
                _userRepository = userRepository;
                _mapper = mapper;
                _authService = authService;
            }

            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                //kontrol
                await _authBusinessRules.EmailCannotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);


                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                User newUser = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Status = true
                };

                User CreatedUser = await _userRepository.AddAsync(newUser);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(CreatedUser);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(CreatedUser, request.IpAdress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);


                RegisteredDto registeredDto = new()
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = addedRefreshToken
                };

                return registeredDto;

            }
        }
    }
}
