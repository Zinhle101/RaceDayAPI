# RaceDay API

## 1. Project Overview

RaceDay API is an ASP.NET Core Web API developed to support the management of running, walking, and cycling events. The system allows users to register and log in, while providing endpoints for managing race events, categories, entries, results, and user profiles.

The project was developed using C# and ASP.NET Core, with Entity Framework Core and SQL Server for database management.

## 2. Technologies Used

- C#
- ASP.NET Core Web API
- .NET 8
- Entity Framework Core
- SQL Server LocalDB
- Swagger / OpenAPI
- JWT (JSON Web Token) authentication
- xUnit for unit testing
- GitHub Actions for automated builds and testing

## 3. Main Features

- User registration and login.
- Password hashing during registration.
- JWT-based authentication.
- Role-based authorisation for organisers and participants.
- CRUD operations for race events.
- CRUD operations for race categories.
- Race entry management.
- Race result management.
- Organiser and participant profile retrieval.
- Automated build and test workflow using GitHub Actions.

**Note:** Profile creation and retrieval have not been fully verified after the latest changes.

## 4. Database Design

The database is managed using Entity Framework Core Code First migrations and SQL Server.

The main entities are:

- User
- RaceEvent
- RaceCategory
- RaceEntry
- RaceResult
- OrganiserProfile
- ParticipantProfile

These entities represent users, events, event categories, registrations, results, and user-specific profile information.

## 5. Authentication and Authorisation

The API uses JWT authentication to protect selected endpoints. Users can register and log in to receive a token. Protected endpoints require a valid token, while certain management operations are restricted to users with the organiser role.

Passwords are hashed before being stored in the database.

## 6. API Documentation

Swagger provides an interactive interface for viewing and testing the API endpoints.

To access Swagger during development:

1. Run the `RaceDay.API` project in Visual Studio.
2. Open the Swagger URL displayed by the application.
3. Expand an endpoint.
4. Select **Try it out** to test the request.

Protected endpoints require a valid JWT bearer token.

## 7. Running the Project

### Prerequisites

- Visual Studio with the ASP.NET and web development workload.
- .NET 8 SDK.
- SQL Server LocalDB.
- Access to the project repository.

### Setup Instructions

1. Clone or download the repository.
2. Open `RaceDay.sln` in Visual Studio.
3. Check the database connection string in `appsettings.json`.
4. Ensure SQL Server LocalDB is installed.
5. Open the Package Manager Console and run:

   `Update-Database`

   This applies the existing Entity Framework Core migrations to the configured database.

6. Build the solution.
7. Run the `RaceDay.API` project.
8. Use the Swagger interface to explore and test the endpoints.

## 8. Testing

The project includes an xUnit test project named `RaceDay.API.Tests`.

The confirmed unit test checks that a `RaceEvent` object stores its event details correctly. The test has passed locally and through the GitHub Actions workflow.

The current test coverage is limited and does not verify all controllers, database operations, or authentication scenarios.

To run the tests in Visual Studio, open **Test → Run All Tests**.

## 9. Continuous Integration

GitHub Actions is configured to build the solution and run the unit tests when changes
