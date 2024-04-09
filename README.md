# PorcupineUnion
# User Management Application

This repository contains a User Management Application that enables CRUD operations for users, groups, and permissions. Below is an overview of the structure and functionality of the application:

## Structure

- **WebAPI Project**: Contains the backend API built using ASP.NET Core. Database connection settings are stored in the `appsettings.json`.
- **WebApp (JQuery) Project**: Frontend application built using JQuery MVC. API URLs and connection settings are stored in the `appsettings.json`.
- **Class Libraries**:
  - **UserManagement.Core**: Contains core models used throughout the application.
  - **UserManagement.Repository**: Implements repository pattern for data access.
  - **UserManagement.Services**: Implements business logic and service layer.

## Functionality

- **User Management**:
  - Allows CRUD operations for users.
  - Views and controllers are present for user management.
- **Group Management**:
  - Allows CRUD operations for groups.
  - Includes functionality for adding permissions to groups via checkbox selection.
- **Permission Management**:
  - Allows CRUD operations for permissions.

## Testing

- **Unit Tests**: Separate xUnit projects are provided for unit tests and integration tests.
- **Integration Tests**: Test scenarios involving the interaction of multiple components.
- **Generic Repository**: Includes generic repository methods for common CRUD operations.
- **Services Layer**: Implements service layer to handle business logic and interactions with repositories.

## Frontend

- **WebApp (JQuery)**:
  - Views and controllers for all models.
  - Utilizes partial views for modularization.
  - Features user interfaces for user, group, and permission management.
  - Supports adding users to groups and permissions to groups via checkboxes.

## Notes

- The application does not currently use view models; it directly references core models.
- Frontend functionality for fetching existing group permissions and users in groups needs improvement.

## Additional Notes

- Ensure database connection settings are correctly configured in `appsettings.json` for both WebAPI and WebApp projects.
