using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.AppServices
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<UserRole> GetUserRoleByIdAsync(ObjectId roleId)
        {
            var userRole = await _userRoleRepository.GetUserRoleByIdAsync(roleId);
            if (userRole == null)
            {
                throw new KeyNotFoundException($"User role with id {roleId} not found.");
            }
            return userRole;
        }

        public async Task<UserRole> GetUserRoleByNameAsync(string roleName)
        {
            var userRole = await _userRoleRepository.GetUserRoleByNameAsync(roleName);
            if (userRole == null)
            {
                throw new KeyNotFoundException($"User role with name {roleName} not found.");
            }
            return userRole;
        }

        public async Task CreateUserRoleAsync(UserRole userRole)
        {
            var existingUserRole = await _userRoleRepository.GetUserRoleByIdAsync(userRole.Id);
            if (existingUserRole != null)
            {
                throw new KeyNotFoundException($"User role with id {userRole.Id} already exists.");
            }
            await _userRoleRepository.CreateUserRoleAsync(userRole);
        }

        public async Task DeleteUserRoleAsync(ObjectId roleId)
        {
            var existingUserRole = await _userRoleRepository.GetUserRoleByIdAsync(roleId);
            if (existingUserRole == null)
            {
                throw new KeyNotFoundException($"User role with id {roleId} doesn't exist.");
            }
            await _userRoleRepository.DeleteUserRoleAsync(roleId);
        }

        public async Task UpdateUserRoleAsync(UserRole userRole)
        {
            var existingUserRole = await _userRoleRepository.GetUserRoleByIdAsync(userRole.Id);
            if (existingUserRole == null)
            {
                throw new KeyNotFoundException($"User role with id {userRole.Id} doesn't exist.");
            }
            await _userRoleRepository.UpdateUserRoleAsync(userRole);
        }
    }
}
