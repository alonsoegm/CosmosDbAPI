namespace CosmosDbAPI.Models;

/// <summary>
/// Represents an item stored in Azure Cosmos DB.
/// </summary>
public class CosmosItem
{
	/// <summary>
	/// Gets or sets the unique identifier for the item.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the partition key value for the item.
	/// </summary>
	public string PartitionKey { get; set; }

	/// <summary>
	/// Gets or sets the name of the item.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets a description of the item.
	/// </summary>
	public string Description { get; set; }
}