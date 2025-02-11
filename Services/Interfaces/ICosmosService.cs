

using CosmosDbAPI.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosDbAPI.Services.Interfaces
{
	/// <summary>
	/// Defines the contract for operations on Azure Cosmos DB.
	/// </summary>
	public interface ICosmosService
	{
		/// <summary>
		/// Creates a new item in the Cosmos DB container.
		/// </summary>
		/// <param name="item">The item to create.</param>
		/// <returns>The created item.</returns>
		Task<CosmosItem> CreateItemAsync(CosmosItem item);

		/// <summary>
		/// Reads an item from the Cosmos DB container.
		/// </summary>
		/// <param name="id">The identifier of the item.</param>
		/// <param name="partitionKey">The partition key value of the item.</param>
		/// <returns>The requested item.</returns>
		Task<CosmosItem> ReadItemAsync(string id, string partitionKey);

		/// <summary>
		/// Updates an existing item in the Cosmos DB container.
		/// </summary>
		/// <param name="id">The identifier of the item to update.</param>
		/// <param name="item">The item with updated values.</param>
		/// <param name="partitionKey">The partition key value of the item.</param>
		/// <returns>The updated item.</returns>
		Task<CosmosItem> UpdateItemAsync(string id, CosmosItem item, string partitionKey);

		/// <summary>
		/// Deletes an item from the Cosmos DB container.
		/// </summary>
		/// <param name="id">The identifier of the item to delete.</param>
		/// <param name="partitionKey">The partition key value of the item.</param>
		Task DeleteItemAsync(string id, string partitionKey);

		/// <summary>
		/// Queries items from the Cosmos DB container using a SQL query.
		/// </summary>
		/// <param name="query">The SQL query string.</param>
		/// <returns>A collection of items matching the query.</returns>
		Task<IEnumerable<CosmosItem>> QueryItemsAsync(string query);


		Task<Course> CreateCourseAsync(Course course);
	}
}
