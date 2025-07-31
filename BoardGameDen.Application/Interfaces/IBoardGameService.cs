using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameDen.Application.DTOs;

namespace BoardGameDen.Application.Interfaces
{
    public interface IBoardGameService
    {
        Task<(List<BoardGameDto> BoardGames, int TotalCount)> GetBoardGamesAsync(string search, int page, int pageSize);
    }
}

