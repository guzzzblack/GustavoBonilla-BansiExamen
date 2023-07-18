using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using apiexamen.Data;
using apiexamen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace apiexamen
{
    public class clsExamen : IDisposable
    {
        public MiContextoDB dbContext;
        public MiDbContextDirecto dbContextDirecto;
        private readonly bool useWebServices;
        private readonly HttpClient httpClient;
        private const string BaseUrl = "https://localhost:7091/api/Examen/";

        public clsExamen(MiContextoDB dbContext, bool useWebServices, HttpClient httpClient)
        {
            this.dbContext = dbContext;
            this.useWebServices = useWebServices;
            this.httpClient = httpClient;

            // Inicializar dbContextDirecto con una nueva instancia si useWebServices es falso
            if (!useWebServices)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MiDbContextDirecto>();
                optionsBuilder.UseSqlServer("Server=localhost;Database=BdiExamen;Integrated Security=true;TrustServerCertificate=true");
                this.dbContextDirecto = new MiDbContextDirecto(optionsBuilder.Options);
            }
        }


        private async Task<HttpResponseMessage> PostToWebService(string action, object data)
        {
            var url = BaseUrl + action;
            return await httpClient.PostAsJsonAsync(url, data);
        }

        private async Task<HttpResponseMessage> PutToWebService(string action, object data)
        {
            var url = BaseUrl + action;
            return await httpClient.PutAsJsonAsync(url, data);
        }

        private async Task<HttpResponseMessage> DeleteFromWebService(string action)
        {
            var url = BaseUrl + action;
            return await httpClient.DeleteAsync(url);
        }
        private async Task<HttpResponseMessage> GetFromWebService(string action)
        {
            var url = BaseUrl + action;
            return await httpClient.GetAsync(url);
        }

        public async Task<bool> AgregarExamen(tblExamen examen)
        {
            try
            {
                if (useWebServices)
                {
                    using (var response = await PostToWebService("AgregarExamen", examen))
                    {
                        response.EnsureSuccessStatusCode();
                        return true;
                    }
                }
                else
                {
                    try
                    {
                        if (dbContextDirecto == null)
                        {
                            throw new Exception("El contexto directo no ha sido inicializado.");
                        }
                        var nombreParam = new SqlParameter("@Nombre", examen.Nombre);
                        var descripcionParam = new SqlParameter("@Descripcion", examen.Descripcion);
                        await dbContextDirecto.Database.ExecuteSqlRawAsync("EXEC spAgregar @Nombre, @Descripcion", nombreParam, descripcionParam);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar o lanzar una excepción personalizada si es necesario
                return false;
            }
        }


        public async Task<bool> ActualizarExamen(tblExamen examen)
        {
            try
            {
                if (useWebServices)
                {
                    using (var response = await PutToWebService($"ActualizarExamen", examen))
                    {
                        response.EnsureSuccessStatusCode();
                        return true;
                    }
                }
                else
                {
                    if (dbContextDirecto == null)
                    {
                        throw new Exception("El contexto directo no ha sido inicializado.");
                    }
                    var idExamenParam = new SqlParameter("@IdExamen", examen.IdExamen);
                    var nombreParam = new SqlParameter("@Nombre", examen.Nombre);
                    var descripcionParam = new SqlParameter("@Descripcion", examen.Descripcion);
                    await dbContextDirecto.Database.ExecuteSqlRawAsync("EXEC spActualizar @IdExamen, @Nombre, @Descripcion", idExamenParam, nombreParam, descripcionParam);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar o lanzar una excepción personalizada si es necesario
                return false;
            }
        }

        public async Task<bool> EliminarExamen(int idExamen)
        {
            try
            {
                if (useWebServices)
                {
                    using (var response = await DeleteFromWebService($"EliminarExamen/?id={idExamen}"))
                    {
                        response.EnsureSuccessStatusCode();
                        return true;
                    }
                }
                else
                {
                    if (dbContextDirecto == null)
                    {
                        throw new Exception("El contexto directo no ha sido inicializado.");
                    }
                    var idExamenParam = new SqlParameter("@IdExamen", idExamen);
                    await dbContextDirecto.Database.ExecuteSqlRawAsync("EXEC spEliminar @IdExamen", idExamenParam);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar o lanzar una excepción personalizada si es necesario
                return false;
            }
        }

        public async Task<List<tblExamen>> Consultar(string nombre, string descripcion )
        {
            try
            {
                if (useWebServices)
                {
                    var queryString = string.Empty;
                    if (!string.IsNullOrEmpty(nombre))
                    {
                        queryString += $"nombre={nombre}";
                    }

                    if (!string.IsNullOrEmpty(descripcion))
                    {
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString += "&";
                        }
                        queryString += $"descripcion={descripcion}";
                    }

                    var response = await GetFromWebService($"ConsultarExamen?{queryString}");
                    response.EnsureSuccessStatusCode();
                    var examenes = await response.Content.ReadFromJsonAsync<List<tblExamen>>();

                    if (examenes == null || examenes.Count == 0)
                    {
                        return new List<tblExamen>(); // Lista vacía en caso de que no se encuentren exámenes
                    }

                    return examenes;
                }
                else
                {
                    if (dbContextDirecto == null)
                        {
                            throw new Exception("El contexto directo no ha sido inicializado.");
                        }
                    var optionsBuilder = new DbContextOptionsBuilder<MiDbContextDirecto>();
                    optionsBuilder.UseSqlServer("Server=localhost;Database=BdiExamen;Integrated Security=true;TrustServerCertificate=true");

                    using (var dbContextDirecto = new MiDbContextDirecto(optionsBuilder.Options))
                    {
                        // Lógica para ejecutar el procedimiento almacenado y obtener los resultados
                        var nombreParam = new SqlParameter("@Nombre", nombre);
                        var descripcionParam = new SqlParameter("@Descripcion", descripcion);

                        // Ejecutar el procedimiento almacenado y obtener los resultados
                        var examenes = await dbContextDirecto.Examen
                            .FromSqlRaw("EXEC spConsultar @Nombre, @Descripcion", nombreParam, descripcionParam)
                            .ToListAsync();

                         return examenes;
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar o lanzar una excepción personalizada si es necesario
                return null;
            }
        }
        public async Task<List<tblExamen>> MostrarTodos()
        {
            var response = await GetFromWebService($"MostrarTodos");
            response.EnsureSuccessStatusCode();
            var examenes = await response.Content.ReadFromJsonAsync<List<tblExamen>>();
            if (examenes == null || examenes.Count == 0)
            {
                return new List<tblExamen>(); // Lista vacía en caso de que no se encuentren exámenes
            }

            return examenes;
        }
        public void Dispose()
        {
            // Liberar recursos, como el HttpClient, si es necesario
            httpClient.Dispose();
        }
    }
}
