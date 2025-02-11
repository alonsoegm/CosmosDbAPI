using Microsoft.Azure.Cosmos;

namespace CosmosDbAPI.Services.Interfaces;

/// <summary>
/// Defines administrative operations for managing Azure Cosmos DB resources,
/// including creating and deleting databases and containers.
/// </summary>
public interface ICosmosAdminService
{
	/// <summary>
	/// Creates a new container in the current database if it does not already exist.
	/// </summary>
	/// <param name="containerName">The name of the container to create.</param>
	/// <param name="partitionKey">The partition key for the container.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains the created or existing <see cref="Container"/> object.
	/// </returns>
	Task<Container> CreateContainerAsync(string containerName, string partitionKey);

	/// <summary>
	/// Deletes the specified container from the current database.
	/// </summary>
	/// <param name="containerName">The name of the container to delete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// </returns>
	Task DeleteContainerAsync(string containerName);

	/// <summary>
	/// Creates a new database if it does not already exist.
	/// </summary>
	/// <param name="databaseName">The name of the database to create.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains the created or existing <see cref="Database"/> object.
	/// </returns>
	Task<Database> CreateDatabaseAsync(string databaseName);

	/// <summary>
	/// Deletes the specified database.
	/// </summary>
	/// <param name="databaseName">The name of the database to delete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// </returns>
	Task DeleteDatabaseAsync(string databaseName);
}