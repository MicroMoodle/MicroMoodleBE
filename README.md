# MicroMoodle Backend

A modern, scalable, and feature-rich Learning Management System (LMS) backend built with microservices architecture.

## 🚀 Features

- Multi-tenant architecture
- Role-based access control
- Course management
- File storage and management
- Real-time notifications
- Payment processing
- API Gateway with Envoy
- Monitoring and logging
- GraphQL support
- WebSocket for real-time features

## 🏗 Architecture

The system is built using a microservices architecture with the following components:

### Core Services

- **Auth Service**: Handles authentication and authorization
- **Tenant Service**: Manages multi-tenancy functionality
- **User Service**: Handles user management
- **Web Config Service**: Manages system-wide configuration
- **Course Service**: Manages course-related operations
- **File Service**: Handles file operations and storage
- **Notification Service**: Manages notifications
- **Payment Service**: Handles payment processing

### Infrastructure

- **API Gateway**: Envoy Proxy for routing and load balancing
- **Message Broker**: RabbitMQ for service communication
- **Caching**: Redis for performance optimization
- **Storage**:
  - PostgreSQL for relational data
  - MongoDB for document storage
  - MinIO/S3 for file storage
- **Monitoring**: Grafana and Prometheus
- **Containerization**: Docker and Kubernetes

## 🛠 Technology Stack

### Backend Services

- NestJS/Node.js/TypeScript
- ASP.NET/C#
- Spring Boot/Java
- Golang

### Infrastructure

- Docker
- Kubernetes
- Envoy Proxy
- RabbitMQ
- Redis
- PostgreSQL
- MongoDB
- MinIO/S3
- Grafana
- Prometheus

### API

- REST
- GraphQL
- WebSocket

## 🚀 Getting Started

### Prerequisites

- Docker and Docker Compose
- Node.js (v16 or later)
- Java 17 or later
- .NET 7 or later
- Go 1.20 or later
- Kubernetes cluster (for production)

### Development Setup

## 📚 Documentation

- [API Documentation](docs/api/README.md)
- [Architecture Overview](docs/architecture/README.md)
- [Development Guide](docs/development/README.md)
- [Deployment Guide](docs/deployment/README.md)

## 🧪 Testing

## 📦 Deployment

### Docker

```bash
# Build all services
docker-compose build

# Run all services
docker-compose up -d
```

### Kubernetes

```bash
# Deploy to Kubernetes
kubectl apply -f k8s/
```

## 🔍 Monitoring

- Grafana Dashboard: http://localhost:3000
- Prometheus: http://localhost:9090

## 🤝 Contributing

### ❔ **How to push**

- Role commit
  `{type}: #{issue_id} {subject}`
  - type: build | chore | ci | docs | feat | fix | perf | refactor | revert | style | test
  - subject: 'Write a short, imperative tense description of the change'
- Automatic: check lint and format pre-commit

- Example:

```bash
git commit -m "{type}: #{issue_id} {subject}"
```

Description
|**Types**| **Description** |
|:---| :--- |
|feat| A new feature|
|fix| A bug fix|
|docs| Documentation only changes|
|style| Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc) |
|refactor| A code change that neither fixes a bug nor adds a feature |
|perf| A code change that improves performance |
|test| Adding missing tests or correcting existing tests |
|build| Changes that affect the build system or external dependencies (example scopes: gulp, broccoli, npm) |
|ci| 'Changes to our CI configuration files and scripts (example scopes: Travis, Circle, BrowserStack, SauceLabs) |
|chore| Other changes that don't modify src or test files |
|revert| Reverts a previous commit |

### 🔗 Workflow

#### Feature Development 🚀

- **Branch Naming:** Create a branch from `dev` using the format `feature/[feature_name]`.
- **For example:** `feature/navbar`

#### Bug Fixes During Development 🐞 🧑‍💻

- **Branch Naming:** Create a branch from `dev` using the format `fixbug/[bug_name]`.
- **For example:** `fixbug/typo`

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors

- Your Name - Initial work

## 🙏 Acknowledgments

- Thanks to all contributors
- Inspired by various open-source LMS projects
