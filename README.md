# ELibrary

Library Management System is a Online Library Web Application made using **ASP.NET MVC** and Frontend built using **HTML CSS**  It is a simple, user-friendly and easy to navigate.

### FEATURES
> For Admin :
* Can *Approve* or *Reject* the request for any book that is requested by User.
* Can perform CRUD operations on Account, Book, Author, Publisher tables.
* Can View all the past Records/Requests of each user.

> For User:
* Can views all the books and Request it.
* Can view all the issued books and Return it.
* Can view all his past Records/Requests.
* Can Search book on basis of Publisher/Author Name,BookTitle etc.

> Landing Page:

![Screenshot (30)](https://user-images.githubusercontent.com/57533030/184601500-1dc9e7bb-860d-4f9d-8bbd-9e1f878eecce.png)

> Login Page:

![Screenshot (33)](https://user-images.githubusercontent.com/57533030/184601730-45868f59-d701-44a5-8299-99d10ff1e6eb.png)

> Admin Dashboard Page:

![Screenshot (35)](https://user-images.githubusercontent.com/57533030/184605364-e94b355d-9877-4abe-b7e6-7065cd7237f6.png)

> ViewBooks Page:

![Screenshot (32)](https://user-images.githubusercontent.com/57533030/184602001-10f6c1ea-c737-4000-9956-ee1944062613.png)

> Search Feature in ViewBooks Page :

![Screenshot (38)](https://user-images.githubusercontent.com/57533030/184606602-2407321e-5526-4eb1-b5c3-d119d7d9d53d.png)

## To Get Started 

### Softwares
* Microsoft Visual Studio Community 2022 (64-bit) Version **17.3.0**
* Microsoft SQL Server Management Studio Version **18.2.1**
* .NET Framework Version **5.0.17**

### Packages
>Inside your Visual Studio Navigate to Tools > NuGet Package Manager > Manage NuGet Packages for Solution and install these packages

* Microsoft.EntityFrameworkCore Version **5.0.17**
* Microsoft.EntityFrameworkCore.Design Version **5.0.17**
* Microsoft.EntityFrameworkCore.Tools Version **5.0.17**
* Microsoft.EntityFrameworkCore.SqlServer Version **5.0.17**

### Commands
> Inside your Visual Stdio Navigate to 
**Tools** > **NuGet Package Manager** > **Package Manager Console**
and enter these commands
```sh
Scaffold-DbContext "Server=localhost;Database=<DB-NAME>;Trusted_Connection=True;"-OutputDir Models (To scaffold All Database tables in your MVC Application)
```
```sh
Add provider Name
Microsoft.EntityFrameworkCore.SqlServer
```
Make sure to update appsettings.json  inside MVC App with the **Database Name** you want to give.
```sh
"DBConnection": "Server=localhost;Database=<DB-NAME>;Trusted_Connection=True"
```

> Schema Diagram

![Screenshot (31)](https://user-images.githubusercontent.com/57533030/184603854-65f1fbfb-c8c5-4a45-b88f-2d281031e8bb.png)

