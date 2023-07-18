using System;
using System.Windows.Forms;
using apiexamen;
using apiexamen.Data;
using apiexamen.Models;
using Microsoft.IdentityModel.Tokens;

namespace BansiExamen
{
    public partial class Examen : Form
    {
        private readonly clsExamen examenHelper;

        public Examen()
        {
            InitializeComponent();
            // Configura el HttpClient para usar el Web Service si es necesario
            var useWebServices = true; // O false, dependiendo de si quieres usar los servicios web o no
            var httpClient = new HttpClient();
            var dbContext = new MiContextoDB();
            examenHelper = new clsExamen(dbContext, useWebServices, httpClient);
            LoadAsync();
            dataGridViewExamenes.CellClick += dataGridViewExamenes_CellClick;
            validacampos();
            // Asociar el evento DataBindingComplete al DataGridView
            // Realizar la consulta

        }

        private void validacampos()
        {
            if (!txtDesc.Text.IsNullOrEmpty() && !txtIdExamen.Text.IsNullOrEmpty() && !txtNombre.Text.IsNullOrEmpty())
            {
                btnEditar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
            }
        }
        private async Task<List<tblExamen>> ConsultarExamenesAsync(string nombre, string descripcion)
        {
            // Realizar la consulta usando el helper de clsExamen
            return await examenHelper.Consultar(nombre, descripcion);
        }
        private async Task<List<tblExamen>> MostrarTodosExamenesAsync()
        {
            // Realizar la consulta usando el helper de clsExamen
            return await examenHelper.MostrarTodos();
        }

        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                string descripcion = txtDesc.Text;

                // Validar campos de entrada
                if (string.IsNullOrWhiteSpace(nombre) && string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("Ingresa al menos un valor para consultar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Realizar la consulta
                var examenes = await ConsultarExamenesAsync(nombre, descripcion);
                dataGridViewExamenes.ReadOnly = false;
                dataGridViewExamenes.DataSource = examenes;
                // Establecer el DataGridView en modo de solo lectura
                dataGridViewExamenes.ReadOnly = true;

                dataGridViewExamenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                if (examenes != null && examenes.Count > 0)
                {
                    // Actualizar el DataGridView con los resultados
                    //ActualizarDataGridView(examenes);
                }
                else
                {
                    dataGridViewExamenes.Rows.Clear();
                    dataGridViewExamenes.DataSource = null;
                    MessageBox.Show("No se encontraron exámenes o hubo un error en la consulta.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al consultar los exámenes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> Agregar(string nombre, string desc)
        {
            // Realizar la consulta usando el helper de clsExamen

            try
            {
                tblExamen examen = new tblExamen();
                examen.Nombre = nombre;
                examen.Descripcion = desc;
                 await examenHelper.AgregarExamen(examen);
                MessageBox.Show("Se agrego satisfactoriamente el Examen : " + nombre, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al consultar los exámenes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        private async Task<bool> Editar(int IdExamen, string nombre, string desc)
        {
            // Realizar la consulta usando el helper de clsExamen

            try
            {
                tblExamen examen = new tblExamen();
                examen.IdExamen = IdExamen;
                examen.Nombre = nombre;
                examen.Descripcion = desc;
                await examenHelper.ActualizarExamen(examen);
                MessageBox.Show("Se modifico satisfactoriamente el Examen con el id: " + IdExamen, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al consultar los exámenes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        private async Task<bool> Eliminar(int IdExamen)
        {
            // Realizar la consulta usando el helper de clsExamen

            try
            {
                tblExamen examen = new tblExamen();
                examen.IdExamen = IdExamen;
                await examenHelper.EliminarExamen(IdExamen);
                MessageBox.Show("Se elimino satisfactoriamente el Examen con el id: " + IdExamen, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al consultar los exámenes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string desc = txtDesc.Text;
            bool variable = await Agregar(nombre, desc);
            var examenes = await ConsultarExamenesAsync(nombre, desc);
            dataGridViewExamenes.ReadOnly = false;
            dataGridViewExamenes.DataSource = examenes;
            // Establecer el DataGridView en modo de solo lectura
            dataGridViewExamenes.ReadOnly = true;
            dataGridViewExamenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }


        private async void btnEditar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string desc = txtDesc.Text;
            int IdExamen = int.Parse(txtIdExamen.Text);
            bool variable = await Editar(IdExamen, nombre, desc);
            var examenes = await ConsultarExamenesAsync(nombre, desc);
            dataGridViewExamenes.ReadOnly = false;
            dataGridViewExamenes.DataSource = examenes;
            // Establecer el DataGridView en modo de solo lectura
            dataGridViewExamenes.ReadOnly = true;
            dataGridViewExamenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }


        private void dataGridViewExamenes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Establecer las columnas del DataGridView como de solo lectura
            foreach (DataGridViewColumn column in dataGridViewExamenes.Columns)
            {
                //column.ReadOnly = true;
            }
        }

        private void dataGridViewExamenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que el clic sea dentro de una fila válida
            {
                // Haz lo que necesites con el valor de la celda seleccionada

                txtIdExamen.Text = dataGridViewExamenes.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNombre.Text = dataGridViewExamenes.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDesc.Text = dataGridViewExamenes.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            validacampos();
        }


        private void txtIdExamen_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdExamen.Text))
            {
                // El contenido del TextBox está vacío o solo contiene espacios en blanco.
                // Ocultar el botón de guardar.
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
            }
            else
            {
                // El contenido del TextBox no está vacío.
                // Mostrar el botón de guardar.
                btnEditar.Visible = true;
                btnEliminar.Visible = true;
            }
        }

        private async Task LoadAsync()
        {
            dataGridViewExamenes.ReadOnly = false;
            var examenes = await MostrarTodosExamenesAsync();

            dataGridViewExamenes.DataSource = examenes;
            // Establecer el DataGridView en modo de solo lectura
            dataGridViewExamenes.ReadOnly = true;
            validacampos();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            int IdExamen = int.Parse(txtIdExamen.Text);
            bool variable = await Eliminar(IdExamen);

        }
    }
}
