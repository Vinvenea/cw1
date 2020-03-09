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
                var url = args.Length > 0 ? args[0] : throw new ArgumentNullException();

                var regexURL = new Regex("https/*");


                if (!regexURL.IsMatch(url))
                {
                    throw new ArgumentException();
                }


                using (var Client = new HttpClient())
                {
                    var response = await Client.GetAsync(url);
                    Console.WriteLine(response + "MOJ RESPONSE");
                    var statusCode = new Regex("Status Code: 2[0-9][0-9]*");
                    if (!statusCode.IsMatch(response.ToString()))
                    {
                        throw new Exception("Blad w pobieraniu strony");
                    }
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
            catch(ArgumentNullException e) 
            {
                Console.WriteLine(e.Message);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
