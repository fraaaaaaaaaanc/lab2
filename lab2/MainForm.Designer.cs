namespace TransportTask
{
    partial class FormTransportTask
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.data_in = new System.Windows.Forms.DataGridView();
            this.data_out = new System.Windows.Forms.DataGridView();
            this.btn_plus_row = new System.Windows.Forms.Button();
            this.btn_minus_row = new System.Windows.Forms.Button();
            this.btn_plus_col = new System.Windows.Forms.Button();
            this.btn_minus_col = new System.Windows.Forms.Button();
            this.btn_solve = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_lim = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_auto = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.data_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_out)).BeginInit();
            this.SuspendLayout();
            // 
            // data_in
            // 
            this.data_in.AllowUserToAddRows = false;
            this.data_in.AllowUserToDeleteRows = false;
            this.data_in.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_in.Location = new System.Drawing.Point(12, 12);
            this.data_in.Name = "data_in";
            this.data_in.RowHeadersWidth = 51;
            this.data_in.RowTemplate.Height = 24;
            this.data_in.Size = new System.Drawing.Size(669, 376);
            this.data_in.TabIndex = 0;
            // 
            // data_out
            // 
            this.data_out.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.data_out.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_out.Location = new System.Drawing.Point(755, 12);
            this.data_out.Name = "data_out";
            this.data_out.RowHeadersWidth = 51;
            this.data_out.RowTemplate.Height = 24;
            this.data_out.Size = new System.Drawing.Size(597, 376);
            this.data_out.TabIndex = 1;
            // 
            // btn_plus_row
            // 
            this.btn_plus_row.Location = new System.Drawing.Point(16, 398);
            this.btn_plus_row.Name = "btn_plus_row";
            this.btn_plus_row.Size = new System.Drawing.Size(529, 28);
            this.btn_plus_row.TabIndex = 2;
            this.btn_plus_row.Text = "+";
            this.btn_plus_row.UseVisualStyleBackColor = true;
            this.btn_plus_row.Click += new System.EventHandler(this.btn_plus_row_Click);
            // 
            // btn_minus_row
            // 
            this.btn_minus_row.Location = new System.Drawing.Point(17, 435);
            this.btn_minus_row.Name = "btn_minus_row";
            this.btn_minus_row.Size = new System.Drawing.Size(529, 28);
            this.btn_minus_row.TabIndex = 3;
            this.btn_minus_row.Text = "-";
            this.btn_minus_row.UseVisualStyleBackColor = true;
            this.btn_minus_row.Click += new System.EventHandler(this.btn_minus_row_Click);
            // 
            // btn_plus_col
            // 
            this.btn_plus_col.Location = new System.Drawing.Point(687, 16);
            this.btn_plus_col.Name = "btn_plus_col";
            this.btn_plus_col.Size = new System.Drawing.Size(28, 376);
            this.btn_plus_col.TabIndex = 4;
            this.btn_plus_col.Text = "+";
            this.btn_plus_col.UseVisualStyleBackColor = true;
            this.btn_plus_col.Click += new System.EventHandler(this.btn_plus_col_Click);
            // 
            // btn_minus_col
            // 
            this.btn_minus_col.Location = new System.Drawing.Point(721, 16);
            this.btn_minus_col.Name = "btn_minus_col";
            this.btn_minus_col.Size = new System.Drawing.Size(28, 376);
            this.btn_minus_col.TabIndex = 5;
            this.btn_minus_col.Text = "-";
            this.btn_minus_col.UseVisualStyleBackColor = true;
            this.btn_minus_col.Click += new System.EventHandler(this.btn_minus_col_Click);
            // 
            // btn_solve
            // 
            this.btn_solve.Location = new System.Drawing.Point(823, 398);
            this.btn_solve.Name = "btn_solve";
            this.btn_solve.Size = new System.Drawing.Size(335, 65);
            this.btn_solve.TabIndex = 6;
            this.btn_solve.Text = "Решить транспортную задачу";
            this.btn_solve.UseVisualStyleBackColor = true;
            this.btn_solve.Click += new System.EventHandler(this.btn_solve_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1216, 410);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Результат";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1164, 435);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(188, 22);
            this.textBox1.TabIndex = 8;
            // 
            // btn_lim
            // 
            this.btn_lim.Location = new System.Drawing.Point(552, 398);
            this.btn_lim.Name = "btn_lim";
            this.btn_lim.Size = new System.Drawing.Size(129, 65);
            this.btn_lim.TabIndex = 10;
            this.btn_lim.Text = "Добавить ограничения";
            this.btn_lim.UseVisualStyleBackColor = true;
            this.btn_lim.Click += new System.EventHandler(this.btn_lim_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(687, 398);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(128, 28);
            this.btn_remove.TabIndex = 11;
            this.btn_remove.Text = "Очистить";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_auto
            // 
            this.btn_auto.Location = new System.Drawing.Point(687, 437);
            this.btn_auto.Name = "btn_auto";
            this.btn_auto.Size = new System.Drawing.Size(128, 24);
            this.btn_auto.TabIndex = 12;
            this.btn_auto.Text = "Заполнить";
            this.btn_auto.UseVisualStyleBackColor = true;
            this.btn_auto.Click += new System.EventHandler(this.btn_auto_Click);
            // 
            // FormTransportTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 467);
            this.Controls.Add(this.btn_auto);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_lim);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_solve);
            this.Controls.Add(this.btn_minus_col);
            this.Controls.Add(this.btn_plus_col);
            this.Controls.Add(this.btn_minus_row);
            this.Controls.Add(this.btn_plus_row);
            this.Controls.Add(this.data_out);
            this.Controls.Add(this.data_in);
            this.MaximizeBox = false;
            this.Name = "FormTransportTask";
            this.Text = "Решение транспортной задачи методом потенциалов";
            ((System.ComponentModel.ISupportInitialize)(this.data_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_out)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView data_in;
        private System.Windows.Forms.DataGridView data_out;
        private System.Windows.Forms.Button btn_plus_row;
        private System.Windows.Forms.Button btn_minus_row;
        private System.Windows.Forms.Button btn_plus_col;
        private System.Windows.Forms.Button btn_minus_col;
        private System.Windows.Forms.Button btn_solve;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_lim;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_auto;
    }
}

