using apiexamen;
using apiexamen.Data;
using apiexamen.Models;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly clsExamen examenHelper;
       
        public Form1()
        {
            InitializeComponent();
            // Configura el HttpClient para usar el Web Service si es necesario
            var useWebServices = false; // O false, dependiendo de si quieres usar los servicios web o no
            var httpClient = new HttpClient();
            var dbContext = new MiContextoDB();
            examenHelper = new clsExamen(dbContext, useWebServices, httpClient);
        }

        private async  void Form1_Load(object sender, EventArgs e)
        {
            //var resultadosConsulta = await examenHelper.Consultar("string", "string");
            tblExamen examen = new tblExamen();
            examen.IdExamen = 2;
            examen.Nombre = "Ramos";
            examen.Descripcion = "Ramos";
            var resultadosConsulta = await examenHelper.Consultar("123", "123");
            // Aquí puedes hacer lo que desees con los resultados, por ejemplo, mostrarlos en un DataGridView o ListBox
            dataGridView1.DataSource = resultadosConsulta;
        }
    }
}