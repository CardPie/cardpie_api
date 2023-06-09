# CardPie API

CardPie API is the backend component of the CardPie flashcard application. It provides the necessary endpoints and functionality to manage decks, flashcards, and user accounts.

## Features

- **Deck Management**: Create, retrieve, update, and delete decks.
- **Flashcard Management**: Add, retrieve, update, and delete flashcards within decks.
- **User Authentication**: Register user accounts and authenticate users using tokens.
- **Authorization**: Implement role-based access control to restrict API endpoints based on user roles.
- **API Documentation**: Generate and provide API documentation for developers to interact with the CardPie API.

## Installation

To run the CardPie API locally, follow these steps:

1. Clone the repository: `git clone https://github.com/CardPie/cardpie_api.git`
2. Navigate to the project directory: `cd API`
3. Install the required packages: `dotnet restore`
4. Set up the database connection: Update the database connection string in the `appsettings.json` file.
5. Apply database migrations: `dotnet ef database update`
6. Start the application: `dotnet run`
7. The API will be available at: `http://localhost:5000`

## Technologies Used

- ASP.NET Core: A cross-platform framework for building APIs using C#.
- Entity Framework Core: An object-relational mapper (ORM) for interacting with the database.
- JWT Authentication: JSON Web Tokens for user authentication and authorization.
- Repository Design Pattern: A design pattern that separates data access logic from business logic by using repositories.
- Swagger: A tool for generating API documentation and providing an interactive UI for testing endpoints.

## API Documentation

The CardPie API provides API documentation using Swagger. You can access the API documentation by navigating to `http://localhost:5000/swagger` after starting the application.

## Contributing

Contributions to the CardPie API are welcome! If you have any ideas, suggestions, or bug reports, please open an issue or submit a pull request.

## License

CardPie API is released under the [MIT License](LICENSE).
