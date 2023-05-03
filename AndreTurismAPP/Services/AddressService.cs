using Models;
using Newtonsoft.Json;

namespace AndreTurismAPP.Services
{
    public class AddressService
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<List<Address>>? GetAddress()
        {
            try
            {
                HttpResponseMessage response = await AddressService.client.GetAsync("https://localhost:7124/api/Addresses");
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<Address>>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public static async Task<Address>? PostAddress(Address address)
        {
            try
            {
                HttpResponseMessage response = await AddressService.client.PostAsJsonAsync("https://localhost:7124/api/Addresses", address);
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<Address>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
