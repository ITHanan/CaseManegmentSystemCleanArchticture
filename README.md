Case Management System ⚖️

A modern full-stack Case Management System built with ASP.NET Core, React, Material UI, and SQL Server.
The system allows organizations to manage clients, cases, tags, notes, assignments, dashboards, and user authentication in a clean and efficient workflow.

Features 🎯
Case Management

Create, update, delete, and view detailed case information

Assign cases to users

Add notes, tags, and descriptions

Case status management (Open / In Progress / Closed)

Dashboard Analytics

Total cases

Status distribution

Cases per user

Cases per client

Recent activity list

Interactive charts (Bar & Pie Charts with Recharts)

User & Client Management

Manage users

Manage clients

Authentication & Authorization (JWT)

Secure login and registration

Search & Filter

Search cases by title

Filter cases by status

Combined search + filter functionality

Pagination for long case lists

UI/UX

Fully responsive Material UI interface

Sidebar navigation

Modern dark theme

Clean form layouts for Create / Edit Case pages

Technology Stack 💻
Frontend

React (Vite)

Material UI (MUI v5)

Axios

React Router

Recharts (analytics)

React Icons

Backend

ASP.NET Core Web API (.NET 8)

Mediator + CQRS

Clean Architecture

Entity Framework Core

SQL Server

Database

SQL Server + EF Core Migrations

Tables: Users, Clients, Cases, Notes, Tags, CaseTags

Getting Started 🚀
Prerequisites

Make sure you have installed:

Node.js 18+

.NET SDK 8

SQL Server or LocalDB

Backend Setup (ASP.NET Core API)

Navigate to backend folder:

cd CaseManagementSystem-API


Update your connection string in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=CaseDB;Trusted_Connection=True;TrustServerCertificate=True"
}


Apply EF Core migrations:

Update-Database


Run the backend:

dotnet run


API will start at:

https://localhost:7220/

Frontend Setup (React + Vite)

Navigate to frontend folder:

cd case-management-frontend


Install dependencies:

npm install


Start the development server:

npm run dev


Frontend runs at:

http://localhost:5173/

API Endpoints 📡
Authentication
Method	Endpoint	Description
POST	/api/Auth/register	Register new user
POST	/api/Auth/login	Login & receive JWT
GET	/api/Auth/users	Get all users
Cases
Method	Endpoint	Description
GET	/api/Cases/GetAllCases	Get all cases
GET	/api/Cases/Get-Case-By-Id/{id}	Get specific case
POST	/api/Cases/Create-Case	Create new case
PUT	/api/Cases/{id}/update-case-By-Id	Update case
DELETE	/api/Cases/{id}	Delete case
Clients
Method	Endpoint
GET	/api/Clients/Get-All-Client
Tags
Method	Endpoint
GET	/api/Tags/Get-All-Tags
Dashboard
Method	Endpoint
GET	/api/Statistics/dashboard
Project Structure 🧱
├── Backend (ASP.NET Core)
│   ├── Domain
│   ├── Application (CQRS, Mediator)
│   ├── Infrastructure (EF Core)
│   └── API (Controllers)
│
└── Frontend (React)
    ├── components
    ├── pages
    ├── styles
    └── router

Known Issues 🐞

Some API responses still wrap data inside { data: { data: ... } }

Material UI dark mode may require additional contrast tweaks

Large datasets may load slowly without server-side pagination

react-icons installation sometimes requires restarting npm run dev

Future Enhancements 🚧

Priority Levels (Low, Medium, High)

Attachments (Upload documents for cases)

Comment Threads

Activity Timeline

Role-Based Access Control (Admin / User)

Email notifications

Case audit log

Contributing 🤝

Contributions are welcome!

Fork the repo

Create a new branch

Commit improvements

Submit a pull request

Please write clean code and meaningful commit messages.

License 📜

This project is licensed under the MIT License.
