using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace FXWorker
{

    public class CardResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public int Total { get; set; }
    }

    public class Program
    {

        private const string ServerUrl = "https://demofxapp.azurewebsites.net/";


        static HttpClient client = new HttpClient();


        static async Task<List<CardResponse>> GetCardsAsync(string path)
        {
            List<CardResponse> cards = new List<CardResponse>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var _response = await response.Content.ReadAsStringAsync();
                cards = JsonConvert.DeserializeObject<List<CardResponse>>(_response);

                // TODO: Test Responce
                // cards = await response.Content.ReadAsAsync<List<CardResponse>>();

                return cards;
            }
            return null;
            // return product;
        }

        static async Task<HttpStatusCode> PostIncorrectAlignmentAsync(Array body)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/IncorrectAlignment", JsonConvert.SerializeObject(body));
            // TODO: Test Serialie

            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }


        public static void Main(string[] args)
        {

            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            var URL = "http://interview-api.azurewebsites.net/";
            // Update port # in the following line.
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {


                HashSet<int> allMatches = new HashSet<int>();

                // Steps
                // 1. Get Array of red cards
                var redCards = await GetCardsAsync("/api/Statistics/redcards");
                // 2. Get Array of yelow  cards
                var yellowCards = await GetCardsAsync("/api/Statistics/yellowcards");


                // 3. Find who has 5 yellow cards or 1 red card. (I undertood == not >=)
                // 4. Combine both by id Set
                // TODO: Refactor and add filters in Controleler for better functionality
                foreach (CardResponse card in redCards)
                {
                    if(card.Total == 1)
                    {
                        allMatches.Add(card.Id);
                    }
                }
                foreach (CardResponse card in yellowCards)
                {
                    if (card.Total == 5)
                    {
                        allMatches.Add(card.Id);
                    }
                }


                // 5. Post the cards (Send the List of Players or Managers)
                var statusCode = await PostIncorrectAlignmentAsync(allMatches.ToArray());
                Console.WriteLine($"PostIncorrectAlignmentAsync (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }




        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
