using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using APICore.Models;

using System.Data;
using System.Data.SqlClient;

namespace APICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly String CadenaSQL;

        private ProductoController(IConfiguration config) {
            CadenaSQL = config.GetConnectionString("CadenaSQL");

        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista() { 
            List<Producto> lista = new List<Producto>();

            try
            {
                using (var conexion = new SqlConnection(CadenaSQL))
                {

                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_productos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read()){
                            lista.Add(new Producto()
                            {

                                IdProducto = Convert.ToInt32(rd["IdProducto"]),
                                CodigoBarra = rd["CodigoBarra"].ToString(),
                                Nombre = rd["Nombre"].ToString(),
                                Marca = rd["Marca"].ToString(),
                                Categoria = rd["Categoria"].ToString(),
                                Precio = Convert.ToDecimal(rd["Precio"])
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }

        }
    }
}
