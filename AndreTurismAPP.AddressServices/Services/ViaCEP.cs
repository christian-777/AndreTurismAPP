using Models;
using Newtonsoft.Json;

namespace AndreTurismAPP.AddressServices.Services
{
    public class ViaCEP
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<AddressDTO> GetAddress(string cep)
        {
            try
            {
                HttpResponseMessage response = await ViaCEP.client.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<AddressDTO>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
