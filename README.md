# Bloggie - Blog Site

## Overview

**Bloggie** is a full-featured blog site built using **ASP.NET Core MVC**. It provides a seamless blogging experience with distinct user roles (Admin, Super Admin, and User) to manage and interact with content. 

### Key Features:
- **Super Admin**:
  - Can create Admin users.
  - Can create, read, update, and assign posts to tags or categories.
  - Can comment on and like posts.

- **Admin**:
  - Can create, read, update, and assign posts to tags or categories.
  - Can comment on and like posts.
  - Cannot create other Admins.

- **User**:
  - Can view blog posts.
  - Can interact with posts by commenting, liking, or disliking.

---

## Tech Stack

- **ASP.NET Core MVC**
- **Identity** for authentication and role management
- **Cloudinary** for image uploading and storage
- **3-Tier Architecture**
- **Repository Pattern** for clean data access layer
- **AutoMapper** for object-to-object mapping
- **Entity Framework Core** for database interaction

---

## Project Structure

This project follows a **3-Tier Architecture** to maintain a clear separation of concerns:
1. **Presentation Layer**: Handles the user interface and frontend logic.
2. **Business Logic Layer**: Contains the core application logic and services.
3. **Data Access Layer**: Manages database operations via repositories.

---

## Setup & Installation

### Prerequisites:
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Cloudinary API Key](https://cloudinary.com/)

### Steps:
1. Clone the repository:
    ```bash
    git clone https://github.com/iHebaMohammed/Bloggie.git
    ```

2. Navigate to the project directory:
    ```bash
    cd Bloggie
    ```

3. Set up the database:
    - Update the **appsettings.json** with your SQL Server connection string.
    - Run migrations to set up the database:
    ```bash
    dotnet ef database update
    ```

4. Configure **Cloudinary**:
    - Update the **appsettings.json** with your Cloudinary credentials.

5. Run the project:
    ```bash
    dotnet run
    ```

6. Access the application:
    - Navigate to `https://localhost:5001` in your browser.

---

## Usage

- **Super Admin**:
  - Log in with Super Admin credentials.
  - Navigate to the Admin dashboard to create or manage posts and Admin users.

- **Admin**:
  - Create or update blog posts, assign tags, and manage categories.

- **User**:
  - Explore posts, leave comments, and engage with the content through likes and dislikes.

---

## Demo


https://github.com/user-attachments/assets/89b18909-0e83-44a0-83b4-898c9dacf319



---
## Contributing
Contributions are welcome! Please follow the standard GitHub workflow:

1. Fork the project.
2. Create a new branch (`feature/your-feature`).
3. Commit your changes.
4. Open a pull request.

---

## License

This project is licensed under the [MIT License](LICENSE).
