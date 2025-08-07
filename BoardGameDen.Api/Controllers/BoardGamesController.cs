using BoardGameDen.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardGameDen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly IBoardGameService _boardGameService;

        public BoardGamesController(IBoardGameService boardGameService)
        {
            _boardGameService = boardGameService;
        }

        [AllowAnonymous]
        [HttpGet]

       public async Task<IActionResult> GetBoardGames(
       [FromQuery] string? search = "",
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 800)
        {
            var (boardGames, totalCount) =
                await _boardGameService.GetBoardGamesAsync(search ?? "", page, pageSize);

            return Ok(new
            {
                boardGames,
                totalCount
            });
        }

    }


}