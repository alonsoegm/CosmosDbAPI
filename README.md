# Azure Cosmos DB API

This project demonstrates a comprehensive API for interacting with Azure Cosmos DB using the official Azure Cosmos DB SDK. The solution is designed to be modular, testable, and professional—mirroring the structure of our Blob Storage project.

## Overview

The API provides endpoints for common operations, including:
- **Creating, reading, updating, and deleting items.**
- **Querying items using SQL queries.**

The implementation leverages:
- **Dependency Injection** for loose coupling.
- **Interface-based design** to facilitate unit testing.
- **Standardized service responses** for consistency.

## Features

- **Create Item:** Add a new item to Cosmos DB.
- **Read Item:** Retrieve an item by its id and partition key.
- **Update Item:** Update an existing item.
- **Delete Item:** Remove an item from the database.
- **Query Items:** Execute SQL queries to retrieve items.

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- An active [Azure Cosmos DB Account](https://azure.microsoft.com/en-us/services/cosmos-db/)
- Cosmos DB settings (ConnectionString, DatabaseName, ContainerName) configured in `appsettings.json`:

    ```json
    {
      "CosmosDb": {
        "ConnectionString": "YOUR_COSMOS_DB_CONNECTION_STRING",
        "DatabaseName": "YourDatabaseName",
        "ContainerName": "YourContainerName"
      }
    }
    ```

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/yourusername/azure-cosmos-db-api.git
   cd azure-cosmos-db-api

2. Restore dependencies and build the project:
   ```bash
   dotnet restore
   dotnet build
   ```
3. Run the application:
   ```bash
   dotnet run
   ```
4. Access the API:

The API will be available at https://localhost:5001 (or another configured port). Swagger UI is available at /swagger for interactive documentation and testing.

# API Endpoints
## Cosmos DB Operations
### Create Item

- Endpoint: POST /api/cosmos
- Description: Creates a new item in Cosmos DB.
- Request: JSON payload representing the item.
- Response: A JSON object containing the created item.
  
### Read Item

- Endpoint: GET /api/cosmos/{id}?partitionKey=yourPartitionKey
- Description: Retrieves an item by its id and partition key.
- Response: A JSON object representing the item.

### Update Item

- Endpoint: PUT /api/cosmos/{id}?partitionKey=yourPartitionKey
- Description: Updates an existing item.
- Request: JSON payload with the updated item data.
- Response: A JSON object containing the updated item.

### Delete Item

- Endpoint: DELETE /api/cosmos/{id}?partitionKey=yourPartitionKey
- Description: Deletes the specified item.
- Response: A JSON object indicating the deletion status.

### Query Items

- Endpoint: GET /api/cosmos/query?query=SELECT * FROM c
- Description: Executes a SQL query against the Cosmos DB container.
- Response: A JSON array of items matching the query.

### Cosmos DB Administrative Operations

The **CosmosAdminController** provides endpoints to manage Cosmos DB resources:

- **Create Container**
  - **Endpoint:** `POST /api/cosmosadmin/container?containerName={name}&partitionKey={key}`
  - **Description:** Creates a new container with the specified name and partition key.
  - **Response:** Returns the created container object.

- **Delete Container**
  - **Endpoint:** `DELETE /api/cosmosadmin/container?containerName={name}`
  - **Description:** Deletes the container with the specified name.
  - **Response:** Returns a status message.

- **Create Database**
  - **Endpoint:** `POST /api/cosmosadmin/database?databaseName={name}`
  - **Description:** Creates a new database with the specified name.
  - **Response:** Returns the created database object.

- **Delete Database**
  - **Endpoint:** `DELETE /api/cosmosadmin/database?databaseName={name}`
  - **Description:** Deletes the database with the specified name.
  - **Response:** Returns a status message.


## Testing
### Unit Testing:
The interface-based design (using ICosmosService) allows you to easily mock dependencies and write unit tests for your controllers.

### Integration Testing:
You can also set up integration tests to verify the end-to-end behavior of the API with a real or emulator Cosmos DB instance.

### Contributing
Contributions are welcome! Please open an issue or submit a pull request if you have any improvements or feature suggestions.

### License
This project is licensed under the MIT License.
