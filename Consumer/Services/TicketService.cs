using System.Net.Http.Json;
using Models;
using Newtonsoft.Json;

namespace AndreTurismAPP.Services
{
    public class TicketService
    {
        static readonly HttpClient client = new HttpClient();
        //public static async Task<List<Ticket>>? GetTicket()
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await TicketService.client.GetAsync("https://localhost:7251/api/Tickets");
        //        response.EnsureSuccessStatusCode();
        //        string ender = await response.Content.ReadAsStringAsync();
        //        var end = JsonConvert.DeserializeObject<List<Ticket>>(ender);
        //        return end;
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        return null;
        //    }
        //}

        public static async Task<Ticket>? PostTcket(Ticket ticket)
        {
            try
            {
                HttpResponseMessage response = await TicketService.client.PostAsJsonAsync("https://localhost:7151/api/Tickets", ticket);
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<Ticket>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
