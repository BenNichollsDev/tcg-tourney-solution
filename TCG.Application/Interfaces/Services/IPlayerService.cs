using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TCG.Application.Dtos;
using TCG.Domain.Entities;

namespace TCG.Application.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<PlayerDto?> GetByIdAsync(int playerId);

        Task<IEnumerable<PlayerDto>> GetAllAsync();

        Task<PlayerDto?> GetByAsync(Expression<Func<Player, bool>> predicate);

        Task<PlayerDto> CreateAsync(PlayerDto playerDto);

        Task<PlayerDto> UpdateAsync(PlayerDto playerDto);

        Task<PlayerDto> DeleteAsync(int id);

        Task EmailIsUniqueAsync(string email, int? currentPlayerId = null);

        Task PhoneIsUniqueAsync(string phone, int? currentPlayerId = null);
    }
}
