using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Homework_9._1.Classes
{
    internal class PublisherCrud : Conexiune
    {
        public static void InsertPublisher(int publisherId, string name)
        {
            // stringul reprezinta query-ul din sql
            string insertString = "SET IDENTITY_INSERT dbo.Publisher ON;" +  // pentru ca trebuie sa inseram manual un id autoincrementat (identity)
                                  "INSERT INTO Publisher (PublisherId, Name) " +
                                  "VALUES (@publisherId, @publisherName);" +    // parametrii de SQL
                                  "SELECT CAST(scope_identity() AS INT)"; // returnneaza Id-ul care se autoincrementeaza

            SqlCommand insertCommand = new SqlCommand(insertString, sqlConnection);

            SqlParameter p_PublisherId = new SqlParameter { ParameterName = "publisherId", SqlDbType = SqlDbType.Int, Value = publisherId }; // valoarea parametrului Sql cu numele publisherId preia valoarea parametrului metodei cu acelasi nume
            SqlParameter p_PublisherName = new SqlParameter { ParameterName = "publisherName", SqlDbType = SqlDbType.VarChar, Value = name }; // numele parametrut\lui trebuie sa fie identic cu nnumele din string @....

            insertCommand.Parameters.Add(p_PublisherId);// am adaugat parametrii la comanda
            insertCommand.Parameters.Add(p_PublisherName);

            try
            {
                sqlConnection.Open();  // deschidem conexiunea
                var pubId = insertCommand.ExecuteScalar();
                Console.WriteLine("A fost adugat publisherul cu Id-ul {0}", pubId);
            }
            catch
            {
                Console.WriteLine("Eroare! Hopa! ");
            }
            finally
            {
                sqlConnection.Dispose();
            }
        }
    }
}
