using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleNS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Write("Enter a command: ");
                string command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "p":
                        await Pokemon();
                        break;    
                    case "exit":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }

            Console.WriteLine("Exiting the Command Console App. Goodbye!");
        }


        public static async Task Pokemon()
        {

            // Base URL of the API you want to call
            string apiUrl = "https://pokeapi.co/api/v2/pokemon/ditto";

            // Create an instance of HttpClient to send the HTTP request
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send a GET request to the API and get the response
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Display the response content
                        Console.WriteLine("API response:");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        // Display an error message if the API call is not successful
                        Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    // Display an error message if there's an exception during the API call
                    Console.WriteLine($"API call failed with exception: {e.Message}");
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

    }
}