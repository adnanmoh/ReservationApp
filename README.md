# 🗓️ Reservation System

A modular ASP.NET Core–based reservation system designed to handle real-time bookings using a clean, maintainable structure.  
It includes layered architecture, real-time communication with SignalR, and supports scalable entity and DTO mapping.

## 📌 Overview

This project allows users to manage and process reservations with real-time communication features.  
It’s suitable for applications like restaurant bookings, event scheduling, seat reservation systems, etc.

Key Capabilities:
- Create, update, and manage reservations
- Real-time updates using SignalR (via `Hubs`)
- Data validation and transformation using DTOs
- Clean separation of concerns using interfaces and repositories

## 🧱 Project Structure

### `Controllers/`
Handles HTTP endpoints related to reservation management.

### `Models/`
Contains core entities like:
- `Reservation`
- `User`
- `Schedule`
- `Table` or `Room` (depending on use case)

### `DTOs/`
Data Transfer Objects used to expose only necessary data and improve security/performance.

### `Hubs/`
Implements SignalR real-time communication channels (e.g., reservation status, live updates).

### `Repositories/`
Implements data access logic using repository pattern.

### `Interfaces/`
Defines contracts for all services and repositories used in the system.

### `MapperHelper/`
AutoMapper profiles for mapping between Models and DTOs.

### `Data/`
Includes `DbContext` and EF Core configuration files.

### `Migrations/`
Entity Framework Core migration scripts for database versioning.

## 🚀 Getting Started

### Prerequisites

- .NET 6 or 8 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio or VS Code

### Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/adnanmoh/ReservationSystem.git
   ```

2. Configure connection string in `appsettings.json`.

3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Access Swagger (if configured) or test using Postman.

## 📡 Technologies Used

- ASP.NET Core MVC / Web API
- Entity Framework Core
- SignalR (real-time)
- AutoMapper
- Repository Pattern

## 🧪 Testing

You can add unit and integration tests by mocking the repositories and services via the defined interfaces.

## 📄 License

This project is licensed under the MIT License.
