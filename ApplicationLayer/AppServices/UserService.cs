using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using MongoDB.Bson;

namespace ApplicationLayer.AppServices;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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

    public async Task<User> GetUserByIdAsync(ObjectId userId)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {userId} not found.");
        }

        return user;
    }
}
