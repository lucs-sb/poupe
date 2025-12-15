using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poupe.Domain.DTOs.User;

public record UserGetAllResponseDTO (List<UserResponseDTO> Users, decimal TotalIncomes, decimal TotalExpenses, decimal NetBalance)
{
}
