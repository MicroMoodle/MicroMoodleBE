# MicroMoodle Backend Services

This directory contains the core services that power the MicroMoodle backend system. Each service is designed to handle specific functionality and follows microservice architecture principles.

## Services Overview

### 1. Auth Service

Handles all authentication and authorization related operations:

- User authentication (login/logout)
- JWT token management
- Role-based access control (RBAC)
- OAuth2 integration
- Session management
- Password reset and email verification

### 2. Tenant Service

Manages multi-tenancy functionality:

- Tenant creation and management
- Tenant configuration
- Domain mapping
- Tenant-specific settings
- Tenant isolation and data separation

### 3. User Service

Handles user management operations:

- User CRUD operations
- User profile management
- User preferences
- User roles and permissions
- User activity tracking
- User search and filtering

### 4. Web Config Service

Manages system-wide configuration:

- UI/UX configurations

### 5. Course Service

Manages all course-related operations:

- Course CRUD operations
- Course enrollment
- Course content management
- Course scheduling
- Course categories and tags
- Course progress tracking
- Course analytics

### 6. File Service

Handles file operations and storage:

- File upload and download
- File storage management
- File type validation
- File compression
- CDN integration
- File access control
- File versioning

### 7. Notification Service

Manages all notification-related operations:

- Email notifications
- Push notifications
- In-app notifications
- Message
- Notification templates
- Notification preferences
- Notification delivery status
- Notification history

### 8. Payment Service

Handles all payment-related operations:

- Payment processing
- Subscription management
- Invoice generation
- Payment gateway integration
- Refund processing
- Payment history
- Financial reporting

## Service Communication

Services communicate with each other through:

- Message queues
- gRPC (for internal service communication)

## Technology Stack

- NestJS framework/Node.js/TypeScript
- ASP.NET/C#
- Spring Boot/Java
- Golang
- PostgreSQL (main database)
- Redis (caching)
- MongoDB
- MinIO/S3 (file storage)
- RabbitMQ (message broker)
- Docker (containerization)
- Kubernetes (orchestration)
- Envoy Proxy (API Gateway)
- Grafana/Prometheus (Logging monitoring)
- GraphQL
- WebSocket

## Development Guidelines

1. Each service should be independently deployable
2. Services should follow the same coding standards and patterns
3. Each service should have its own database schema
4. Services should implement proper error handling and logging
5. All services should include comprehensive unit and integration tests
6. Services should implement proper monitoring and health checks
7. Documentation should be maintained for each service
8. All database migrations for the Auth Service must be created in the folder `src/auth/AuthService.Infrastructure/Persistence/PostgreSQL` and should follow best practices for migration scripts (clear naming, idempotency, and proper rollback support).

   To create a new migration using EF Core, run the following command from the project root:

   ```bash
   dotnet ef migrations add <MigrationName> --project src/auth/AuthService.Infrastructure --output-dir Persistence/PostgreSQL
   ```

   Replace `<MigrationName>` with a descriptive name for your migration.

## Getting Started

## Contributing

Please read the contributing guidelines before submitting pull requests.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
