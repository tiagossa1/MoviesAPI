namespace Application.Common.Dtos;

public record MovieDto(long Id, string Title, decimal Budget, string HomepageUrl, string Plot, DateTime ReleaseDate,
    int RuntimeInMinutes, IEnumerable<MovieCastDto> MovieCasts, IEnumerable<GenreDto> Genres);