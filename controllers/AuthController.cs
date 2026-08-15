using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventarySystem_backend.application.DTOs;
using inventarySystem_backend.application.Interfaces;
using inventarySystem_backend.application.Services;
using Microsoft.AspNetCore.Mvc;

namespace inventarySystem_backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var response = await _authService.RegisterAsync(dto);
        if(response == null)
        {
            return BadRequest(new { message = "El nombre de usuario o correo electrónico ya se encuentra registrado."});
        }
        return Ok(response);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);
        if(response == null)
        {
            return Unauthorized( new { message = "Credenciales incorrectas." });
        }
        return Ok(response);
    }

}
