using Mapster;
using Poupe.API.Models.Transaction;
using Poupe.Domain.DTOs.Transaction;
using Poupe.Domain.Entities;

namespace Poupe.API.Mappers;

public static class TransactionMappingConfigurations
{
    public static void RegisterTransactionMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateTransactionModel, TransactionCreateDTO>
            .NewConfig()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.UserId, src => src.UserId);

        TypeAdapterConfig<TransactionCreateDTO, Transaction>
            .NewConfig()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.UserId, src => src.UserId);

        TypeAdapterConfig<Transaction, TransactionResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.UserId, src => src.UserId);

        TypeAdapterConfig<UpdateTransactionModel, TransactionUpdateDTO>
            .NewConfig()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.CategoryId, src => src.CategoryId);

        TypeAdapterConfig<TransactionUpdateDTO, Transaction>
            .NewConfig()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.CategoryId, src => src.CategoryId);
    }
}
