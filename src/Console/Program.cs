using Repository;
using Service.Services;

namespace Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
             var jsonRepository = new JsonRepository("mongodb://root:root@localhost:27017", "congresso");
             
            bool running = true;
            DeputadoService deputadoService = new DeputadoService(jsonRepository);
                
            while (running)
            {
                System.Console.Write("Enter a command: ");
                string command = "d";//Console.ReadLine().ToLower();

                switch (command)
                {
                    case "d":
                        await deputadoService.SaveOnMongoDB();
                        break;    
                    case "exit":
                        running = false;
                        break;
                    default:
                        System.Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }

            System.Console.WriteLine("Exiting the Command Console App. Goodbye!");
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
                        System.Console.WriteLine("API response:");
                        System.Console.WriteLine(responseBody);
                    }
                    else
                    {
                        // Display an error message if the API call is not successful
                        System.Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    // Display an error message if there's an exception during the API call
                    System.Console.WriteLine($"API call failed with exception: {e.Message}");
                }
            }

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }

    }
}