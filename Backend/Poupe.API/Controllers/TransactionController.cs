using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poupe.API.Models.Transaction;
using Poupe.API.Utils;
using Poupe.Domain.DTOs.Transaction;
using Poupe.Domain.Interfaces;

namespace Poupe.API.Controllers;

[ApiController]
[Route("api/transaction")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTransactionModel createTransactionModel)
    {
        TransactionCreateDTO transactionCreateDTO = createTransactionModel.Adapt<TransactionCreateDTO>();

        TransactionResponseDTO transactionResponseDTO = await _transactionService.CreateAsync(transactionCreateDTO);

        return Ok(transactionResponseDTO);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] string id)
    {
        Util.ValidateGuid(id);

        Guid identifier = Guid.Parse(id);

        TransactionResponseDTO transactionResponseDTO = await _transactionService.GetByIdAsync(identifier);

        return Ok(transactionResponseDTO);
    }

    [HttpGet]
    public async Task<IActionResult> GetByAllAsync()
    {
        List<TransactionResponseDTO> transactionResponseDTOs = await _transactionService.GetAllAsync();

        return Ok(transactionResponseDTOs);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransactionByIdAsync([FromRoute(Name = "id")] string id, [FromBody] UpdateTransactionModel updateTransactionModel)
    {
        Util.ValidateGuid(id);

        Guid identifier = Guid.Parse(id);

        TransactionUpdateDTO transactionUpdateDTO = updateTransactionModel.Adapt<TransactionUpdateDTO>();

        await _transactionService.UpdateAsync(identifier, transactionUpdateDTO);

        return Accepted();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdentifierAsync([FromRoute(Name = "id")] string id)
    {
        Util.ValidateGuid(id);

        Guid identifier = Guid.Parse(id);

        await _transactionService.DeleteByIdAsync(identifier);

        return NoContent();
    }
}
