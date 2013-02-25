using EcommerceSearch;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.File.WriteAllText("SearchResult.txt", "SearchEngine\r\n");;
            string busca = string.Empty;

            Console.Write("Type the name of the game you want to search for: ");
            busca = Console.ReadLine();

            Console.WriteLine("Searching");

            Console.WriteLine("-------------------------------------------------------------------");

            EcommerceSearchEngine[] searchEngine = { new LeaderSearchEngine(), new UzgamesSearchEngine() };

            foreach (EcommerceSearchEngine search in searchEngine)
            {
                System.IO.File.AppendAllText("SearchResult.txt", "Buscado em: " + search.EcommerceName + "\r\n"); ;
                Console.WriteLine("Buscado em: " + search.EcommerceName);
                Console.WriteLine();

                EcommerceSearchRescult result = search.Search(busca);

                foreach (var item in result.EcommerceSearchRetultItems)
                {
                    System.IO.File.AppendAllText("SearchResult.txt", string.Format("{0} - {1} - {2} \r\n", item.ItemName, item.ItemImageUrl, item.ItemImageUrl)); ;
                }
               
                Console.WriteLine();
            }           

            Console.WriteLine("-------------------------------------------------------------------");

            System.Diagnostics.Process.Start("SearchResult.txt");

            Console.ReadKey();
        }
    }
}
