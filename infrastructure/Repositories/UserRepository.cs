

using inventarySystem_backend.domain.Entities;
using inventarySystem_backend.domain.Interfaces;
using inventarySystem_backend.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace inventarySystem_backend.infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository (AppDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetUserAsync(int id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<User?> GetEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
    }
}