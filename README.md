# ClientSphere CRM

ClientSphere CRM is a comprehensive, user-friendly Client Relationship Management (CRM) system designed to streamline client data management for companies. This full-stack application consists of multiple components to manage both client and employee interactions, from information capture to client communications. The CRM system is composed of the following parts:

## Tech Stack

- **Backend:** ASP.NET Core (Web API)
- **Frontend:**
  - **Employee Interface:** ASP.NET Core MVC
  - **Client Interface:** Angular
- **Database:** SQL Server
- **Authentication:** JWT Token-based authentication

## Features

### Employee Interface (Management)
- **Manage Client Information:** Employees can add, update, and view client details, including name, email, contact number, address, and profile picture.
- **Notes and Comments:** Employees can leave notes and comments on clients' profiles. Clients can view these comments and respond if needed.
- **Client Type Management:** Employees can assign a client type to each client (e.g., New Client, Important Client, Super Client, Client Removed).
  
### Client Interface
- **Secure Client Login:** Clients can securely log in to their profiles with a username and password.
- **Manage Profile:** Clients can view and edit their personal information, including contact details and profile picture.
- **Client Communication:** Clients can view company notes and leave feedback/comments in response to employee messages.

### Database Features
- **Client Table:** Stores client data, including contact information and profile picture (stored as `VARBINARY(MAX)`).
- **Client Types:** Different categories for clients (New, Important, Super, Client Removed).
- **Comments:** Employees and clients can leave and view notes/comments associated with each client.
- **Data Integrity:** SQL stored procedures for updating, retrieving, and deleting client data, with triggers to back up data when clients are deleted.

### Security Features
- **JWT Authentication:** JSON Web Tokens are used to secure API endpoints, ensuring that only authenticated users can access certain features.
- **Password Strength:** Strong password enforcement, with a minimum length of 8 characters, containing both uppercase letters and numbers.

## How to Run

### Prerequisites
- .NET Core SDK (3.1 or later)
- SQL Server instance (for database)
- Node.js (for Angular app)
- Angular CLI (for building the client-side app)

### Setting up the Database
1. Create a new database in your SQL Server instance, e.g., `ClientSphere_CRM`.
2. Run the provided SQL scripts to create the necessary tables and stored procedures.
3. Ensure the database schema is set up according to the `Clients`, `Client_Types`, `Titles`, and `Comments` tables.

### Setting up the Web API (Backend)
1. Open the solution in Visual Studio or Visual Studio Code.
2. Configure the connection string in the `appsettings.json` file to point to your SQL Server instance.
3. Build and run the application to start the Web API service.
4. Ensure that JWT authentication is properly configured.

### Setting up the Employee Management Interface (MVC)
1. Open the MVC project in Visual Studio.
2. Configure any necessary settings, including database connection strings, in `appsettings.json`.
3. Build and run the MVC project to launch the employee management interface.

### Setting up the Client Interface (Angular)
1. Navigate to the Angular project directory and install dependencies using `npm install`.
2. Set up the Angular project to communicate with the Web API by configuring the appropriate API URLs.
3. Build and run the Angular project using `ng serve`.

## Additional Features:
- **Data Integrity:** SQL Server stored procedures and triggers for efficient data management and automatic backup during client deletion.
- **Responsive Design:** The Angular client interface is fully responsive, ensuring a seamless experience on desktop and mobile devices.
- **Scalability:** The design is built with scalability in mind, allowing future expansions such as additional features or platforms.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
