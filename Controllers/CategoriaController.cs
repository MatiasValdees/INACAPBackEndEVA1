using EVA1_BackEnd.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVA1_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Trabajador")]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriaController(AppDbContext context)
        {
            _context = context;   
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult>ReadAll()
        {
            return Ok(await _context.TblCategorias.ToListAsync());

        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult>Create(Categoria c)
        {
            var existe=await _context.TblCategorias.Where(x=>x.Nombre==c.Nombre).FirstOrDefaultAsync();
            if (existe == null)
            {
                _context.TblCategorias.Add(c);
                await _context.SaveChangesAsync();
                return Ok();
            }
           return BadRequest("Categoria ya Ingresada");

        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult>Update(int id,Categoria c)
        {
            var existe= await _context.TblCategorias.Where(x=>x.CategoriaId==id).FirstOrDefaultAsync();
            if (existe == null)
            {
                return NotFound();
            }
            existe.Descripcion=c.Descripcion;
            existe.Nombre=c.Nombre;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult>Delete(int id)
        {
            var existe=await _context.TblCategorias.Where(x=>x.CategoriaId == id).FirstOrDefaultAsync();
            if(existe == null)
            {
                return NotFound("No se encuntra categoria a eliminar");
            }
            _context.Remove(existe);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("getForId")]
        public async Task<IActionResult> GetForId(int id) {
            var existe = await _context.TblCategorias.Where(x => x.CategoriaId == id).FirstOrDefaultAsync();
            if (existe == null)
            {
                return NotFound("No se encuentra categoria");
            }
            return Ok(existe);
        }

        [HttpGet]
        [Route("getForName")]
        public async Task<IActionResult> GetForName(string nombre)
        {
            var existe = await _context.TblCategorias.Where(x => x.Nombre == nombre).FirstOrDefaultAsync();
            if (existe == null)
            {
                return NotFound("No se encuentra categoria");
            }
            return Ok(existe);
        }



    }
}
