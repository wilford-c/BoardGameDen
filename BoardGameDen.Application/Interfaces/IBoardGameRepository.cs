using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameDen.Domain.Entities;

namespace BoardGameDen.Application.Interfaces
{
    public interface IBoardGameRepository
    {
       
        Task<(List<BoardGame> BoardGames, int TotalCount)> GetBoardGamesAsync(string search, int page, int pageSize);

    }


}
