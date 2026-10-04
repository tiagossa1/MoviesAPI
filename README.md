# MoviesAPI

A REST API about movies, built with ASP.NET Core (.NET 10) and SQLite. I use it as a base for front-end projects and for trying out architecture ideas.

## Structure

The solution has five projects: Domain, Application, Infrastructure, IoC and WebAPI.

- Commands and queries go through MediatR, and FluentValidation runs as a MediatR pipeline behaviour.
- Data access uses Dapper. FluentMigrator creates the tables and seeds genders and genres.
- Handlers return FluentResults, which the controllers turn into HTTP responses.
- Swagger, health checks with a UI dashboard, CORS and an in-memory cache are set up in `Program.cs` and the IoC project.
- 74 NUnit tests cover the command validators. A GitHub Actions workflow builds the API and runs them on every push and pull request to `main`.

## Endpoints

| Method | Route | Description |
|---|---|---|
| GET | `/api/movies` | List movies |
| POST | `/api/movies` | Create a movie |
| DELETE | `/api/movies/{id}` | Delete a movie |
| GET | `/api/people` | List people |
| POST | `/api/people` | Create people |
| DELETE | `/api/people/{id}` | Delete a person |
| POST | `/api/person` | Create a person |
| GET | `/api/genres` | List genres |
| GET | `/api/genders` | List genders |
| GET | `/health` | Health check |

## Running it

You need the .NET 10 SDK.

```bash
git clone https://github.com/tiagossa1/MoviesAPI.git
cd MoviesAPI/MoviesApi
dotnet run --project WebAPI --launch-profile MoviesApi
```

The launch profile sets the environment to Development, which is when the app creates `movies.db` and seeds it on startup. Swagger is at `https://localhost:7212/swagger`.

To run the tests:

```bash
dotnet test Application.UnitTests
```

### Docker

From the `MoviesApi` folder:

```bash
docker build -f WebAPI/Dockerfile -t moviesapi .
docker run -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development moviesapi
```

Migrations only run in Development, so the container needs that environment variable to create the database. Swagger is then at `http://localhost:8080/swagger`.

Outside Development the API rate limits requests by IP (5 per hour, configured in `appsettings.json`) and Swagger is off.

## Database schema

![movies_schema](https://github.com/tiagossa1/MoviesAPI/assets/39096494/67c4b82d-6f46-44c5-af22-5d4a3bc6f63c)

The schema comes from the [Database Star sample movies database](https://www.databasestar.com/sample-database-movies/), simplified to make it easier to implement.

The API has no authentication.
