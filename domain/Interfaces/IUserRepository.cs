using inventarySystem_backend.domain.Entities;
using inventarySystem_backend.domain.Interfaces;

namespace inventarySystem_backend.domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetEmailAsync(string email);
    Task AddAsync(User user);
}