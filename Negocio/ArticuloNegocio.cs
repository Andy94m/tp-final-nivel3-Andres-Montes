using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> listar()
        {

            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion DescMarca, C.Descripcion DescCategoria, ImagenUrl, A.IdMarca, A.IdCategoria, Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and  A.IdCategoria= C.ID and Nombre is not null and Codigo NOT LIKE '#%'");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                    lista.Add(auxFila(datos.Lector));

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private Articulo auxFila(SqlDataReader lector)
        {
            Articulo aux = new Articulo
            {
                Id = (int)lector["Id"],
                Cod = (string)lector["Codigo"],
                Nombre = (string)lector["Nombre"],
                Descripcion = (string)lector["Descripcion"],
                Compania = new Marcas
                {
                    Id = (int)lector["IdMarca"],
                    Descripcion = (string)lector["DescMarca"]
                },
                Tipo = new Categorias
                {
                    Id = (int)lector["IdCategoria"],
                    Descripcion = (string)lector["DescCategoria"]
                },
                Precio = (decimal)lector["Precio"]
            };

            if (!(lector["ImagenUrl"] is DBNull))
                aux.UrlImagen = (string)lector["ImagenUrl"];

            return aux;
        }

        public List<Articulo> listarExcluidos()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion DescMarca, C.Descripcion DescCategoria, ImagenUrl, A.IdMarca, A.IdCategoria, Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and  A.IdCategoria= C.ID and Nombre is not null and Codigo LIKE '#%'");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                    lista.Add(auxFila(datos.Lector));

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Cod);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Compania.Id);
                datos.setearParametro("@IdCategoria", nuevo.Tipo.Id);
                datos.setearParametro("@ImagenUrl", nuevo.UrlImagen);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
                Console.WriteLine($"Consulta generada: insert into DISCOS (Titulo, FechaLanzamiento, CantidadCanciones) values ('{nuevo.Nombre}', '{nuevo.Cod}', {nuevo.Descripcion})");
            }
        }

        public void modificar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                datos.setearParametro("@Id", nuevo.Id);
                datos.setearParametro("@Codigo", nuevo.Cod);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Compania.Id);
                datos.setearParametro("@IdCategoria", nuevo.Tipo.Id);
                datos.setearParametro("@ImagenUrl", nuevo.UrlImagen);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void eliminarFisico (int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE from ARTICULOS where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = '#' + Codigo where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void restaurarEliminado(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = REPLACE(Codigo, '#', '') where Codigo LIKE '#%' AND Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        public List<Articulo> filtrar (string columna, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos ();
            Console.WriteLine("Dentro de metodo filtrar");
            try 
            {
                string consulta = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion DescMarca, C.Descripcion DescCategoria, ImagenUrl, A.IdMarca, A.IdCategoria, Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and  A.IdCategoria = C.Id and Nombre is not null and Codigo IS NOT NULL and Codigo NOT LIKE '#%' AND ";

                if(columna == "Nombre" || columna == "Codigo" || columna == "Marca" || columna == "Categoria" || columna == "Descripcion")
                {
                    if(columna == "Marca")
                    {
                        columna = "M.Descripcion";
                    }
                    else if(columna == "Categoria")
                    {
                        columna = "C.Descripcion";
                    }

                    if (criterio == "Comienza con")
                        consulta += columna + " like '" + filtro + "%'";
                    else if (criterio == "Termina con")
                        consulta += columna + " like '%" + filtro + "'";
                    else
                        consulta += columna + " like '%" + filtro + "%'";

                    Console.WriteLine (consulta);
                }
                else if (columna == "Precio")
                {
                    if (criterio == "Mayor a")
                        consulta += "Precio > " + filtro;
                    else if (criterio == "Menor a")
                        consulta += "Precio < " + filtro;
                    else
                        consulta += "Precio = " + filtro;
                }

                Console.WriteLine ("SQL " + consulta);
                datos.setearConsulta (consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                    lista.Add(auxFila(datos.Lector));

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
