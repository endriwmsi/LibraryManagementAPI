# LibraryManagementAPI
This is an WebAPI using C# using RESTful pattern, where each controller is responsible for handling CRUD operations related to a specific entity. AutoMapper is used to map between the domain models and their corresponding Data Transfer Objects (DTOs) and ViewModels.

The API interacts with the database using the DataContext class, which inherits from the Entity Framework Core's DbContext class. The database has three entity sets: Books, Authors, and Users, which are mapped to their respective tables in the database.

## Features
- GET - Get all or by Id Books, Authors and Users
- POST - Add any entity
- PUT - Edit any entity by Id
- DELETE - Remove any entity by Id

## Run Locally
Clone the project
```bash
  git clone https://github.com/endriwmsi/LibraryManagementAPI.git
```

Go to the project directory
```bash
  cd LibraryManagementAPI
```

Install dependencies
```bash
  dotnet restore
```

Run Migrations
```bash
  dotnet ef migrations add InitialMigration
```

Run Database
```bash
  dotnet ef database update
```

## Deployment
Build or execute this project locally

```bash
  dotnet build / dotnet run
```

## API Reference

#### Get Authors
```http
  GET /api/authors
```
```JSON
{
  "id": 1,
  "name": "Author name",
  "books": []
}
```

#### POST Author
```http
  POST /api/authors/
```
```JSON
{
  "name": "Author name"
}
```
#### Get Books
```http
  GET /api/books/
```
```JSON
{
  "id": 1,
  "title": "Book title",
  "authors": []
}
```

#### POST Book
```http
  POST /api/books/
```
```JSON
{
  "title": "Book title",
  "authorsId": [
    0
  ]
}
```

## Authors

- [@endriwmsi](https://www.github.com/endriwmsi)
