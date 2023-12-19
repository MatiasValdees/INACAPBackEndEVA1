using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EVA1_BackEnd.Model.DTO
{
    public class ProductoDTO
    {
        public int ProductId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
        public int CategoriaId { get; set; }
    }
}
