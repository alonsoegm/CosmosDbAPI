using CosmosDbAPI.Services.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CosmosDbAPI.Services;
	public class CosmosAdminService : ICosmosAdminService
	{

	private readonly CosmosClient _cosmosClient;
	private readonly Database _database;

	/// <summary>
	/// Initializes a new instance of the <see cref="CosmosAdminService"/> class.
	/// </summary>
	/// <param name="configuration">The configuration instance for accessing Cosmos DB settings.</param>
	public CosmosAdminService(IConfiguration configuration)
	{
		var connectionString = configuration["CosmosDb:ConnectionString"];
		var databaseName = configuration["CosmosDb:DatabaseName"];
		var containerName = configuration["CosmosDb:ContainerName"];

		_cosmosClient = new CosmosClient(connectionString);
		_database = _cosmosClient.GetDatabase(databaseName);
	}

	public async Task<Container> CreateContainerAsync(string containerName, string partitionKey)
	{
		var containerProperties = new ContainerProperties(containerName, partitionKey);
		return await _database.CreateContainerIfNotExistsAsync(containerProperties, throughput: 400);
	}

	public async Task DeleteContainerAsync(string containerName)
	{
		await _database.GetContainer(containerName).DeleteContainerAsync();
	}

	public async Task<Database> CreateDatabaseAsync(string databaseName)
	{
		return await _cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
	}

	public async Task DeleteDatabaseAsync(string databaseName)
	{
		await _cosmosClient.GetDatabase(databaseName).DeleteAsync();
	}
}
