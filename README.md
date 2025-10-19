# E-Commerce API

ASP.NET Core Web API for e-commerce functionality with Clean Architecture, JWT authentication, and product management.

## Technologies

- **.NET 8** - Web API framework
- **Entity Framework Core** - ORM with SQLite database
- **ASP.NET Core Identity** - User authentication and management
- **JWT Bearer** - Token-based authentication
- **AutoMapper** - Object-to-object mapping
- **Swagger/OpenAPI** - API documentation
- **Clean Architecture** - Separation of concerns (Domain, Application, Infrastructure, WebApi)

## Features

- **Authentication** - User registration and JWT-based login
- **Product Management** - Browse, search, and filter products
- **Shopping Basket** - Add, update, and remove items from basket
- **Favorites** - Manage favorite products
- **DummyJSON Integration** - Sync products from external API
- **Caching** - In-memory caching for improved performance

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd mid.net
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run database migrations (if needed):
```bash
cd AbySalto.Mid
dotnet ef database update
```

4. Start the application:
```bash
dotnet run --project AbySalto.Mid
```

The API will be available at `http://localhost:5269`

## Usage

### Swagger UI

Open your browser and navigate to:
```
http://localhost:5269
```

Swagger UI provides interactive API documentation where you can test all endpoints.

### API Endpoints

#### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token
- `GET /api/auth/me` - Get current user info (requires auth)

#### Products
- `GET /api/product` - Get all products
- `GET /api/product/{id}` - Get product by ID
- `GET /api/product/search?term={term}` - Search products
- `GET /api/product/category/{category}` - Get products by category
- `POST /api/product/sync` - Sync products from DummyJSON API (requires auth)

#### Basket
- `GET /api/basket` - Get user's basket (requires auth)
- `POST /api/basket/items` - Add item to basket (requires auth)
- `PUT /api/basket/items/{itemId}` - Update item quantity (requires auth)
- `DELETE /api/basket/items/{itemId}` - Remove item from basket (requires auth)
- `DELETE /api/basket` - Clear entire basket (requires auth)

#### Favorites
- `GET /api/favorite` - Get user's favorites (requires auth)
- `GET /api/favorite/{productId}/check` - Check if product is favorite (requires auth)
- `POST /api/favorite/{productId}` - Add to favorites (requires auth)
- `DELETE /api/favorite/{productId}` - Remove from favorites (requires auth)

### Authentication Flow

1. **Register** a new user:
```bash
POST /api/auth/register
{
  "email": "user@example.com",
  "password": "Password123!",
  "firstName": "John",
  "lastName": "Doe"
}
```

2. **Login** to get JWT token:
```bash
POST /api/auth/login
{
  "email": "user@example.com",
  "password": "Password123!"
}
```

3. **Use the token** in Swagger:
   - Click the "Authorize" button (🔓)
   - Enter: `Bearer {your-token}`
   - Click "Authorize"

4. Now you can access protected endpoints!

### Initial Data Setup

To populate the database with products from DummyJSON:

1. Register and login to get a JWT token
2. Authorize in Swagger with your token
3. Execute `POST /api/product/sync` with `limit=100`
4. Products will be synced to the database

## Project Structure

```
AbySalto.Mid/
├── AbySalto.Mid.Domain/          # Domain entities and interfaces
├── AbySalto.Mid.Application/     # Business logic and DTOs
├── AbySalto.Mid.Infrastructure/  # Data access and external services
└── AbySalto.Mid/                 # WebApi and controllers
```

## Architecture

The project follows **Clean Architecture** principles:
- **Domain Layer** - Core business entities and repository interfaces
- **Application Layer** - Business logic, services, and DTOs
- **Infrastructure Layer** - EF Core repositories, database context, JWT implementation
- **WebApi Layer** - Controllers, dependency injection, and configuration

## Configuration

Edit `appsettings.json` or `appsettings.Development.json` to configure:
- Database connection string (SQLite)
- JWT settings (secret key, issuer, audience, expiration)