# E-Commerce Backend Development

## Objective

This project is the backend development for an e-commerce platform using .NET technologies. The backend is designed to handle core functionalities such as product management, user management, order processing, shopping cart management, payment integration, and generating reports & analytics.

## Core Functionalities

1. **Product Management**:
   - APIs for adding, updating, deleting, and retrieving product details.
   - Product attributes include categories, descriptions, pricing, stock levels, and images.

2. **User Management**:
   - Authentication and authorization using JWT for secure user access.
   - APIs for user registration, login, profile updates, and password management.

3. **Order Processing**:
   - Modules for managing orders: order placement, tracking, and status updates (Pending, Shipped, Delivered).
   - Invoice generation for completed orders.

4. **Shopping Cart**:
   - Endpoints for adding items to the cart, updating quantities, and removing items.
   - Functionality to save carts for logged-in users.

5. **Payment Integration**:
   - Simulated payment processing with mock APIs for Credit/Debit Cards and third-party gateways like PayPal and Stripe.

6. **Reports and Analytics**:
   - APIs for sales reports, product performance, and user activity analytics.

## Technical Requirements

- **Framework**: ASP.NET Core 7.0
- **Database**: SQL Server with proper indexing and optimization for large datasets.
- **API Documentation**: Swagger UI for testing and documentation.
- **Unit Testing**: Unit tests for all critical functionalities.

## Database Design

### Users Table
Stores information about users.

| Column        | Data Type | Description |
|---------------|-----------|-------------|
| `UserId`      | INT       | Primary Key |
| `Username`    | NVARCHAR  | Unique Username |
| `Email`       | NVARCHAR  | Unique Email |
| `PasswordHash`| NVARCHAR  | Encrypted Password |
| `Role`        | NVARCHAR  | User Role (Admin, Customer) |
| `CreatedAt`   | DATETIME  | Account Creation Date |
| `UpdatedAt`   | DATETIME  | Last Update Date |

### Products Table
Stores details about products.

| Column        | Data Type | Description |
|---------------|-----------|-------------|
| `ProductId`   | INT       | Primary Key |
| `Name`        | NVARCHAR  | Product Name |
| `Description` | NVARCHAR  | Product Description |
| `Price`       | DECIMAL   | Product Price |
| `Stock`       | INT       | Product Stock Level |
| `CategoryId`  | INT       | Foreign Key referencing `Categories.CategoryId` |
| `ImageURL`    | NVARCHAR  | Product Image URL |
| `CreatedAt`   | DATETIME  | Product Creation Date |
| `UpdatedAt`   | DATETIME  | Last Update Date |

### Categories Table
Stores categories for products.

| Column        | Data Type | Description |
|---------------|-----------|-------------|
| `CategoryId`  | INT       | Primary Key |
| `Name`        | NVARCHAR  | Category Name |
| `Description` | NVARCHAR  | Category Description |

### Orders Table
Stores information about orders.

| Column        | Data Type | Description |
|---------------|-----------|-------------|
| `OrderId`     | INT       | Primary Key |
| `UserId`      | INT       | Foreign Key referencing `Users.UserId` |
| `OrderDate`   | DATETIME  | Date when the order was placed |
| `Status`      | NVARCHAR  | Order Status (e.g., Pending, Shipped, Delivered) |
| `TotalAmount` | DECIMAL   | Total amount for the order |

### OrderDetails Table
Stores line item details for each order.

| Column        | Data Type | Description |
|---------------|-----------|-------------|
| `OrderDetailId`| INT      | Primary Key |
| `OrderId`      | INT      | Foreign Key referencing `Orders.OrderId` |
| `ProductId`    | INT      | Foreign Key referencing `Products.ProductId` |
| `Quantity`     | INT      | Quantity of the product |
| `UnitPrice`    | DECIMAL  | Price per unit of the product |

### ShoppingCart Table
Stores the shopping cart for users.

| Column        | Data Type | Description |
|---------------|-----------|-------------|
| `CartId`      | INT       | Primary Key |
| `UserId`      | INT       | Foreign Key referencing `Users.UserId` |
| `ProductId`   | INT       | Foreign Key referencing `Products.ProductId` |
| `Quantity`    | INT       | Quantity of the product in the cart |
| `CreatedAt`   | DATETIME  | Date when the item was added to the cart |

### Payments Table
Stores payment details for completed orders.

| Column        | Data Type | Description |
|---------------|-----------|-------------|
| `PaymentId`   | INT       | Primary Key |
| `OrderId`     | INT       | Foreign Key referencing `Orders.OrderId` |
| `PaymentMethod`| NVARCHAR | Payment Method (e.g., CreditCard, PayPal) |
| `PaymentDate` | DATETIME  | Date of payment |
| `Amount`      | DECIMAL   | Payment Amount |
| `Status`      | NVARCHAR  | Payment Status (e.g., Success, Failed) |

## Data Flow & Database Changes

### Shopping Cart:
- When a user adds a product, it gets stored in the `ShoppingCart` table.
- The `ShoppingCart` table is unique per user, allowing them to store items while they browse.
- Upon checkout, the data is moved from the `ShoppingCart` to the `OrderDetails` table.

### Orders & OrderDetails:
- The `Orders` table stores the overall order information (status, total, etc.).
- The `OrderDetails` table stores each product that was part of the order (quantity, price, etc.).

### Payments:
- Once an order is successfully processed and payment is completed, the `Payments` table is updated with payment details.

## API Documentation

This project includes Swagger UI for testing and documentation. You can access it by running the application and navigating to `/swagger` in your browser.

## Unit Testing

Unit tests have been written for all critical functionalities including:
- User authentication and authorization
- Product management APIs
- Shopping cart operations
- Order processing and payment handling

## Installation and Setup

1. Clone the repository.
2. Navigate to the project folder.
3. Open the solution file in Visual Studio or your preferred IDE.
4. Ensure that SQL Server is running and the database connection string is configured correctly in the `appsettings.json` file.
5. Run the application using `dotnet run` or through Visual Studio.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

