using Microsoft.AspNetCore.Mvc;

namespace UND2_ACT1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {
        // Lista estática para guardar productos en memoria
        private static List<Producto> productos = new();

        // Verifica si el encabezado X-API-KEY es correcto
        private bool ApiKeyValida()
        {
            return Request.Headers.TryGetValue("X-API-KEY", out var key) && key == "12345";
        }

        /// <summary>
        /// Devuelve la lista de productos
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerProductos()
        {
            if (!ApiKeyValida())
                return Unauthorized("Falta X-API-KEY o no es válida.");

            return Ok(productos);
        }

        /// <summary>
        /// Agrega un nuevo producto a la lista
        /// </summary>
        [HttpPost]
        public IActionResult CrearProducto([FromBody] Producto producto)
        {
            if (!ApiKeyValida())
                return Unauthorized("Falta X-API-KEY o no es válida.");

            productos.Add(producto);
            return CreatedAtAction(nameof(ObtenerProductos), producto);
        }
    }

    // Clase del modelo Producto (en el mismo archivo)
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
    }
}