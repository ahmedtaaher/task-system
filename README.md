# Task Management System

A scalable task management system built with **.NET 9**, featuring JWT authentication, role-based access, background processing, and Redis caching.

---

## Features

- **User Authentication**: JWT-based register & login
- **Role-Based Access Control**:
  - Admin
  - User
- **Task Management**:
  - Create, read, update tasks
  - Update task status
- **Task Ownership**:
  - Users can only access their own tasks
- **Admin Module**:
  - View all users
  - Create users
  - Delete users
- **Background Processing**:
  - Asynchronous task handling using background worker
- **Caching**:
  - Redis caching for optimized task retrieval
- **Database Migrations**:
  - Automatic migration on startup

---

## Tech Stack

- **Framework**: ASP.NET Core (.NET 9)
- **Architecture**: Clean Architecture (N-Tier)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Caching**: Redis
- **Authentication**: JWT Bearer
- **Background Jobs**: Hosted Services
- **Containerization**: Docker & Docker Compose

---

## Project Structure

```text
TaskManagementSystem/
├── API/                # Entry point (Controllers, Program.cs)
├── Application/        # Business logic (Services, Interfaces)
├── Domain/             # Entities & Enums
├── Infrastructure/     # DB, Repositories, Redis, Security
├── docker-compose.yml  # Multi-container setup
└── README.md
```

---

## How to Run the Project

You can run the project using **Docker** or **locally**.

---

### Run with Docker (Recommended)

#### 1. Clone the repository

```bash
git clone https://github.com/your-username/task-management-system
cd TaskManagementSystem
```

---

#### 2. Run the application

```bash
docker compose up --build
```

---

#### 3. Access the applicaion

http://localhost:5000/swagger