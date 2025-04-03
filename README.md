# TeamA.DevFollow.API

## Overview

TeamA.DevFollow.API is a .NET 9 web API designed to manage habits and tags. It provides endpoints for creating, updating, retrieving, and deleting habits and tags, with support for HATEOAS links and data shaping.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
  - [Tags](#tags)
  - [Habits](#habits)
- [Validation](#validation)
- [Contributing](#contributing)
- [License](#license)

## Features

- CRUD operations for habits and tags
- HATEOAS support for navigation links
- Data shaping for optimized responses
- FluentValidation for request validation
- OpenTelemetry for observability
- Docker support for containerized deployment

## Technologies

- .NET 9
- ASP.NET Core
- Entity Framework Core
- FluentValidation
- OpenTelemetry
- PostgreSQL
- Docker

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started)
- [PostgreSQL](https://www.postgresql.org/download/)

### Installation

1. Clone the repository:
    - git clone https://github.com/your-repo/TeamA.DevFollow.API.git
    - cd TeamA.DevFollow.API

2. Set up the database:
    - Update the connection string in `appsettings.json` to point to your PostgreSQL instance.

3. Apply database migrations:
    - dotnet ef database update


### Running the Application

1. Run the application:
    - dotnet run

2. Alternatively, you can use Docker:
    - docker-compose up


## API Endpoints

### Tags

- **GET /tags**: Retrieve all tags
- **GET /tags/{id}**: Retrieve a specific tag by ID
- **POST /tags**: Create a new tag
- **PUT /tags/{id}**: Update an existing tag
- **DELETE /tags/{id}**: Delete a tag

### Habits

- **GET /habits**: Retrieve all habits
- **GET /habits/{id}**: Retrieve a specific habit by ID
- **POST /habits**: Create a new habit
- **PUT /habits/{id}**: Update an existing habit
- **PATCH /habits/{id}**: Partially update a habit
- **DELETE /habits/{id}**: Delete a habit

## Validation

The project uses FluentValidation to validate incoming requests. Validators are defined for DTOs such as `CreateTagDto` and `CreateHabitDto`.

Example validation rules for `CreateTagDto`:
```
public sealed class CreateTagDtoValidator : AbstractValidator<CreateTagDto> 
{ 
    public CreateTagDtoValidator() 
    { 
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
         RuleFor(x => x.Description).MaximumLength(50); 
    } 
}
```

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Create a new Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
