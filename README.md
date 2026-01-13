# URL Shortener

This project is a simple full-stack URL shortener built as part of a technical assessment.

## Tech Stack
- Backend: ASP.NET Core Web API (.NET)
- Frontend: React
- Database: SQLite
- Authentication: None (kept intentionally simple)

## Features
- Create shortened URLs
- Redirect to original URLs
- Track click count
- Wallet system with profit sharing:
  - R10 per click
  - User starts at 10% share of click profit
  - Increases by 10% every 5 clicks up to a maximum of 80%

## Running the Project

### Database
This project uses SQLite with EF Core migrations.
On first run, create the database by running:
```bash
update-database

### Backend
1. Open the backend solution in Visual Studio
2. Run the project (API runs on `https://localhost:7299`)

### Frontend
```bash
cd url-shortener-ui
npm install
npm start