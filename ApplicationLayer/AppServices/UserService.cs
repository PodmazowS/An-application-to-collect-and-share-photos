using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;

namespace ApplicationLayer.AppServices;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task CreateUserAsync(User user)
    {
        var existingUser = await _userRepository.GetUserByEmail(user.Email);

        if (existingUser != null)
        {
            throw new DuplicateBsonMemberMapAttributeException($"User with email {user.Email} already exists.");
        }

        await _userRepository.CreateUser(user);
    }

    public async Task DeleteUserAsync(ObjectId id)
    {
        var existingUser = await _userRepository.GetUserById(id);

        if (existingUser == null)
        {
            throw new KeyNotFoundException($"User with id {id} doesn't exist.");
        }

        await _userRepository.DeleteUser(id);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            return null;
        }

        return user;
    }

    public async Task<User> GetUserByIdAsync(ObjectId id)
    {
        var user = await _userRepository.GetUserById(id);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found.");
        }

        return user;
    }

    public async Task UpdateUserAsync(ObjectId id, User user)
    {
        var existingUser = await _userRepository.GetUserById(id);

        if (existingUser == null)
        {
            throw new KeyNotFoundException($"User with id {id} doesn't exist.");
        }

        await _userRepository.UpdateUser(id, user);
    }

    public async Task<bool> CheckPasswordAsync(ObjectId userId, string password)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {userId} not found.");
        }

        var passwordMatch = await _userRepository.CheckPassword(userId, password);

        return passwordMatch;
    }

}
