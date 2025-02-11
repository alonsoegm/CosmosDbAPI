using CosmosDbAPI.Models;
using CosmosDbAPI.Services.Interfaces;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDbAPI.Services;

/// <summary>
/// Provides operations to interact with Azure Cosmos DB.
/// </summary>
public class CosmosService : ICosmosService
{
	private readonly CosmosClient _cosmosClient;
	private readonly Container _container;
	private readonly Database _database;

	/// <summary>
	/// Initializes a new instance of the <see cref="CosmosService"/> class.
	/// </summary>
	/// <param name="configuration">The configuration instance for accessing Cosmos DB settings.</param>
	public CosmosService(IConfiguration configuration)
	{
		var connectionString = configuration["CosmosDb:ConnectionString"];
		var databaseName = configuration["CosmosDb:DatabaseName"];
		var containerName = configuration["CosmosDb:ContainerName"];

		_cosmosClient = new CosmosClient(connectionString);
		_database = _cosmosClient.GetDatabase(databaseName);
		_container = _database.GetContainer(containerName);
	}

	public async Task<CosmosItem> CreateItemAsync(CosmosItem item)
	{
		var response = await _container.CreateItemAsync(item, new PartitionKey(item.PartitionKey));
		return response.Resource;
	}

	public async Task<CosmosItem> ReadItemAsync(string id, string partitionKey)
	{
		var response = await _container.ReadItemAsync<CosmosItem>(id, new PartitionKey(partitionKey));
		return response.Resource;
	}

	public async Task<CosmosItem> UpdateItemAsync(string id, CosmosItem item, string partitionKey)
	{
		// UpsertItemAsync creates or updates an item.
		var response = await _container.UpsertItemAsync(item, new PartitionKey(partitionKey));
		return response.Resource;
	}

	public async Task DeleteItemAsync(string id, string partitionKey)
	{
		await _container.DeleteItemAsync<CosmosItem>(id, new PartitionKey(partitionKey));
	}

	public async Task<IEnumerable<CosmosItem>> QueryItemsAsync(string query)
	{
		var queryDefinition = new QueryDefinition(query);
		var items = new List<CosmosItem>();
		var iterator = _container.GetItemQueryIterator<CosmosItem>(queryDefinition);

		while (iterator.HasMoreResults)
		{
			var response = await iterator.ReadNextAsync();
			items.AddRange(response.ToList());
		}

		return items;
	}

	public async Task<Course> CreateCourseAsync(Course course)
	{
		var response = await _container.CreateItemAsync<Course>(course, new PartitionKey(course.category));
		return response.Resource;
	}



}