# BloggingPlatform API

A RESTful API for a blogging platform built with ASP.NET Core, allowing users to manage blogs and categories.

## Prerequisites

- .NET 10.0 SDK
- SQL Server (Express or full version)

## Getting Started

### Cloning the Repository

1. Clone the repository from GitHub:

   ```bash
   git clone https://github.com/ahmedmohamedc712/BloggingPlatformAPI.git
   ```

2. Navigate to the project directory:

   ```bash
   cd BloggingPlatformAPI
   ```

### Setting up the Database

1. Ensure SQL Server is installed and running on your machine. The default connection string uses `.\SQLEXPRESS`.

2. If needed, update the connection string in `appsettings.json` to match your SQL Server configuration.

3. Apply the database migrations to create the necessary tables:

   ```bash
   dotnet ef database update
   ```

### Running the Application

1. Restore the project dependencies:

   ```bash
   dotnet restore
   ```

2. Run the application:

   ```bash
   dotnet run
   ```

The application will start, and you should see output indicating the URLs where it's running (typically `https://localhost:5001` for HTTPS).

## API Documentation

The API includes Swagger/OpenAPI documentation. Once the application is running, you can access the Swagger UI at:

`https://localhost:5001/swagger`

This provides an interactive interface to explore and test the API endpoints.

## API Endpoints

### Blogs

- `GET /api/blogs` - Retrieve all blogs. Optional query parameters:

  - `tag`: Filter blogs by tag
  - `categoryId`: Filter blogs by category (default: 1)

- `GET /api/blogs/{id}` - Retrieve a specific blog by its ID

- `POST /api/blogs` - Create a new blog. Request body should be a `CreateBlogDto` object. Optional query parameter: `categoryId` (default: 1)

- `PUT /api/blogs/{id}` - Update an existing blog. Request body: `UpdateBlogDto`

- `DELETE /api/blogs/{id}` - Delete a blog by its ID

### Categories

- `GET /api/categories` - Retrieve all categories

- `GET /api/categories/{id}` - Retrieve a specific category by its ID

- `POST /api/categories` - Create a new category. Request body: `CreateCategoryDto`

- `PUT /api/categories/{id}` - Update an existing category. Request body: `UpdateCategoryDto`

- `DELETE /api/categories/{id}` - Delete a category by its ID

## Usage

You can interact with the API using tools like:

- **Swagger UI**: For testing and exploring endpoints interactively
- **Postman**: For manual API testing
- **curl**: For command-line requests
- **HTTP files**: The project includes `BloggingPlatform.http` for testing with VS Code's REST Client extension

### Example: Creating a Blog

Using curl:

```bash
curl -X POST "https://localhost:5001/api/blogs" \
     -H "Content-Type: application/json" \
     -d '{
       "title": "My First Blog",
       "content": "This is the content of my blog post.",
       "tags": ["introduction", "dotnet"]
     }'
```

### Example: Getting All Blogs

```bash
curl -X GET "https://localhost:5001/api/blogs"
```

## Project Structure

- `Controllers/`: API controllers for blogs and categories
- `Models/`: Entity models (Blog, Category, Tag, etc.)
- `DTOs/`: Data Transfer Objects for requests and responses
- `Services/`: Business logic services
- `Data/`: Database context and configurations
- `Migrations/`: Entity Framework migrations

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## Problem Statement

This project addresses a blog management problem inspired by the challenges outlined in the https://roadmap.sh/projects/blogging-platform-api