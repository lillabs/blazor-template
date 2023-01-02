﻿using Model.Entities.Authentication;
using Model.Entities.Authentication.Models;

namespace Domain.Repositories.Implementations;

public class UserRepository : ARepository<User>, IUserRepository {
    public UserRepository(ModelDbContext context) : base(context) {
    }


    public async Task<User?> FindByEmailAsync(string email, CancellationToken ct = default) {
        var user = await Table
            .Include(u => u.RoleClaims)
            .ThenInclude(rc => rc.Role)
            .AsSplitQuery() // <--- this is the magic
            .FirstOrDefaultAsync(u => u.Email == email, ct);

        return user?.ClearSensitiveData();
    }

    public async Task<User?> AuthorizeAsync(int id, CancellationToken ct = default) {
        var user = await Table
            .Include(u => u.RoleClaims)
            .ThenInclude(rc => rc.Role)
            .AsSplitQuery() // <--- this is the magic
            .FirstOrDefaultAsync(u => u.Id == id, ct);

        return user?.ClearSensitiveData();
    }

    public Task<User?> AuthorizeAsync(string token, CancellationToken ct = default) {
        throw new NotImplementedException("Bababum");
/*
        var user = await Table
            .Include(u => u.RoleClaims)
            .ThenInclude(rc => rc.Role)
            .AsSplitQuery() // <--- this is the magic
            .FirstOrDefaultAsync(u => false, ct);
        
        return user?.ClearSensitiveData();
*/
    }

    public async Task<User?> AuthorizeAsync(LoginModel model, CancellationToken ct = default) {
        var user = await Table
            .Include(u => u.RoleClaims)
            .ThenInclude(rc => rc.Role)
            .AsSplitQuery() // <--- this is the magic
            .FirstOrDefaultAsync(u => u.Email == model.Email, ct);

        if (user is null) return null;

        if (!User.VerifyPassword(model.Password, user.PasswordHash)) return null!;

        return user.ClearSensitiveData();
    }
    
    public async Task UpdateInfoAsync(User user, CancellationToken ct = default) {
        // check if email is already taken
        var emailExists = await Table.AnyAsync(u => u.Email == user.Email && u.Id != user.Id, ct);
        if (emailExists) throw new DuplicateEmailException();

        // update user
        await UpdateAsync(user, ct);
    }

    public async Task<string> GetHiddenEmail(string email, CancellationToken ct = default)
    {
        var parts = email.Split("@");

        var hiddenemail = "";

        hiddenemail += parts[0][..1];
        
        for (int i = 0; i < parts[0][..^1].Length - 1; i++)
        {
            hiddenemail += "*";
        }

        hiddenemail += parts[0][^1..];

        hiddenemail += "@";
        
        hiddenemail += parts[1].Substring(0, 1);
        
        for (int i = 0; i < parts[1][..^1].Length - 1; i++)
        {
            hiddenemail += "*";
        }

        hiddenemail += parts[1].Substring(parts[1].Length - 1);

        return hiddenemail;
    }
}