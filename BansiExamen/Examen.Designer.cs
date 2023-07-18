namespace BansiExamen
{
    partial class Examen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewExamenes = new DataGridView();
            txtNombre = new TextBox();
            txtDesc = new TextBox();
            btnConsultar = new Button();
            btnAgregar = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtIdExamen = new TextBox();
            panel1 = new Panel();
            btnEliminar = new Button();
            btnEditar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewExamenes).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewExamenes
            // 
            dataGridViewExamenes.AllowUserToOrderColumns = true;
            dataGridViewExamenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewExamenes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewExamenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewExamenes.Location = new Point(27, 268);
            dataGridViewExamenes.Name = "dataGridViewExamenes";
            dataGridViewExamenes.RowTemplate.Height = 25;
            dataGridViewExamenes.Size = new Size(436, 150);
            dataGridViewExamenes.TabIndex = 0;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(90, 35);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(100, 23);
            txtNombre.TabIndex = 1;
            // 
            // txtDesc
            // 
            txtDesc.Location = new Point(90, 64);
            txtDesc.Name = "txtDesc";
            txtDesc.Size = new Size(100, 23);
            txtDesc.TabIndex = 2;
            // 
            // btnConsultar
            // 
            btnConsultar.Location = new Point(39, 78);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(105, 30);
            btnConsultar.TabIndex = 3;
            btnConsultar.Text = "Consultar";
            btnConsultar.UseVisualStyleBackColor = true;
            btnConsultar.Click += btnConsultar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(39, 40);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(105, 30);
            btnAgregar.TabIndex = 4;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 35);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 5;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 72);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 6;
            label2.Text = "Descripcion";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 5);
            label3.Name = "label3";
            label3.Size = new Size(23, 15);
            label3.TabIndex = 7;
            label3.Text = "Id :";
            // 
            // txtIdExamen
            // 
            txtIdExamen.Location = new Point(90, 3);
            txtIdExamen.Name = "txtIdExamen";
            txtIdExamen.Size = new Size(100, 23);
            txtIdExamen.TabIndex = 8;
            txtIdExamen.TextChanged += txtIdExamen_TextChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEliminar);
            panel1.Controls.Add(btnEditar);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtIdExamen);
            panel1.Controls.Add(txtNombre);
            panel1.Controls.Add(txtDesc);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(163, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(255, 180);
            panel1.TabIndex = 9;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(136, 126);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 10;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(23, 126);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 23);
            btnEditar.TabIndex = 9;
            btnEditar.Text = "Actualizar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // Examen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(496, 450);
            Controls.Add(panel1);
            Controls.Add(btnAgregar);
            Controls.Add(btnConsultar);
            Controls.Add(dataGridViewExamenes);
            Name = "Examen";
            Text = "Examen";
            ((System.ComponentModel.ISupportInitialize)dataGridViewExamenes).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }



        #endregion

        private DataGridView dataGridViewExamenes;
        private TextBox txtNombre;
        private TextBox txtDesc;
        private Button btnConsultar;
        private Button btnAgregar;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtIdExamen;
        private Panel panel1;
        private Button btnEliminar;
        private Button btnEditar;
    }
}