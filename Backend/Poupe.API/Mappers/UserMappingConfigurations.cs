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
            .Map(dest => dest.Age, src => src.Age);

        TypeAdapterConfig<UserCreateDTO, User>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Age, src => src.Age);

        TypeAdapterConfig<User, UserResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Age, src => src.Age);

        TypeAdapterConfig<UpdateUserModel, UserUpdateDTO>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Age, src => src.Age);

        TypeAdapterConfig<UserUpdateDTO, User>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Age, src => src.Age);

        TypeAdapterConfig<(List<UserResponseDTO>, decimal, decimal, decimal), UserSummaryResponseDTO>
            .NewConfig()
            .Map(dest => dest.Users, src => src.Item1)
            .Map(dest => dest.TotalIncomes, src => src.Item2)
            .Map(dest => dest.TotalExpenses, src => src.Item3)
            .Map(dest => dest.NetBalance, src => src.Item4);
    }
}