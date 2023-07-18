using System;
using System.Linq;
using apiexamen.Data;
using apiexamen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace apiexamen.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        private readonly MiContextoDB dbContext;
        public ExamenController(MiContextoDB _context)
        {
            dbContext = _context;
        }







        [HttpPost("AgregarExamen")]
        public IActionResult AgregarExamen([FromBody] tblExamen examen)
        {
            try
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                     // Agrega el examen a la base de datos
                         dbContext.Examen.Add(examen);
                        dbContext.SaveChanges();
                        transaction.Commit();
                        return Ok(new { Resultado = true });
                     }
                     catch (Exception ex)
                     {
                        transaction.Rollback();
                        return Ok(new { Resultado = false, Descripcion = "Error al agregar el examen: " + ex.Message });
                      }
                    }

                //Agregar un return aquí con un resultado predeterminado
                return Ok(new { Resultado = false, Descripcion = "Error al agregar el examen: No se pudo completar la operación" });
            }
            catch (Exception ex)
            {
                return Ok(new { Resultado = false, Descripcion = "Error al agregar el examen: " + ex.Message });
            }
        }

    [HttpPut("ActualizarExamen")]
        public IActionResult ActualizarExamen([FromBody] tblExamen examen)
        {
            try
            {
                tblExamen oExamen = dbContext.Examen.Find(examen.IdExamen);
                if (oExamen == null)
                    return Ok(new { Resultado = false, Descripcion = "El examen con Id " + examen.IdExamen + " no existe" });

                try
                {
                    oExamen.Nombre = examen.Nombre ?? oExamen.Nombre;
                    oExamen.Descripcion = examen.Descripcion ?? oExamen.Descripcion;

                    dbContext.Examen.Update(oExamen);
                    dbContext.SaveChanges();

                    return Ok(new { Resultado = true });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { Resultado = false, Descripcion = "Error al actualizar el examen: " + ex.Message });
            }
        }


    [HttpDelete("EliminarExamen")]
        public IActionResult EliminarExamen( int id)
        {
            try
            {
                using (var scope = dbContext.Database.BeginTransaction())
                {
                    var examen = dbContext.Examen.FirstOrDefault(e => e.IdExamen == id);
                    if (examen == null)
                        return Ok(new { Resultado = false, Descripcion = "El examen con Id " + id + " no existe" });
                    else
                    {
                        dbContext.Examen.Remove(examen);
                        dbContext.SaveChanges();

                        scope.Commit();
                        return Ok(new { Resultado = true });
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Resultado = false, Descripcion = "Error al eliminar el examen: " + ex.Message });
            }
        }

        [HttpGet("ConsultarExamen")]
        public IActionResult ConsultarExamen( string nombre, string descripcion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
                {
                    return Ok(new { Resultado = false, Descripcion = "Se deben proporcionar valores para los campos 'nombre' o 'descripcion'." });
                }

                var consulta = dbContext.Examen.AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    consulta = consulta.Where(e => e.Nombre.Contains(nombre));
                }

                if (!string.IsNullOrWhiteSpace(descripcion))
                {
                    consulta = consulta.Where(e => e.Descripcion.Contains(descripcion));
                }

                var examenes = consulta.ToList();

                if (examenes.Count == 0)
                {
                    return Ok(new { Resultado = true, Descripcion = "No se encontraron exámenes con los criterios de búsqueda proporcionados." });
                }
                else
                {
                    return Ok(examenes);
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Resultado = false, Descripcion = "Error al consultar el examen: " + ex.Message });
            }
        }
        [HttpGet]
        [Route("MostrarTodos")]
        public IActionResult MostrarTodos()
        {
            List<tblExamen> lista = new List<tblExamen>();

            try
            {
                lista = dbContext.Examen.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });

            }
        }
        private IActionResult Ok(bool resultado, string descripcion = "")
        {
            return Ok(new { Resultado = resultado, Descripcion = descripcion });
        }
    }
}
