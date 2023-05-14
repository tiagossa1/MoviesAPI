namespace Application.Movies.Command.UpdateMovie;

public record UpdateMovieCastsCommand(long Id, long GenderId, long PersonId, string CharacterName);