using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Infrastructure.Services;

public class AccountService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly DataContext _dataContext;
    private readonly IConfiguration _configuration;


    public AccountService(UserManager<UserEntity> userManager, DataContext dataContext, IConfiguration configuration)
    {
        _userManager = userManager;
        _dataContext = dataContext;
        _configuration = configuration;
    }



    public async Task<User?> GetUserAsync(ClaimsPrincipal claimsPrincipal)
    {
        var nameIdentifier = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var userEntity = await _dataContext.Users.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifier);

        if (userEntity != null) 
        {
            return UserFactory.Create(userEntity!);
        }
        return null!;
    }

    public async Task<bool> UpdatebasicInfoAsync(ClaimsPrincipal claimsPrincipal, AccountBasicInfoModel basicinfo)
    {
        try
        {
            var nameIdentifier = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userEntity = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == nameIdentifier);

            if (userEntity != null)
            {
                userEntity.FirstName = basicinfo.FirstName;
                userEntity.LastName = basicinfo.LastName;
                userEntity.PhoneNumber = basicinfo.PhoneNumber;
                userEntity.Bio = basicinfo.Biography;

                _dataContext.Update(userEntity);
                await _dataContext.SaveChangesAsync();

                return true; 
            }
        }
        catch { }
        return false;
    }


    public async Task<bool> UpdateAddressInfoAsync(ClaimsPrincipal claimsPrincipal, AccountAddressInfo addressinfo)
    {
        try
        {
            var nameIdentifier = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userEntity = await _dataContext.Users.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifier);

            if (userEntity!.Address != null)
            {
                userEntity.Address!.AddressLine_1 = addressinfo.AddressLine_1;
                userEntity.Address!.AddressLine_2 = addressinfo.AddressLine_2;
                userEntity.Address!.PostalCode = addressinfo.PostalCode;
                userEntity.Address!.City = addressinfo.City;

            }
            else
            {
                userEntity.Address = new AddressEntity
                {
                    AddressLine_1 = addressinfo.AddressLine_1,
                    AddressLine_2 = addressinfo.AddressLine_2,
                    PostalCode = addressinfo.PostalCode,
                    City = addressinfo.City,
                };
            }

            _dataContext.Update(userEntity);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch { }
        return false;
    }

    public async Task<bool> UploadUserProfileImageAsync(ClaimsPrincipal userClaims, IFormFile file)
    {
        try
        {
            if (userClaims != null && file != null && file.Length != 0) 
            {
                var user = await _userManager.GetUserAsync(userClaims);
                if (user != null)
                {
                    var fileName = $"p_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @_configuration["FilePath:ProfileUploadPath"]!, fileName);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);

                    user.ProfileImage = fileName;
                    _dataContext.Update(user);
                    await _dataContext.SaveChangesAsync();

                    return true;
                }
            }
        }
        catch { }
        return false;
    }

    public async Task<UserEntity> GetuserAsync(ClaimsPrincipal claimsPrincipal)
    {
        var user = await _userManager.GetUserAsync(claimsPrincipal);
        return user;
    }
}
