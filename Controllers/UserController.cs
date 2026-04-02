using FinanceBackend.DTOs;
using FinanceBackend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult CreateUser(CreateUserDto dto)
    {
        try
        {
            var createdUser = _userService.CreateUser(dto);
            return Created("", createdUser);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        Console.WriteLine("GET HIT"); // debug
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserById(int userId)
    {
        var users = _userService.GetUserById(userId);
        if (users == null) return NotFound();
        return Ok(users);
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId,CreateUserDto updateDto)
    {
        try
        {
            var user=_userService.UpdateUser(userId, updateDto);
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{userId}/toggle")]
    public IActionResult ToggleUserStatus(int userId)
    {
        try
        {
            var user=_userService.ToggleUserStatus(userId);
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
