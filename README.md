# TaskFlow Pro - Task Management System

TaskFlow Pro is a modern full-stack task management application built for the "Applied Software Testing" university course. It features a robust ASP.NET Core backend and a sleek, dark-themed responsive UI.

## Tech Stack
- **Frontend**: HTML5, CSS3 (Vanilla), JavaScript, FontAwesome
- **Backend**: ASP.NET Core 9.0 MVC
- **Database**: SQLite (Entity Framework Core)
- **Testing**: Playwright, NUnit, C#

## Features
1. **User Authentication**: Secure Login and Registration.
2. **Dashboard**: Statistics overview and recent activity.
3. **Task CRUD**: Create, Read, Update, and Delete tasks with Priority and Due Dates.
4. **Filtering & Search**: Live search and status/priority filtering.
5. **Profile Management**: Update user info and change password.

## Project Structure
- `TaskFlowPro.Web`: The main web application.
- `TaskFlowPro.Tests`: Playwright automation framework using Page Object Model (POM).

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- PowerShell (for running scripts)

### Run the Application
1. Open a terminal in the root directory.
2. Navigate to the web project: `cd TaskFlowPro.Web`
3. Run the app: `dotnet run`
4. The application will be available at `https://localhost:5001` (or the port specified in console).

### Run Automation Tests
1. Install Playwright browsers:
   ```bash
   cd TaskFlowPro.Tests
   dotnet build
   pwsh bin/Debug/net9.0/playwright.ps1 install
   ```
2. Run tests:
   ```bash
   dotnet test
   ```

## Test Data
The application seeds with the following default users:
- **Admin**: `admin@taskflow.com` / `Admin123!`
- **John Doe**: `john@example.com` / `Password123!`
- **Jane Smith**: `jane@example.com` / `Password123!`

## Automation Selectors
All key elements include `data-test` attributes for stable automation:
- `data-test="email-input"`
- `data-test="login-submit"`
- `data-test="create-task"`
- `data-test="task-row-{id}"`
