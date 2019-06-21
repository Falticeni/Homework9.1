using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SummaryPublisherApp.Classes;

namespace SummaryPublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PublisherCrud.NumberOfRows();
            PublisherCrud.TopTenPublishers();
            PublisherCrud.NumberOfBooksPerPublisher();
            PublisherCrud.TotalBookCostOfPublisher();
            Console.ReadKey();
        }
    }
}
