using Models;
using Newtonsoft.Json;

namespace AndreTurismAPP.TicketServices.Services
{
    public class CustomerService
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<List<Customer>>? GetCustomer()
        {
            try
            {
                HttpResponseMessage response = await CustomerService.client.GetAsync("https://localhost:7110/api/Customers");
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<Customer>>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public static async Task<Customer>? PostCustomer(Customer customer)
        {
            try
            {
                HttpResponseMessage response = await CustomerService.client.PostAsJsonAsync("https://localhost:7110/api/Customers/", customer);
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<Customer>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
