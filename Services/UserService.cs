using System;
using FinanceBackend.Data;
using FinanceBackend.DTOs;
using FinanceBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceBackend.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _applicationDbContext = dbContext;
        }


        public User CreateUser([FromBody] CreateUserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentException("Invalid User Data");
            if (string.IsNullOrWhiteSpace(userDto.name))
                throw new ArgumentException("Invalid User Name");
            if (string.IsNullOrWhiteSpace(userDto.email))
                throw new ArgumentException("Invalid User Email");

            UserRole parsedRole;
            if (!Enum.TryParse<UserRole>(userDto.role, true, out parsedRole))
                throw new ArgumentException("Invalid User Role");

            User newUser = new User();
            newUser.name = userDto.name;
            newUser.email = userDto.email;
            newUser.role = parsedRole;
            newUser.isActive = true;
            newUser.createdAt = DateTime.Now;

            _applicationDbContext.Users.Add(newUser);
            _applicationDbContext.SaveChanges();

            return newUser;
        }

        public List<User> GetAllUsers()
        {
            var users = _applicationDbContext.Users.ToList();
            Console.WriteLine("Users count: " + users.Count);
            return users;
        }

        public User? GetUserById(int userId)
        {
            return _applicationDbContext.Users.Find(userId);
        }

        public User UpdateUser(int userId, CreateUserDto updateDto)
        {
            User? user = _applicationDbContext.Users.Find(userId);
            if (user == null) throw new ArgumentException("Invalid UserId");

            if (string.IsNullOrWhiteSpace(updateDto.name))
                throw new ArgumentException("Invalid User Name");
            user.name = updateDto.name;

            if (string.IsNullOrWhiteSpace(updateDto.email))
                throw new ArgumentException("Invalid User Email");
            user.email = updateDto.email;

            UserRole parsedUserRole;
            if (!Enum.TryParse<UserRole>(updateDto.role, true, out parsedUserRole))
                throw new ArgumentException("Invalid User Role");
            user.role = parsedUserRole;

            _applicationDbContext.SaveChanges();
            return user;
        }

        public User ToggleUserStatus(int userId)
        {
            User? user = _applicationDbContext.Users.Find(userId);
            if (user == null) throw new ArgumentException("Invalid UserId");

            user.isActive = !user.isActive;
            _applicationDbContext.SaveChanges();
            return user;
        } 
    }
}

