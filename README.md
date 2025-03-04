# Project Overview
    This project provides a user and product management system with features such as authentication,
    product listing, shopping cart, and order placement.
    The backend exposes REST APIs with Swagger documentation and includes logging, 
    error handling, some caching, and validation mechanisms.

## Features
### User Management
1. **Create New User**
2. **Login for Existing User**
3. **Update Existing User Details**
4. **User Validations**
   - Email Address validation
5. **Check Password Strength**

### Product Management
6. **Go To Products Page**
7. **Display all Products** 
8. **Display Categories**
9. **Filter by:**
   - Category
   - Price
   - Name

### Shopping Cart & Orders
10. **Add To Cart**
11. **Go To Shopping Bag**
12. **Display Order Sum**
13. **Remove Items from Cart**
14. **Place Order**
15. **Order Confirmation Alert** (Order ID was placed successfully)

## Functionality & Architecture
### API & Documentation
- **Swagger** for all REST calls.
- Clear separation of implemented and unimplemented functions.

### Code Practices
- **DTO Usage**: All API responses use DTOs (Data Transfer Objects).

### Logging & Error Handling
- **Logging**
  - Information and above logs stored in a file.
  - Errors trigger an email notification.
- **Error Handling**: Proper error management for all API requests.

### Configuration
- **Connection String Configuration**: Defined in a centralized configuration file.

### Performance Enhancements
- **API Request Caching**: Caching implemented to optimize performance.
- **Order Sum Validation**: Order sum consistency is checked and logged in case of a mismatch.
- **API Call Rating System**: A rating mechanism for API performance.

## Getting Started
### Prerequisites
- Install dependencies using the package manager.
- Configure the database connection string.
- Run the project and access the Swagger UI for API testing.

### Running the Project
1. Clone the repository.
2. Install dependencies.
3. Set up the database.
4. Start the backend server.
5. Use Swagger to test APIs.




