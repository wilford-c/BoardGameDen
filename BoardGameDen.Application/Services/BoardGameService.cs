using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameDen.Application.DTOs;
using BoardGameDen.Application.Interfaces;

namespace BoardGameDen.Application.Services
{
    public class BoardGameService : IBoardGameService
    {
        private readonly IBoardGameRepository  _boardGameRepository;

        public BoardGameService(IBoardGameRepository boardGameRepository)
        {
            _boardGameRepository = boardGameRepository;
        }

        public async Task<(List<BoardGameDto> BoardGames, int TotalCount)> GetBoardGamesAsync(string search, int page, int pageSize)
        {
            var (boardGames, totalCount) = await _boardGameRepository.GetBoardGamesAsync(search, page, pageSize);

            var filteredGames = string.IsNullOrWhiteSpace(search)
                ? boardGames
                : boardGames.Where(g => g.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            var pagedGames = filteredGames
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(g => new BoardGameDto
                {
                    Name = g.Name,
                    Thumbnail = g.Thumbnail,
                    SalePrice = g.SalePrice,
                    OurPrice = g.SalePrice * 1.10m, 

                })
                .ToList();

            return (pagedGames, filteredGames.Count);
        }
    }
}
