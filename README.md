# CookBookApp

> Simple app where you can have you recipes at one placed organized

![.NET Version](https://img.shields.io/badge/.NET-10.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10.0-blue)
![EF Core](https://img.shields.io/badge/EF%20Core-10.0-512BD4?)
![License](https://img.shields.io/badge/license-MIT-green)

## Table of Contents
- [About the Project](#about)
- [Technologies Used](#technologies)
- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Features](#features)
- [Database Setup](#database-setup)
- [Usage](#usage)
- [License](#license)

## About
This is a simple app where you can have you recipes at one placed organized. The problem the app solves is it stores recipes on one place and you can search the recipe much more easier than if its spread across multiple sheet of paper. Its build for people who want to have their recipes organized and not scattered.

## Technologies

| Technology            | Version   | Purpose                          |
|-----------------------|---------- |----------------------------------|
| ASP.NET Core MVC      | 10.0      | Web framework                    |
| Entity Framework Core | 10.0      | ORM / Database access            |
| SQL Server            | -         | Database                         |
| Bootstrap             | 5.3       | Frontend styling                 |
| Razor Pages / Views   | -         | Server-side HTML rendering       |

## Prerequisites

Make sure you have the following installed before running the project:

- [.NET SDK 10.0](https://builds.dotnet.microsoft.com/dotnet/Sdk/10.0.402/dotnet-sdk-10.0.402-win-x64.exe)
- [Visual Studio 2026](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Git](https://git-scm.com/) or [GitHubDesktop](https://desktop.github.com/download/)

## Project Structure

```
CookBookApp/
│
├── CookBookApp.Core/                   
├──── Contracts                     #Interfaces the app implements
├──── Services                      #Services that contains the business logic
├── CookBookApp.Data/                       
├──── Configurations                #Configuration on the database models
├──── Migrations                    #Version control of the implemented changes to the database
├──── ApplicationDbContex.cs        #The application DbContext
├── CookBookApp.DTOs/               #All the Data transfer object that the application uses
├── CookBookApp.GCommon/                        
├──── Constants                     #All the constant values the application uses      
├──── Enums                         #All the enums the application uses
├── CookBookApp.Models/             #Application database models
├── CookBookApp.Web/                            
├──── wwwroot/                      # Static files (CSS, JS, images)
├──── Controllers/                  # MVC Controllers
├──── Views/                        # Razor Views (.cshtml)
├──── Extensions                    # Contains application services and dbContext config.
├──── appsettings.json              # App configuration
└──── Program.cs                    # App entry point and middleware setup
```

## Features

- CRUD operations for Recipe model
- Input validation (server-side & client-side)
- Responsive UI with Bootstrap

## Database Setup
Go in CookBookApp.Web/appsettings.json and you can edit the DefaultConnection of the database

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=(Datebase);Integrated Security=True;TrustServerCertificate=True;"
}
```

## Usage

```
1.0  Navigate to Recipe/Create to create an a brand new recipe.
2.0  Navigate to the recipe you created and edit it so you can add ingredients to it.
2.1  If you you wish to remove to an ingredient you can remove it in the Edit page.
3.0  Now in Details page you can see the ingredient.
4.0  If you wish to remove the recipe either in the Edit or Index page you can do so.
```

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

*Built as part of the **ASP.NET Fundamentals** course.*


