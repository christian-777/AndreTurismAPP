using Models;
using Newtonsoft.Json;

namespace AndreTurismAPP.PackageServices.Services
{
    public class HotelService
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<List<Hotel>>? GetHotel()
        {
            try
            {
                HttpResponseMessage response = await HotelService.client.GetAsync("https://localhost:7142/api/Hotels");
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<Hotel>>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public static async Task<Hotel>? PostHotel(Hotel hotel)
        {
            try
            {
                HttpResponseMessage response = await HotelService.client.PostAsJsonAsync("https://localhost:7142/api/Hotels", hotel);
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<Hotel>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
