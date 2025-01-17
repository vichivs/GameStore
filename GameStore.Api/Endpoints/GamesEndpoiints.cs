using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoiints
    {
        const string GetGameendpointName = "GetGame";

        public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
        {

            var group = routes.MapGroup("/games")
                              .WithParameterValidation();

            group.MapGet("", (IGamerepository repository) => 
                repository.GetAll().Select(game => game.AsDto()));

            group.MapGet("/{id}", (IGamerepository repository, int id) => 
            {
            Game? game  = repository.Get(id);
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
            })
            .WithName(GetGameendpointName);

            group.MapPost("/", (IGamerepository repository, CreateGameDto gameDto) =>
            {
                Game game = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageUri = gameDto.ImageUri
                };

                repository.Create(game);
                return Results.CreatedAtRoute(GetGameendpointName, new {id = game.Id}, game);
            });

            group.MapPut("/{id}", (IGamerepository repository, int id, UpdateGameDto updatedGameDto) => 
            {
                Game? existingGame  = repository.Get(id);
                if(existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updatedGameDto.Name;
                existingGame.Genre = updatedGameDto.Genre;
                existingGame.Price = updatedGameDto.Price;
                existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
                existingGame.ImageUri = updatedGameDto.ImageUri;

                repository.Update(existingGame);

                return Results.NoContent();
            });

            group.MapDelete("/{id}", (IGamerepository repository, int id) =>
            {
                Game? game  = repository.Get(id);
                if(game is not null)
                {
                    repository.Delete(id);
                }

                return Results.NoContent();
            });
            return group;
        }
    }
}