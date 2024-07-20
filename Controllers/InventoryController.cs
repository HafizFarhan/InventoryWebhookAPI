using InventoryWebhookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace InventoryWebhookAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InventoryController : ControllerBase
	{
		private static List<InventoryItem> inventory = new List<InventoryItem>
		{
			new InventoryItem { ItemId = "item1", Quantity = 100 },
			new InventoryItem { ItemId = "item2", Quantity = 200 },
		};

		[HttpPost("transaction")]
		public IActionResult HandleTransaction([FromBody] TransactionRecord transaction)
		{
			var item = inventory.FirstOrDefault(i => i.ItemId == transaction.ItemId);
			if (item == null)
			{
				return BadRequest("Invalid item ID");
			}

			item.Quantity -= transaction.Quantity;
			// Additional recalculation logic here

			return Ok("Transaction processed and inventory recalculated");
		}

		[HttpPost("update-received")]
		public IActionResult UpdateReceivedInventory([FromBody] InventoryItem updatedItem)
		{
			var item = inventory.FirstOrDefault(i => i.ItemId == updatedItem.ItemId);
			if (item == null)
			{
				return BadRequest("Invalid item ID");
			}

			item.Quantity = updatedItem.Quantity;
			// Additional recalculation logic here

			return Ok("Received inventory updated and recalculated");
		}

		[HttpGet("current")]
		public IActionResult GetCurrentInventory()
		{
			return Ok(inventory);
		}
	}
}
