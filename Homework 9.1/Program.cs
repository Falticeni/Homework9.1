using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_9._1.Classes;
using IODS.Handlers;

namespace InsertPublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int pubId = InputHandling.ReadValue("Publisher Id: ");
            string pubName = InputHandling.ReadString("Publisher Name: ");
            PublisherCrud.InsertPublisher(pubId,pubName);
            Console.ReadKey();
        }
    }
}
