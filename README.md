# Restaurant Management System

A comprehensive web-based Restaurant Management System developed using ASP.NET and SQL Server. The system streamlines restaurant operations including menu management, table reservations, order processing, customer management, staff administration, and feedback collection.

---

# Features

## Authentication & User Management

* User Registration
* Secure Login & Logout
* Role-Based Access Control
* Profile Management
* Account Activation/Deactivation

---

# Customer Module

## Dashboard

* View available menu items
* Browse food categories
 

## Food Ordering

* Browse restaurant menu
* Add items to cart
* Place table orders
* Track order status

## Order History

* View previous orders
* Check payment status

## Feedback System

* Submit ratings and reviews
* Share dining experience

 

# Admin Module

## Dashboard

* Monitor restaurant activities
* View customer statistics
* Manage staff and operations

## Category Management

* Add food categories
* Update category information
* Delete categories
 

## Menu Management

* Add new menu items
* Update menu information
* Upload food images
 

## Table Management

* Add restaurant tables
* Update table capacity
* Manage table availability
* Monitor reservation status

## Reservation
*Do Reservation for Customer

## Customer Management

* View customer information
* Track customer activities
* Manage customer records

## Staff Management
* Assign Admins
 

## Order Management

* View assigned orders
* Update order status
* Process customer requests
* Update Customer Payment Status

## Customer Service

* Assist customers during dining
* Manage table services

## Performance Tracking

* Track handled orders
* Monitor ratings and performance
 

# Database Structure

## Tables

### Users

Stores user account information:

* ID (Primary Key)
* Email
* Username
* Password
* Role
* Phone
* Age
* Gender
* Active Status

### Customer

Stores customer details:

* Id (Primary Key)
* Address
* TotalOrders
* CreatedAt
* UpdatedAt

### Admin

Stores administrator information:

* ID (Primary Key)
* Joined_At
* Employees_Assigned
* NID_Number
* Qualifications

### Waiter

Stores waiter information:

* ID (Primary Key)
* Stuff_Duty_ID
* Monthly_Salary
* Bank_AC_Number
* Joined_At
* Total_Orders_Handled
* Rating
* Assigned_By

### Categories

Stores food categories:

* ID (Primary Key)
* Name
* Description
* Active

### MenuItems

Stores menu information:

* ID (Primary Key)
* Category_ID
* Name
* Price
* Description
* Quantity
* Image_URL
* Active Status

### CartItem

Stores shopping cart items:

* MenuItemId
* Name
* Price
* Quantity
* Total

### Restaurant_Table

Stores restaurant table information:

* ID (Primary Key)
* Table_Number
* Capacity
* Status

### Reservation

Stores reservation records:

* ID (Primary Key)
* Customer_ID
* Table_ID
* Reservation_Time
* Number_Of_People
* Status

### Table_Order

Stores customer orders:

* ID (Primary Key)
* Table_ID
* Customer_ID
* Waiter_ID
* Status
* Total_Amount
* Payment_Status

### Order_Details

Stores ordered food items:

* ID (Primary Key)
* Order_ID
* MenuItem_ID
* Item_Name
* Price
* Quantity

### Feedback

Stores customer feedback:

* ID (Primary Key)
* Ratings
* Comments
* Customer_ID

---

# Technology Stack

## Frontend

* HTML5
* CSS3
* Bootstrap 5
* JavaScript
* Razor Views

## Backend

* ASP.NET Core MVC
* C#

## Database

* Microsoft SQL Server

## Development Tools

* Visual Studio 2022
* SQL Server Management Studio (SSMS)
* Git & GitHub

---

# Core Functionalities

### Menu Management

* Create, Read, Update, Delete menu items
* Category-wise food organization
* Food image support

### Reservation System

* Online table reservation
* Capacity management
* Reservation status tracking

### Order Processing

* Cart management
* Table-based ordering
* Payment tracking

### Customer Feedback

* Ratings system
* Customer reviews
* Service quality monitoring

 
 

---

# Installation Guide

## Clone Repository

```bash
git clone https://github.com/your-username/Restaurant-Management-System.git
```

## Database Setup

1. Open SQL Server Management Studio (SSMS).
2. Create a database named:

```sql
RestaurantManagementDB
```

3. Execute the provided SQL script (`RestaurantManagementDB.sql`).

## Configure Connection String

Update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=RestaurantManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## Run the Project

1. Open the solution in Visual Studio.
2. Restore NuGet Packages.
3. Build the solution.
4. Run the application.

---

# Future Enhancements

* Online Payment Gateway Integration
* Real-Time Order Tracking
* QR Code Menu System
* Inventory Management
* Delivery Management
* Email & SMS Notifications
* Restaurant Analytics Dashboard
* Mobile Application Support

---

# Project Objective

The Restaurant Management System aims to automate restaurant operations by providing an efficient platform for menu management, table reservations, order processing, customer feedback collection, and staff administration, ultimately improving customer satisfaction and operational efficiency.

---

# License

This project is developed for academic and educational purposes.
