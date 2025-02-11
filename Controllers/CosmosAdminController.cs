using CosmosDbAPI.Models;
using CosmosDbAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace CosmosDbAPI.Controllers;
/// <summary>
/// Provides administrative endpoints for managing Cosmos DB resources such as databases and containers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CosmosAdminController : ControllerBase
{
	private readonly ICosmosAdminService _cosmosAdminService;
	private readonly ILogger<CosmosAdminController> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="CosmosAdminController"/> class.
	/// </summary>
	/// <param name="cosmosAdminService">The Cosmos admin service for administrative operations.</param>
	/// <param name="logger">The logger instance.</param>
	public CosmosAdminController(ICosmosAdminService cosmosAdminService, ILogger<CosmosAdminController> logger)
	{
		_cosmosAdminService = cosmosAdminService;
		_logger = logger;
	}

	/// <summary>
	/// Creates a new container in Cosmos DB with the specified name and partition key.
	/// </summary>
	/// <param name="containerName">The name of the container to create.</param>
	/// <param name="partitionKey">The partition key for the container.</param>
	/// <returns>A ServiceResponse containing the created container.</returns>
	[HttpPost("container")]
	public async Task<IActionResult> CreateContainer([FromQuery] string containerName, [FromQuery] string partitionKey)
	{
		try
		{
			Container container = await _cosmosAdminService.CreateContainerAsync(containerName, partitionKey);
			return Ok(new ServiceResponse<Container>
			{
				Data = container,
				Message = "Container created successfully."
			});
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while creating container '{ContainerName}'", containerName);
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<Container>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while creating container '{ContainerName}'", containerName);
			return StatusCode(500, new ServiceResponse<Container>
			{
				Success = false,
				Message = "An error occurred while creating the container."
			});
		}
	}

	/// <summary>
	/// Deletes an existing container from Cosmos DB.
	/// </summary>
	/// <param name="containerName">The name of the container to delete.</param>
	/// <returns>A ServiceResponse indicating the deletion status.</returns>
	[HttpDelete("container")]
	public async Task<IActionResult> DeleteContainer([FromQuery] string containerName)
	{
		try
		{
			await _cosmosAdminService.DeleteContainerAsync(containerName);
			return Ok(new ServiceResponse<string>
			{
				Data = containerName,
				Message = "Container deleted successfully."
			});
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while deleting container '{ContainerName}'", containerName);
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<string>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while deleting container '{ContainerName}'", containerName);
			return StatusCode(500, new ServiceResponse<string>
			{
				Success = false,
				Message = "An error occurred while deleting the container."
			});
		}
	}

	/// <summary>
	/// Creates a new database in Cosmos DB with the specified name.
	/// </summary>
	/// <param name="databaseName">The name of the database to create.</param>
	/// <returns>A ServiceResponse containing the created database.</returns>
	[HttpPost("database")]
	public async Task<IActionResult> CreateDatabase([FromQuery] string databaseName)
	{
		try
		{
			Database database = await _cosmosAdminService.CreateDatabaseAsync(databaseName);
			return Ok(new ServiceResponse<Database>
			{
				Data = database,
				Message = "Database created successfully."
			});
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while creating database '{DatabaseName}'", databaseName);
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<Database>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while creating database '{DatabaseName}'", databaseName);
			return StatusCode(500, new ServiceResponse<Database>
			{
				Success = false,
				Message = "An error occurred while creating the database."
			});
		}
	}

	/// <summary>
	/// Deletes an existing database from Cosmos DB.
	/// </summary>
	/// <param name="databaseName">The name of the database to delete.</param>
	/// <returns>A ServiceResponse indicating the deletion status.</returns>
	[HttpDelete("database")]
	public async Task<IActionResult> DeleteDatabase([FromQuery] string databaseName)
	{
		try
		{
			await _cosmosAdminService.DeleteDatabaseAsync(databaseName);
			return Ok(new ServiceResponse<string>
			{
				Data = databaseName,
				Message = "Database deleted successfully."
			});
		}
		catch (CosmosException cosmosEx)
		{
			_logger.LogError(cosmosEx, "CosmosException while deleting database '{DatabaseName}'", databaseName);
			return StatusCode((int)cosmosEx.StatusCode, new ServiceResponse<string>
			{
				Success = false,
				Message = $"Cosmos DB error: {cosmosEx.Message}"
			});
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while deleting database '{DatabaseName}'", databaseName);
			return StatusCode(500, new ServiceResponse<string>
			{
				Success = false,
				Message = "An error occurred while deleting the database."
			});
		}
	}
}
