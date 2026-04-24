using Microsoft.AspNetCore.Mvc;

namespace ZooManagmentSystem.Controllers
{
    [Route("api/external-animals")]
    public class ExternalAnimalsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ExternalAnimalsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAnimal(string name)
        {
            var client = _httpClientFactory.CreateClient("ExternalAnimalsClient");

            var response = await client.GetAsync($"animals?name={name}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return Ok(data);
            }

            return StatusCode((int)response.StatusCode, "Error fetching animal data from external API.");
        }
    }
}
