using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.repository
{
    class DBConnection
    {
        private readonly string ConnectionString = "Data Source=localhost;Initial Catalog=Library; Integrated Security=SSPI";
        private SqlConnection Con { get; set; }


        public SqlConnection Connect()
        {
            Con = new SqlConnection(ConnectionString);
            try
            {
                Con.Open();
                Console.WriteLine("Connection Successful");
            }
            catch(SqlException e)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < e.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + e.Errors[i].Message + "\n" +
                        "LineNumber: " + e.Errors[i].LineNumber + "\n" +
                        "Source: " + e.Errors[i].Source + "\n" +
                        "Procedure: " + e.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            return Con;
            
        }

        public void Disconnect()
        {
            try
            {
                Con.Close();
                Console.WriteLine("Disconnect Succesful");
            }
            catch(Exception e)
            {
                Console.WriteLine("did not disconnect");

            }
            
        }
    }
}
