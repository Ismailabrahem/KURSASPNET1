using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories
{
    public class UserFactory
    {
        public static User Create(UserEntity userEntity)
        {
            try
            {
                return new User
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    ProfileImage = userEntity.ProfileImage,
                    Email = userEntity.Email!,
                    UserName = userEntity.Email!,
                    PhoneNumber = userEntity.PhoneNumber,
                    Bio = userEntity.Bio,
                    AddressLine_1 = userEntity.Address?.AddressLine_1,
                    AddressLine_2 = userEntity.Address?.AddressLine_2,
                    PostalCode = userEntity.Address?.PostalCode,
                    City = userEntity.Address?.City,
                    IsExternal = userEntity.IsExternal,
                };
            }
            catch { }
            return null!;
        }
    }
}
