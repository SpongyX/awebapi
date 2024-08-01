using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

[Route("api/[controller]")]
[ApiController]
public class ChatbotController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] JObject data)
    {
        // Check if data is null
        if (data == null)
        {
            return BadRequest("Invalid data.");
        }

        // Check if queryResult is present
        var queryResult = data["queryResult"];
        if (queryResult == null)
        {
            return BadRequest("Missing 'queryResult' in the request.");
        }

        // Check if intent is present
        var intent = queryResult["intent"]?["displayName"]?.ToString();
        if (intent == null)
        {
            return BadRequest("Missing 'intent.displayName' in the request.");
        }

        // Default response text
        string responseText = "I didn't understand that.";

        // Check for specific intent
        if (intent == "Order Status")
        {
            responseText = "Your order is on the way!";
        }

        return Ok(new
        {
            fulfillmentText = responseText
        });
    }
}
