using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using System.Data.SqlClient;
using Lab1.repository;

namespace Lab1.repository
{
    class Repository
    {
        private DBConnection DbConnection = new DBConnection();
        private SqlConnection Con { get; set; }

        public DataTable FindAll(string tableString)
        {
            DataTable table = new DataTable();
            Con = DbConnection.Connect();
            SqlCommand command = new SqlCommand("SELECT * FROM "+tableString, Con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(table);
            DbConnection.Disconnect();
            return table;
        }

        public DataTable FindAllById(string tableString, int id)
        {
            DataTable table = new DataTable();
            Con = DbConnection.Connect();
            SqlCommand command = new SqlCommand("SELECT * FROM " + tableString+" WHERE authorId = "+id.ToString() + ";", Con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(table);
            DbConnection.Disconnect();
            return table;
        }

        public DataTable Remove(string tableString, int id)
        {
            DataTable table = new DataTable();
            Con = DbConnection.Connect();
            SqlCommand command = new SqlCommand("DELETE FROM "+tableString+" WHERE bookId = "+id.ToString()+";", Con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(table);
            DbConnection.Disconnect();
            return table;
        }

        public DataTable Update(string tableString, int bookId, string title, int authorId)
        {
            DataTable table = new DataTable();
            Con = DbConnection.Connect();
            SqlCommand command = new SqlCommand("UPDATE "+tableString+" SET title = \'"+title+"\', authorId = "+authorId+" WHERE bookId = "+bookId+";", Con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(table);
            DbConnection.Disconnect();
            return table;
        }

        public DataTable Add(string tableString, string title, int authorId)
        {
            DataTable table = new DataTable();
            Con = DbConnection.Connect();
            SqlCommand command = new SqlCommand("INSERT INTO "+tableString+" (title,authorId) VALUES (\'"+title+"\', "+authorId.ToString()+");", Con);
            command.ExecuteNonQuery();
            table=FindAllById(tableString, authorId);
            DbConnection.Disconnect();
            return table;
        }
    }
}
