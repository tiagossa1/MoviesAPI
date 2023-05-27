namespace Domain.Responses;

public record PeopleAlreadyExistResponse(string Name, bool Exists);