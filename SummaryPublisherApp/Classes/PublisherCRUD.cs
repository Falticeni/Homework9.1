using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_9._1.Classes;
using System.Data;
using System.Data.SqlClient;
using IODS.Handlers;

namespace SummaryPublisherApp.Classes
{
    internal class PublisherCrud : Conexiune
    {
        public static void NumberOfRows()
        {
            string selectString = "SELECT COUNT(*) FROM Publisher";
            SqlCommand fetchNumberOfRows = new SqlCommand(selectString, sqlConnection);

            try
            {
                sqlConnection.Open();
                var numberOfRows = fetchNumberOfRows.ExecuteScalar();
                OutputHandling.Message($"Number of rows = {numberOfRows}");
            }

            catch
            {
                OutputHandling.Error("Cannot connect to database");
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        public static void TopTenPublishers()
        {
            string selectString = "SELECT TOP 10 * FROM Publisher";
            SqlCommand getTop10PublishersCommand = new SqlCommand(selectString, sqlConnection);
            SqlDataAdapter publisherDataAdapter = new SqlDataAdapter(getTop10PublishersCommand);        // folosit pentru a prelua datele din mai multe coloane ale unui tabel sql . SqlDataAdapter deschide si inchide automat conexiunea
            DataTable publisherTable = new DataTable("Top 10 Publishers");  // o structura de date ce simuleaza in .NET un tabel sql
            publisherDataAdapter.Fill(publisherTable); // cu adaptorul de date  umplem structura de mai sus cu valorile obtinute din tabelul real sql

            EnumerableRowCollection<DataRow> tabledata = publisherTable.AsEnumerable();     // permite sa parcurgi tabelul cu foreach, ca si cum ar fi arr

            foreach (DataRow row in tabledata)
            {
                OutputHandling.Message($"Publisher Id: {row[0]}");
                OutputHandling.Message($"Publisher Name: {row[1]}");
                Console.WriteLine();
            }
        }

        public static void NumberOfBooksPerPublisher()
        {
            string selectString = "SELECT Publisher.Name, Count(*) " +
                                    "FROM Publisher, Book " +
                                    "WHERE Publisher.PublisherId = Book.PublisherId " +
                                    "GROUP BY Publisher.Name; ";

            SqlCommand numberOfBooksPerPublisherCommand = new SqlCommand(selectString, sqlConnection);
            SqlDataAdapter numberOfBooksPerPublisherAdapter = new SqlDataAdapter(numberOfBooksPerPublisherCommand);
            numberOfBooksPerPublisherAdapter.SelectCommand.Connection = sqlConnection;

            DataTable numberOfBooksPerPublisherTable = new DataTable("Books Per Publisher");

            numberOfBooksPerPublisherAdapter.Fill(numberOfBooksPerPublisherTable);

            EnumerableRowCollection<DataRow> tableData = numberOfBooksPerPublisherTable.AsEnumerable();

            foreach (DataRow row in tableData)
            {
                OutputHandling.Message($"Publisher Name: {row[0]}");
                OutputHandling.Message($"Number of Books: {row[1]}");
                Console.WriteLine();
            }
        }

        public static void TotalBookCostOfPublisher()
        {
            string selectString = "SELECT SUM(Book.Price) FROM Book " +
                                    "WHERE PublisherId = @publisherId";

            int pId = InputHandling.ReadValue("Publisher Id: ");
            SqlParameter publisherId = new SqlParameter { Value = pId, ParameterName = "publisherId", SqlDbType = SqlDbType.Int };

            sqlConnection.Open();
            SqlCommand totalBookCostOfPublisherCommand = new SqlCommand(selectString, sqlConnection);
            totalBookCostOfPublisherCommand.Parameters.Add(publisherId);
            var numberOfBooks = totalBookCostOfPublisherCommand.ExecuteScalar();
            sqlConnection.Close();
            Console.WriteLine($"Total cost of books for publisher with id {pId} is {numberOfBooks}");
        }
    }
}
