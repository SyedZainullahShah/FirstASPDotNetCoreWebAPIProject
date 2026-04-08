Got it Zainullah — here is the **clean, perfect, final GitHub README.md** exactly formatted for GitHub (no extra images, no Fiverr formatting, no emojis unless you want them).

You can **copy-paste this directly into your README.md**.

---

# ASP.NET Core 8 CRUD Web API with JWT Authentication

This is a sample **ASP.NET Core 8 Web API** project demonstrating fully functional **CRUD operations** for **Employees**, **Products**, and **Categories**, secured with **JWT authentication**, integrated with **Swagger**, and deployed to **Azure App Service**.

---

## Live API (Azure)

[https://zainazurecrudapi-djeeh0dqdrdghmgt.centralus-01.azurewebsites.net/index.html](https://zainazurecrudapi-djeeh0dqdrdghmgt.centralus-01.azurewebsites.net/index.html)

---

## Technologies Used

* ASP.NET Core 8 Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger / OpenAPI
* AutoMapper
* Azure App Service Deployment

---

## Features

### CRUD Modules

* Employees
* Products
* Categories

### Authentication

* Token-based JWT authentication
* Protected endpoints
* Token validation via middleware

### API Documentation

* Swagger UI integrated
* Interactive request testing

---

## Project Structure

```
/Controllers
/Models
/Data (DbContext)
/DTOs
/Profiles (AutoMapper)
/Middlewares
```

---

## API Endpoints

### Authentication

| Method | Endpoint        | Description       |
| ------ | --------------- | ----------------- |
| POST   | /api/auth/login | Returns JWT token |

### Employees (Products and Categories follow same pattern)

| Method | Endpoint            | Description         |
| ------ | ------------------- | ------------------- |
| GET    | /api/employees      | Get all employees   |
| GET    | /api/employees/{id} | Get employee by ID  |
| POST   | /api/employees/bulk      | Create new employee |
| PUT    | /api/employees/{id} | Update employee     |
| DELETE | /api/employees/{id} | Delete employee     |
| GET    | /api/categories      | Get all categories   |
| GET    | /api/categories/{id} | Get category by ID  |
| POST   | /api/categoris/bulk      | Create new categories in bulk |
| PUT    | /api/categories/{id} | Update category     |
| DELETE | /api/categories/{id} | Delete category     |
| GET    | /api/products      | Get all products   |
| GET    | /api/products/{id} | Get product by ID  |
| POST   | /api/products/bulk      | Create new products in bulk |
| PUT    | /api/products/{id} | Update product     |
| DELETE | /api/products/{id} | Delete product     |

---

## How to Run Locally

1. Clone the repository

   ```
   git clone https://github.com/SyedZainullahShah/FirstASPDotNetCoreWebAPIProject
   ```

2. Update the SQL Server connection string in `appsettings.json`.

3. Apply migrations (if required):

   ```
   dotnet ef database update
   ```

4. Run the project:

   ```
   dotnet run
   ```

5. Open Swagger UI at:

   ```
   https://localhost:<port>/swagger
   ```

---

## Deployment (Azure)

* Published using Visual Studio Publish Profile
* Azure App Service configured with environment variables
* Connection string added in Azure Configuration
* Swagger enabled in production
* API accessible via Azure-generated URL

---

## Purpose of This Project

This project is built as a sample API to demonstrate:

* Clean Web API development
* Secure JWT authentication
* CRUD operations with EF Core
* API documentation using Swagger
* Deployment to Azure

It serves as a portfolio piece showcasing backend skills in .NET Core.

---

## Repository Link

[https://github.com/SyedZainullahShah/FirstASPDotNetCoreWebAPIProject](https://github.com/SyedZainullahShah/FirstASPDotNetCoreWebAPIProject)

---

## Author

**Zainullah Shah**
ASP.NET Core Backend Developer
