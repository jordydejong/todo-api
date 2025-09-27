# Todo API

A RESTful Todo API built with .NET 9.0, PostgreSQL, and Entity Framework Core.

## Features

- ✅ Full CRUD operations (Create, Read, Update, Delete)
- 🔄 Partial updates with PATCH endpoints
- 🐘 PostgreSQL database with Entity Framework Core
- 📚 Comprehensive API documentation with Swagger/OpenAPI
- 🔒 IP-based access restriction for production
- 🏗️ Clean architecture with separated DTOs and entities
- ⚡ Async/await throughout for optimal performance

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todos` | Get all todos |
| GET | `/api/todos/{id}` | Get a specific todo |
| POST | `/api/todos` | Create a new todo |
| PUT | `/api/todos/{id}` | Update an entire todo |
| PATCH | `/api/todos/{id}` | Partially update a todo |
| DELETE | `/api/todos/{id}` | Delete a todo |

## Quick Start

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd tasks-api/TodoApi
   ```

2. **Configure the database**

   Update `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=todoapi;Username=your_username;Password=your_password"
     }
   }
   ```

3. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Start the API**
   ```bash
   dotnet run
   ```

5. **Access Swagger documentation**

   Open your browser to: `https://localhost:5001/swagger`

## Todo Model

```json
{
  "id": 1,
  "name": "Complete project documentation",
  "description": "Write comprehensive README and API docs",
  "status": "In Progress",
  "date": "2024-01-15",
  "assignee": "John Doe",
  "creator": "Jane Smith"
}
```

## Example Usage

### Create a Todo
```bash
curl -X POST "https://localhost:5001/api/todos" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Learn .NET",
    "description": "Complete the .NET tutorial",
    "status": "Not Started",
    "date": "2024-01-20",
    "assignee": "Developer",
    "creator": "Manager"
  }'
```

### Partial Update (PATCH)
```bash
curl -X PATCH "https://localhost:5001/api/todos/1" \
  -H "Content-Type: application/json" \
  -d '{
    "status": "Completed"
  }'
```

## Production Deployment

### Environment Variables

For production, override settings using environment variables:

```bash
# Database connection
export ConnectionStrings__DefaultConnection="Host=prod-host;Port=5432;Database=todoapi;Username=prod_user;Password=secure_password"

# IP restrictions (optional)
export AllowedIPs__0="203.0.113.1"
export AllowedIPs__1="203.0.113.2"

# Set production environment
export ASPNETCORE_ENVIRONMENT=Production
```

### Security

The API includes IP-based access restriction for production environments. Configure allowed IP addresses in `appsettings.json` or via environment variables.

**Note**: IP restrictions are disabled in Development mode for easier local development.

## Architecture

```
TodoApi/
├── Controllers/          # API controllers
├── Services/            # Business logic layer
├── Models/              # DTOs and data models
│   ├── Entities/        # Database entities
│   └── *.cs            # DTO classes
├── Data/               # Entity Framework DbContext
├── Extensions/         # Service collection extensions
├── Middleware/         # Custom middleware
└── Migrations/         # EF Core migrations
```

### Key Design Patterns

- **Repository Pattern**: Encapsulated via Entity Framework DbContext
- **DTO Pattern**: Separation between API models and database entities
- **Extension Methods**: Clean Program.cs organization
- **Dependency Injection**: Built-in .NET DI container
- **Record Types**: Immutable DTOs with inheritance

## Development

### Available Commands

```bash
# Run with auto-reload
dotnet watch run

# Build the project
dotnet build

# Run tests (when added)
dotnet test

# Create new migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update
```

### Adding New Features

1. Create/update entity in `Models/Entities/`
2. Update `AppDbContext` if needed
3. Create migration: `dotnet ef migrations add <Name>`
4. Update service layer in `Services/`
5. Add/update controller endpoints
6. Update DTOs in `Models/`

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License.