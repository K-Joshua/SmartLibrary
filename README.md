# Smart Library Management System

## Project Overview

The **Smart Library Management System** is a C# application designed to help librarians efficiently manage books, users, loans, fines, and reservations. This project demonstrates the application of **Object-Oriented Programming (OOP) principles** such as classes, inheritance, polymorphism, encapsulation, abstraction, and design patterns.  

The system allows:

- Managing books and catalogues  
- Registering users (students and faculty)  
- Borrowing and returning books  
- Calculating fines for overdue books  
- Handling reservations  
- Generating basic reports  

---

## Technical Specifications

- **Programming Language:** C#  
- **Backend Framework:** ASP.NET Core (Web API) or C# Desktop App (WPF/WinForms)  
- **Database:** MySQL or PostgreSQL (via Entity Framework Core)  
- **Testing:** xUnit or NUnit  
- **Version Control:** GitHub or GitLab  

---

## OOP Design

### Classes
- `Book` – Represents books in the library.  
- `User` – Base class for library users.  
- `Student` – Derived from `User` with specific borrowing rules.  
- `Faculty` – Derived from `User` with extended borrowing privileges.  
- `Loan` – Tracks borrowed books and due dates.  
- `Fine` – Calculates penalties for overdue books.  
- `Reservation` – Manages book reservations.  
- `Catalog` – Organizes and manages book collections.  

### Inheritance
- Base class: `User`  
- Derived classes: `Student`, `Faculty`  
- Different borrowing rules implemented per user type  

### Polymorphism
- Methods like `BorrowBook()` are **overridden** for different user types  
- Supports **method overloading** where applicable  

### Encapsulation
- Private fields with **public properties** and validation logic  

### Abstraction & Interfaces
- Interfaces like `IBookRepository` and `ILoanService` provide abstraction and modularity  

### Design Patterns
- **Repository Pattern** – Separates data access from business logic  
- **Factory** – Used for modularity and scalability  

### SOLID Principles
- **Single Responsibility:** Each class has one responsibility  
- **Open/Closed:** Classes are open for extension but closed for modification  
- **Liskov Substitution:** Derived classes can replace base classes safely  
- **Interface Segregation:** Interfaces are specific and focused  
- **Dependency Inversion:** High-level modules depend on abstractions  

---

## Features

1. **Book Management:** Add, update, remove, and search books  
2. **User Management:** Register students and faculty, maintain profiles  
3. **Loan Management:** Borrow/return books, track due dates  
4. **Fines:** Automatic calculation of overdue fines  
5. **Reservations:** Reserve unavailable books  
6. **Reports:** Generate reports on borrowed books, overdue fines, and user activity  
