using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVA1_BackEnd.Model
{

    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }

    public class Producto
    {
        [Key]
        public int ProductId { get; set;}
        [Required]
        public string Nombre { get; set;}
        [Required]
        public string Descripcion { get; set;}
        [Required]
        public int Precio { get; set;}
        [Required]
        public int Stock { get; set;}
        [Required]
        public string Imagen { get; set;}

        [ForeignKey("CategoriaId")]
        public int CategoriaId { get; set;}
        public Categoria categoria { get; set;}
    }
}
