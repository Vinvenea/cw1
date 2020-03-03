using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {

        private static async Task Main(string[] args)
        {
            try
            {
                // Console.WriteLine("Hello World!");

                var newPerson = new Person { FirstName = "Daniel" };
                var url = args.Length > 0 ? args[0] : "https://pja.edu.pl";


                using (var Client = new HttpClient())
                {
                    var response = await Client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var htmlContent = await response.Content.ReadAsStringAsync();
                        var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

                        var matches = regex.Matches(htmlContent);
                       


                        foreach (var item in matches)
                        {
                            Console.WriteLine(item.ToString());
                        }
                    }
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);

            }
        }
    }
}
