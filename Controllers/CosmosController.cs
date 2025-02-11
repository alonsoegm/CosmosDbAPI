using CosmosDbAPI.Models;
using CosmosDbAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace CosmosDbAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CosmosController : ControllerBase
{
	private readonly ICosmosService _cosmosService;
	private readonly ILogger<CosmosController> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="CosmosController"/> class.
	/// </summary>
	/// <param name="cosmosService">The Cosmos DB service to handle operations.</param>
	public CosmosController(ICosmosService cosmosService, ILogger<CosmosController> logger)
	{
		_cosmosService = cosmosService;
		_logger = logger;
	}

	/// <summary>
	/// Creates a new item in Cosmos DB.
	/// </summary>
	/// <param name="item">The item to create.</param>
	/// <returns>A ServiceResponse containing the created item.</returns>
	[HttpPost]
	public async Task<IActionResult> CreateItem([FromBody] CosmosItem item)
	{
		var response = new ServiceResponse<CosmosItem>();
		try
		{
			var createdItem = await _cosmosService.CreateItemAsync(item);
			response.Data = createdItem;
			response.Message = "Item created successfully.";
			return Ok(response);
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while creating an item '{Item}'", item);
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<CosmosItem>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
	}

	/// <summary>
	/// Reads an item from Cosmos DB by its id and partition key.
	/// </summary>
	/// <param name="id">The identifier of the item.</param>
	/// <param name="partitionKey">The partition key value of the item.</param>
	/// <returns>A ServiceResponse containing the requested item.</returns>
	[HttpGet("{id}")]
	public async Task<IActionResult> GetItem(string id, [FromQuery] string partitionKey)
	{
		var response = new ServiceResponse<CosmosItem>();
		try
		{
			var item = await _cosmosService.ReadItemAsync(id, partitionKey);
			response.Data = item;
			response.Message = "Item retrieved successfully.";
			return Ok(response);
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while reading item '{ItemId}'", id);
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<CosmosItem>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
	}

	/// <summary>
	/// Updates an existing item in Cosmos DB.
	/// </summary>
	/// <param name="id">The identifier of the item to update.</param>
	/// <param name="partitionKey">The partition key value of the item.</param>
	/// <param name="item">The updated item data.</param>
	/// <returns>A ServiceResponse containing the updated item.</returns>
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateItem(string id, [FromQuery] string partitionKey, [FromBody] CosmosItem item)
	{
		var response = new ServiceResponse<CosmosItem>();
		try
		{
			var updatedItem = await _cosmosService.UpdateItemAsync(id, item, partitionKey);
			response.Data = updatedItem;
			response.Message = "Item updated successfully.";
			return Ok(response);
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while updating item '{ItemId}'", id);
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<CosmosItem>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
	}

	/// <summary>
	/// Deletes an item from Cosmos DB by its id and partition key.
	/// </summary>
	/// <param name="id">The identifier of the item to delete.</param>
	/// <param name="partitionKey">The partition key value of the item.</param>
	/// <returns>A ServiceResponse indicating the deletion status.</returns>
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteItem(string id, [FromQuery] string partitionKey)
	{
		var response = new ServiceResponse<string>();
		try
		{
			await _cosmosService.DeleteItemAsync(id, partitionKey);
			response.Data = id;
			response.Message = "Item deleted successfully.";
			return Ok(response);
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while deleting item '{ItemId}'", id);
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<string>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
	}

	/// <summary>
	/// Queries items from Cosmos DB using a SQL query.
	/// </summary>
	/// <param name="query">The SQL query string.</param>
	/// <returns>A ServiceResponse containing a collection of items matching the query.</returns>
	[HttpGet("query")]
	public async Task<IActionResult> QueryItems([FromQuery] string query)
	{
		var response = new ServiceResponse<IEnumerable<CosmosItem>>();
		try
		{
			var items = await _cosmosService.QueryItemsAsync(query);
			response.Data = items;
			response.Message = "Query executed successfully.";
			return Ok(response);
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while querying items");
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<IEnumerable<CosmosItem>>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
	}


	/// <summary>
	/// Creates a new Course in Cosmos DB.
	/// </summary>
	/// <param name="course">The course to create.</param>
	/// <returns>A ServiceResponse containing the created item.</returns>
	[HttpPost("course")]
	public async Task<IActionResult> CreateCurse([FromBody] Course course)
	{
		var response = new ServiceResponse<Course>();
		try
		{
			var createdItem = await _cosmosService.CreateCourseAsync(course);
			response.Data = createdItem;
			response.Message = "Item created successfully.";
			return Ok(response);
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while creating an item '{course}'", course);
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<Course>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
	}
}