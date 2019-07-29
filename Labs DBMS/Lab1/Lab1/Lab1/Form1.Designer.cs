namespace Lab1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.parentDataGridView = new System.Windows.Forms.DataGridView();
            this.childDataGridView = new System.Windows.Forms.DataGridView();
            this.deleteButton = new System.Windows.Forms.Button();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.authorIdTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.authorIdLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.parentDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.childDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // parentDataGridView
            // 
            this.parentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.parentDataGridView.Location = new System.Drawing.Point(400, 12);
            this.parentDataGridView.Name = "parentDataGridView";
            this.parentDataGridView.Size = new System.Drawing.Size(388, 204);
            this.parentDataGridView.TabIndex = 1;
            this.parentDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ParentDataGridView_CellClick);
            // 
            // childDataGridView
            // 
            this.childDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.childDataGridView.Location = new System.Drawing.Point(400, 234);
            this.childDataGridView.Name = "childDataGridView";
            this.childDataGridView.Size = new System.Drawing.Size(388, 204);
            this.childDataGridView.TabIndex = 2;
            this.childDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ChildDataGridView_CellClick);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(168, 193);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(155, 110);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(100, 20);
            this.titleTextBox.TabIndex = 5;
            // 
            // authorIdTextBox
            // 
            this.authorIdTextBox.Location = new System.Drawing.Point(155, 152);
            this.authorIdTextBox.Name = "authorIdTextBox";
            this.authorIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.authorIdTextBox.TabIndex = 6;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(168, 234);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.changeAddButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(168, 277);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(98, 113);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(27, 13);
            this.titleLabel.TabIndex = 10;
            this.titleLabel.Text = "Title";
            // 
            // authorIdLabel
            // 
            this.authorIdLabel.AutoSize = true;
            this.authorIdLabel.Location = new System.Drawing.Point(98, 155);
            this.authorIdLabel.Name = "authorIdLabel";
            this.authorIdLabel.Size = new System.Drawing.Size(52, 13);
            this.authorIdLabel.TabIndex = 11;
            this.authorIdLabel.Text = "Author ID";
            this.authorIdLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.authorIdLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.authorIdTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.childDataGridView);
            this.Controls.Add(this.parentDataGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.parentDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.childDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView parentDataGridView;
        private System.Windows.Forms.DataGridView childDataGridView;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox authorIdTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label authorIdLabel;
    }
}

