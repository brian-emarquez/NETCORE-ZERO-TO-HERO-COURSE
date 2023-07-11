namespace APICore.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public String CodigoBarra { get; set; }

        public String Nombre { get; set; }

        public String Marca { get; set; }

        public String Categoria { get; set; }

        public decimal Precio { get; set; }

    }
}
