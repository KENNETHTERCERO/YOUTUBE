using loan_and_sell_be.Models;
using System.Text.Json;

namespace loan_and_sell_be.Services
{
    public class BeerService: IBeerService
    {
        string pathJson = @"C:\loan-and-sell\loan-and-sell-be\loan-and-sell-be\beers.json";

        public async Task<List<Beer>> Get() {
            string content = await File.ReadAllTextAsync(pathJson);
            var beers = JsonSerializer.Deserialize<List<Beer>>(content);
            return beers;
        }
    }
}
