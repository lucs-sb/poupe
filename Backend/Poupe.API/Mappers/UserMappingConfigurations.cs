using Mapster;
using Poupe.API.Models.User;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Entities;

namespace Poupe.API.Mappers;

public static class UserMappingConfigurations
{
    public static void RegisterUserMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateUserModel, UserCreateDTO>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Age, src => src.Age)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password);

        TypeAdapterConfig<UserCreateDTO, User>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Age, src => src.Age)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password);

        TypeAdapterConfig<User, UserResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Age, src => src.Age)
            .Map(dest => dest.Email, src => src.Email);

        TypeAdapterConfig<UpdateUserModel, UserUpdateDTO>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Age, src => src.Age)
            .Map(dest => dest.Email, src => src.Email);

        TypeAdapterConfig<UserUpdateDTO, User>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Age, src => src.Age)
            .Map(dest => dest.Email, src => src.Email);

        TypeAdapterConfig<(List<UserResponseDTO>, decimal, decimal, decimal), UserGetAllResponseDTO>
            .NewConfig()
            .Map(dest => dest.Users, src => src.Item1)
            .Map(dest => dest.TotalIncomes, src => src.Item2)
            .Map(dest => dest.TotalExpenses, src => src.Item3)
            .Map(dest => dest.NetBalance, src => src.Item4);

        TypeAdapterConfig<LoginModel, LoginDTO>
            .NewConfig()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password);
    }
}