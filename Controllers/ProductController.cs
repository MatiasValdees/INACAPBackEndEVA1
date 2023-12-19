using EVA1_BackEnd.Model;
using EVA1_BackEnd.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVA1_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> ReadAll()
        {
            return Ok(await _context.TblProductos.ToListAsync());

        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(ProductoDTO p)
        {
            var existe = await _context.TblProductos.Where(x => x.Nombre == p.Nombre).FirstOrDefaultAsync();

            if (existe == null)
            {   
                Producto producto = new Producto();

                producto.Nombre = p.Nombre;
                producto.Descripcion=p.Descripcion;
                producto.Precio = p.Precio;
                producto.Stock = p.Stock;
                producto.Imagen=p.Imagen;
                producto.CategoriaId = p.CategoriaId;

                _context.TblProductos.Add(producto);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Producto ya Ingresada");

        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(int id, ProductoDTO p)
        {
            var existe = await _context.TblProductos.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (existe == null)
            {
                return NotFound();
            }

            existe.Nombre = p.Nombre;
            existe.Descripcion=p.Descripcion;
            existe.Precio = p.Precio;
            existe.Stock = p.Stock;
            existe.Imagen = p.Imagen;
            existe.CategoriaId=p.CategoriaId;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await _context.TblProductos.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (existe == null)
            {
                return NotFound("No se encuentra Producto a eliminar");
            }
            _context.Remove(existe);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("getForId")]
        public async Task<IActionResult> GetForId(int id)
        {
            var existe = await _context.TblProductos.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (existe == null)
            {
                return NotFound("No se encuentra Producto");
            }
            return Ok(existe);
        }

        [HttpGet]
        [Route("getForName")]
        public async Task<IActionResult> GetForName(string nombre)
        {
            var existe = await _context.TblProductos.Where(x => x.Nombre == nombre).FirstOrDefaultAsync();
            if (existe == null)
            {
                return NotFound("No se encuentra Producto");
            }
            return Ok(existe);
        }

    }
}
