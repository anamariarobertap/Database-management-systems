using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace lab2 {
    public partial class App : Form {

        string sqlConnectionString = ConfigurationManager.ConnectionStrings["employeeConnectionString"].ConnectionString;
        SqlConnection Connection;

        SqlCommand SqlCommand;
        SqlDataAdapter daParent;
        SqlDataAdapter daChild;
        BindingSource bsParent = new BindingSource();
        BindingSource bsChild = new BindingSource();

        List<TextBox> TextBoxes = new List<TextBox>();
        List<Label> Labels = new List<Label>();
        List<string> Columns = new List<string>(); // column
        List<string> Values = new List<string>(); // @column
        string ParentTable = ConfigurationManager.AppSettings["ParentTable"];
        string ChildTable = ConfigurationManager.AppSettings["ChildTable"];
        string ForeignKeyParent = "";
        string ForeignKeyChild = "";

        int RowId = 0;

        public App() {
            InitializeComponent();

            SetTable();
            GetData();
            dataGridView2.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(dataGridView2_RowHeaderMouseClick);
            dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(dataGridView1_RowHeaderMouseClick);
        }

        private void SetTable() {
            try {
                Connection = new SqlConnection(sqlConnectionString);
                Connection.Open();
                string query = string.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " +
                                             "WHERE TABLE_NAME = \'{0}\'", ChildTable);

                using (SqlCommand command = new SqlCommand(query, Connection)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            Columns.Add(reader.GetString(0)); // add column names to column list
                        }
                    }
                }

                foreach (string column in Columns.Skip(1)) {
                    Values.Add(string.Concat("@", column)); // add @column to value list
                }

                // design textbox, label
                int yCoordinate = 100;

                for (int i = 0; i <= Values.Count; i++) { // for each column, add textbos and label
                    TextBox tbox = new TextBox();
                    Label label = new Label();

                    Labels.Add(label);
                    TextBoxes.Add(tbox);
                    TextBoxes[i].Location = new System.Drawing.Point(120, yCoordinate);
                    TextBoxes[i].Size = new System.Drawing.Size(150, 50);
                    TextBoxes[i].Name = string.Concat("tbox", i);
                    Labels[i].Location = new System.Drawing.Point(50, yCoordinate);
                    Labels[i].Text = string.Concat(Columns.ElementAt(i), ':');

                    Controls.Add(TextBoxes[i]);
                    Controls.Add(Labels[i]);
                    yCoordinate += 30;
                }

            } catch (SqlException ex) {
                MessageBox.Show("- connection error -" + ex);
            } catch (Exception ex) {
                MessageBox.Show("- error -" + ex);
            } finally {
                Connection.Close();
            }
        }

        private void GetData() {
            try {
                Connection = new SqlConnection(sqlConnectionString);
                Connection.Open();
                string query = string.Format("SELECT ccu.table_name AS SourceTable, ccu.constraint_name AS SourceConstraint, " +
                "ccu.column_name AS SourceColumn, kcu.table_name AS TargetTable, kcu.column_name AS TargetColumn " +
                "FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc " +
                "ON ccu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu " +
                "ON kcu.CONSTRAINT_NAME = rc.UNIQUE_CONSTRAINT_NAME " +
                "WHERE ccu.table_name = \'{0}\' AND kcu.table_name = \'{1}\'", ChildTable, ParentTable);

                // create dataset
                DataSet dataSet = new DataSet();
                daParent = new SqlDataAdapter(string.Format("select * from {0}", ParentTable), Connection);
                daChild = new SqlDataAdapter(string.Format("select * from {0}", ChildTable), Connection);
                daParent.Fill(dataSet, ParentTable);
                daChild.Fill(dataSet, ChildTable);

                // get foreign key parent and child (column names can be different)
                using (SqlCommand command = new SqlCommand(query, Connection)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            //if (reader.GetString(0).Equals(ChildTable) && reader.GetString(3).Equals(ParentTable)) {
                                ForeignKeyParent = reader.GetString(4);
                                ForeignKeyChild = reader.GetString(2);
                            //}
                        }
                    }
                }

                // create parent-child relation between columns
                DataRelation relation = new DataRelation(string.Concat(ParentTable, ChildTable),
                dataSet.Tables[ParentTable].Columns[ForeignKeyParent],
                dataSet.Tables[ChildTable].Columns[ForeignKeyChild]);
                dataSet.Relations.Add(relation);

                // data binding parent
                bsParent.DataSource = dataSet;
                bsParent.DataMember = ParentTable;
                // data binging child to parent
                bsChild.DataSource = bsParent;
                bsChild.DataMember = string.Concat(ParentTable, ChildTable);

                dataGridView1.DataSource = bsParent;
                dataGridView2.DataSource = bsChild;

            } catch (SqlException ex) {
                MessageBox.Show("- connection error -" + ex);
            } catch (Exception ex) {
                MessageBox.Show("- error -" + ex);
            } finally {
                Connection.Close();
            }
        }

        private void ClearData() {
            // clear text boxes
            foreach (TextBox textBox in TextBoxes) {
                textBox.Text = "";
            }
            RowId = 0;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            ClearData();
            int index = Columns.FindIndex(x => x.Equals(ForeignKeyParent)); // fk index from columns
            TextBoxes.ElementAt(index).Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // fill tb
            TextBoxes.ElementAt(index).ReadOnly = true;
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            int index = Columns.FindIndex(x => x.Equals(ForeignKeyParent));
            TextBoxes.ElementAt(index).ReadOnly = false;
            RowId = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString()); // rowid = child pk

            int i = 0;
            foreach (TextBox textBox in TextBoxes) {
                textBox.Text = dataGridView2.Rows[e.RowIndex].Cells[i].Value.ToString(); // "update" in tb list
                i++;
            }
        }

        private void createButton_Click(object sender, EventArgs e) {
            try {
                Connection = new SqlConnection(sqlConnectionString);
                Connection.Open();

                string columns = string.Join(", ", Columns.Skip(1).ToArray()); // y,z
                string values = string.Join(", ", Values.ToArray()); // @x,@y,@z
                SqlCommand = new SqlCommand(string.Format("insert into {0}({1}) values({2})", ChildTable, columns, values), Connection);
                int index = 1;

                foreach (string column in Values) {
                    TextBox textbox = (TextBox)Controls.Find(string.Format("tbox{0}", index), false).FirstOrDefault();
                    SqlCommand.Parameters.AddWithValue(column, textbox.Text); // @column -> text
                    index++;
                }

                SqlCommand.ExecuteNonQuery();
                GetData();
                ClearData();

            } catch (SqlException ex) {
                MessageBox.Show("- creation error -" + ex);
            } catch (Exception ex) {
                MessageBox.Show("- error -" + ex);
            } finally {
                Connection.Close();
            }
        }

        private void updateButton_Click(object sender, EventArgs e) {
            try {
                if (RowId != 0) {
                    Connection = new SqlConnection(sqlConnectionString);
                    Connection.Open();

                    List<string> columnValue = new List<string>();
                    int i = 0;
                    foreach (string column in Columns.Skip(1)) {
                        columnValue.Add(column + "=" + Values.ElementAt(i)); // column = @column
                        i++;
                    }

                    string columns = string.Join(", ", columnValue); // y=@y,z=@z
                    SqlCommand = new SqlCommand(string.Format("update {0} set {1} where {2}=@rowId",
                                                ChildTable, columns, Columns.ElementAt(0)), Connection);

                    SqlCommand.Parameters.AddWithValue("rowId", RowId); // where pk = rowid
                    int index = 1;
                    foreach (string column in Values) {
                        TextBox textbox = (TextBox)Controls.Find(string.Format("tbox{0}", index), false).FirstOrDefault();
                        SqlCommand.Parameters.AddWithValue(column, textbox.Text); // @column -> text
                        index++;
                    }

                    SqlCommand.ExecuteNonQuery();
                    GetData();
                    ClearData();
                } else {
                    MessageBox.Show("- select tupel -");
                }

            } catch (SqlException ex) {
                MessageBox.Show("- update error -" + ex);
            } catch (Exception ex) {
                MessageBox.Show("- error -" + ex);
            } finally {
                Connection.Close();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            try {
                if (RowId != 0) {
                    Connection = new SqlConnection(sqlConnectionString);
                    SqlCommand = new SqlCommand(string.Format("delete {0} where {1}=@rowId", ChildTable, Columns.ElementAt(0)), Connection);
                    Connection.Open();

                    SqlCommand.Parameters.AddWithValue("@rowId", RowId);
                    SqlCommand.ExecuteNonQuery();

                    GetData();
                    ClearData();
                } else {
                    MessageBox.Show("- select tupel -");
                }

            } catch (SqlException ex) {
                MessageBox.Show("- deletion error -" + ex);
            } catch (Exception ex) {
                MessageBox.Show("- error -" + ex);
            } finally {
                Connection.Close();
            }
        }
    }
}
