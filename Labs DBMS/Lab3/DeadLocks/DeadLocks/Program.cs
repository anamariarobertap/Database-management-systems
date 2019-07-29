using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeadLocks
{
    namespace DeadLocks
    {
        class Program
        {
            static readonly Object locker = new Object();
            static void Main(string[] args)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["dataBaseString"].ConnectionString;
                SqlConnection connection = null;

                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Thread t1 = new Thread(() => { runStoredPRoccedure(connection, "deadLock2"); });
                    t1.Start();
                    runStoredPRoccedure(connection, "deadLock1");
                }



            }

            public static void runStoredPRoccedure(SqlConnection connection, String procedureName)
            {
                int count = 1;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = CommandType.StoredProcedure })
                {
                    while (count < 4)
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.ConnectionString = ConfigurationManager.ConnectionStrings["dataBaseString"].ConnectionString;
                            connection.Open();
                        }
                        try
                        {

                            Console.WriteLine("procedure " + procedureName + " executed on try: " + count);
                            command.ExecuteNonQuery();
                            break;

                        }
                        catch ( SqlException ex)
                        {
                            lock (locker)
                            {
                                Console.WriteLine(ex.Message);
                                count++;
                                Console.WriteLine("Procedure " + procedureName + " retry number: " + count.ToString());
                                System.Threading.Thread.Sleep(5000);
                            }
                        }
                    }
                }
            }
        }
    }
}
