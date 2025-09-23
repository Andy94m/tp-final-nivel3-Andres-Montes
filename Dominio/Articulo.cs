using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Cod { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marcas Compania { get; set; }
        public Categorias Tipo { get; set; }
        public string UrlImagen { get; set; }
        public decimal Precio { get; set; }
    }
}
