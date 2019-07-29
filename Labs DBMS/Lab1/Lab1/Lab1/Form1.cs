using Lab1.repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private Repository Repo = new Repository();
        public Form1()
        {
            InitializeComponent();
            DataTable parentTable = Repo.FindAll("Authors");
            parentDataGridView.DataSource = parentTable;

            
        }

        private void ParentDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (parentDataGridView == null)
                return;
            if (parentDataGridView.CurrentCell.Selected)
            {
                DataTable childTable = Repo.FindAllById("Books", (int)parentDataGridView.CurrentRow.Cells[0].Value);
                childDataGridView.DataSource = childTable;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (childDataGridView.CurrentCell.Selected)
            {
                DataTable childTable = Repo.Remove("Books", (int)childDataGridView.CurrentRow.Cells[0].Value);
                childTable = Repo.FindAllById("Books", (int)parentDataGridView.CurrentRow.Cells[0].Value);
                childDataGridView.DataSource = childTable;
            }
        }

        private void changeAddButton_Click(object sender, EventArgs e)
        {
            if (parentDataGridView.CurrentCell.Selected)
            {
                    if (string.IsNullOrWhiteSpace(authorIdTextBox.Text))
                    {
                        DataTable childTable = Repo.Add("Books", titleTextBox.Text, (int)parentDataGridView.CurrentRow.Cells[0].Value);
                        childDataGridView.DataSource = childTable;

                    }
                    else
                    {
                        DataTable childTable1 = Repo.Add("Books", titleTextBox.Text, Int32.Parse(authorIdTextBox.Text));
                        childDataGridView.DataSource = childTable1;
                    }
                    
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (parentDataGridView.CurrentCell.Selected)
            {
                if (childDataGridView.CurrentCell.Selected)
                {
                    DataTable childTable = Repo.Update("Books", (int)childDataGridView.CurrentRow.Cells[0].Value, titleTextBox.Text, Int32.Parse(authorIdTextBox.Text));
                    childTable = Repo.FindAllById("Books", (int)parentDataGridView.CurrentRow.Cells[0].Value);
                    childDataGridView.DataSource = childTable;
                }
            }
        }

        private void ChildDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            authorIdTextBox.Text = childDataGridView.CurrentRow.Cells[2].Value.ToString();   
        }

   
    }
}
