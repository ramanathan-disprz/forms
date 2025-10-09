using forms.Dto;
using forms.Model;
using forms.Request;
using Microsoft.AspNetCore.Identity.Data;

namespace forms.Service.Interface;

public interface IAuthService
{
    User Register(UserRequest request);

    AuthResponseDto Login(LoginRequest request);
}