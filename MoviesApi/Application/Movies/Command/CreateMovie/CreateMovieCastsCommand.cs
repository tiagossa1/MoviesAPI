namespace Application.Movies.Command.CreateMovie;

public record CreateMovieCastsCommand(long GenderId, long PersonId, string CharacterName);