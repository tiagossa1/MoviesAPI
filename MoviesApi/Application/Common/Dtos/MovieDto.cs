namespace Application.Common.Dtos;

public record MovieDto(long Id, string Title, decimal Budget, string HomepageUrl, string Plot, DateOnly ReleaseDate, int RuntimeInMinutes, IEnumerable<MovieCastDto> MovieCasts, IEnumerable<GenreDto> Genres);