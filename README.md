# RaceDay – Race Event Management System

## 1. Project Overview

RaceDay is a web-based race event management system developed using ASP.NET Core and C#. The system is designed to support the management of running, walking and cycling events.

The project is divided into two main applications:

- **RaceDay.API:** A RESTful Web API that handles communication with the database and provides account and race management functionality.
- **RaceDay.MVC:** An ASP.NET Core MVC application that provides the website interface through which users can interact with the system.

## 2. Technologies Used

The project uses the following technologies:

- C#
- ASP.NET Core Web API
- ASP.NET Core MVC
- Entity Framework Core
- Microsoft SQL Server LocalDB
- HTML and CSS
- Bootstrap
- JSON
- JWT Authentication
- Cookie Authentication
- Git and GitHub

## 3. Project Structure

The solution contains two main projects.

### RaceDay.API

The API project contains:

- Controllers for accounts, profiles, race categories, race entries, race events and race results.
- Models representing the system's data.
- Entity Framework Core database context.
- API endpoints for communicating with the application.

### RaceDay.MVC

The MVC project contains:

- Controllers for handling website requests.
- Models used for login and registration.
- Views for the homepage, account pages and dashboards.
- Shared layout and navigation.
- An ApiService class for sending HTTP requests to the API.
- Session and cookie authentication configuration.

## 4. Authentication and User Roles

RaceDay supports two user roles:

- **Participant:** A user who can create an account and access the participant area.
- **Organiser:** A user who can access the organiser area.

The API includes registration and login endpoints.

The MVC application includes:

- Login and registration pages.
- Cookie-based authentication.
- Session storage for the API authentication token.
- Role-based navigation.
- Logout functionality.

The MVC application uses the API for account registration and login rather than directly accessing the database.

## 5. Database

The project uses Microsoft SQL Server LocalDB with Entity Framework Core Code First.

The database stores information relating to users, participant profiles, organiser profiles, race events, race categories, race entries and race results.

The database connection is configured in the API project's `appsettings.json`.

## 6. Running the Application

### Requirements

- Visual Studio with the ASP.NET and web development workload.
- .NET 8 SDK.
- Microsoft SQL Server LocalDB.
- Entity Framework Core tools.

### Setup Instructions

1. Clone or download the RaceDay repository.
2. Open the solution in Visual Studio.
3. Check the database connection string in the API project's configuration.
4. Check the API base URL in the MVC project's `appsettings.json`.
5. Ensure that the database is available and migrations have been applied.
6. Run the RaceDay.API project.
7. Run the RaceDay.MVC project.

The API and MVC applications must both be running for MVC features that communicate with the API to work.

## 7. Current Implementation Status

The following components have been implemented:

- ASP.NET Core Web API project.
- MVC website project.
- RaceDay homepage.
- API communication service.
- API account registration and login endpoints.
- MVC login and registration pages.
- Cookie authentication and session configuration.
- Role-based navigation for participants and organisers.
- Participant and organiser dashboard views.
- Logout functionality.

The MVC project builds successfully.

**Outstanding work:** Full end-to-end testing of MVC authentication, API communication and protected pages is still required. The race event, entry and result operations also require further MVC integration and testing. These features should not be considered fully verified.

## 8. Conclusion

RaceDay provides a foundation for a race event management platform. It combines a RESTful API with an MVC web application and includes account management, authentication configuration and separate user areas. Further integration and testing are required to complete and verify all application functionality.
