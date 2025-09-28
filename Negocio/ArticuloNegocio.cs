using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> listar(string id = "")
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            //AccesoDatos datos = new AccesoDatos();

            try
            {
                conexion.ConnectionString = ConfigurationManager.AppSettings["cadenaConexion"];
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion DescMarca, C.Descripcion DescCategoria, ImagenUrl, A.IdMarca, A.IdCategoria, Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and  A.IdCategoria= C.ID and Nombre is not null and Codigo NOT LIKE '#%'";
                //datos.setearConsulta("select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion DescMarca, C.Descripcion DescCategoria, ImagenUrl, A.IdMarca, A.IdCategoria, Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and  A.IdCategoria= C.ID and Nombre is not null and Codigo NOT LIKE '#%'");
                //datos.ejecutarLectura();
                //while (datos.Lector.Read())
                //    lista.Add(auxFila(datos.Lector));

                //return lista;

                if (id != "")
                    comando.CommandText += " and A.Id = " + id;

                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["Id"];
                    aux.Cod = (string)lector["Codigo"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.UrlImagen = (string)lector["ImagenUrl"];
                    aux.Precio = (decimal)lector["Precio"];

                    if (!(lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)lector["ImagenUrl"];

                    aux.Compania = new Marcas();
                    aux.Compania.Id = (int)lector["IdMarca"];
                    aux.Compania.Descripcion = (string)lector["DescMarca"];

                    aux.Tipo = new Categorias();
                    aux.Tipo.Id = (int)lector["IdCategoria"];
                    aux.Tipo.Descripcion = (string)lector["DescCategoria"];

                    lista.Add(aux);
                }
                conexion.Close();
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> listarConSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedListar");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Cod = (string)datos.Lector["Codigo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Compania = new Marcas();
                    aux.Compania.Id = (int)datos.Lector["IdMarca"];
                    aux.Compania.Descripcion = (string)datos.Lector["DescMarca"];

                    aux.Tipo = new Categorias();
                    aux.Tipo.Id = (int)datos.Lector["IdCategoria"];
                    aux.Tipo.Descripcion = (string)datos.Lector["DescCategoria"];

                    lista.Add(aux);
                }

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

        public void agregar(Articulo artic)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.setearParametro("@Codigo", artic.Cod);
                datos.setearParametro("@Nombre", artic.Nombre);
                datos.setearParametro("@Descripcion", artic.Descripcion);
                datos.setearParametro("@IdMarca", artic.Compania.Id);
                datos.setearParametro("@IdCategoria", artic.Tipo.Id);
                datos.setearParametro("@ImagenUrl", artic.UrlImagen);
                datos.setearParametro("@Precio", artic.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
                Console.WriteLine($"Consulta generada: insert into DISCOS (Titulo, FechaLanzamiento, CantidadCanciones) values ('{artic.Nombre}', '{artic.Cod}', {artic.Descripcion})");
            }
        }

        public void agregarConSP(Articulo artic)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedAltaArticulo");
                datos.setearParametro("@Codigo", artic.Cod);
                datos.setearParametro("@Nombre", artic.Nombre);
                datos.setearParametro("@Descripcion", artic.Descripcion);
                datos.setearParametro("@IdMarca", artic.Compania.Id);
                datos.setearParametro("@IdCategoria", artic.Tipo.Id);
                datos.setearParametro("@ImagenUrl", artic.UrlImagen);
                datos.setearParametro("@Precio", artic.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
                //Console.WriteLine($"Consulta generada: insert into DISCOS (Titulo, FechaLanzamiento, CantidadCanciones) values ('{nuevo.Nombre}', '{nuevo.Cod}', {nuevo.Descripcion})");
            }
        }

        public void modificar(Articulo artic)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                datos.setearParametro("@Id", artic.Id);
                datos.setearParametro("@Codigo", artic.Cod);
                datos.setearParametro("@Nombre", artic.Nombre);
                datos.setearParametro("@Descripcion", artic.Descripcion);
                datos.setearParametro("@IdMarca", artic.Compania.Id);
                datos.setearParametro("@IdCategoria", artic.Tipo.Id);
                datos.setearParametro("@ImagenUrl", artic.UrlImagen);
                datos.setearParametro("@Precio", artic.Precio);

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

        public void modificarConSP(Articulo artic)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarArticulo");
                datos.setearParametro("@Id", artic.Id);
                datos.setearParametro("@Codigo", artic.Cod);
                datos.setearParametro("@Nombre", artic.Nombre);
                datos.setearParametro("@Descripcion", artic.Descripcion);
                datos.setearParametro("@IdMarca", artic.Compania.Id);
                datos.setearParametro("@IdCategoria", artic.Tipo.Id);
                datos.setearParametro("@ImagenUrl", artic.UrlImagen);
                datos.setearParametro("@Precio", artic.Precio);
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

        public void eliminarFisico(int id)
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


        public List<Articulo> filtrar(string columna, string criterio, string filtro, string codigo)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            Console.WriteLine("Dentro de metodo filtrar");
            try
            {
                string consulta = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion DescMarca, C.Descripcion DescCategoria, ImagenUrl, A.IdMarca, A.IdCategoria, Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and  A.IdCategoria = C.Id and Nombre is not null and Codigo IS NOT NULL and Codigo NOT LIKE '#%' AND ";

                if (columna == "Nombre" || columna == "Codigo" || columna == "Marca" || columna == "Categoria" || columna == "Descripcion")
                {
                    if (columna == "Marca")
                    {
                        columna = "M.Descripcion";
                    }
                    else if (columna == "Categoria")
                    {
                        columna = "C.Descripcion";
                    }

                    if (criterio == "Comienza con")
                        consulta += columna + " like '" + filtro + "%'";
                    else if (criterio == "Termina con")
                        consulta += columna + " like '%" + filtro + "'";
                    else
                        consulta += columna + " like '%" + filtro + "%'";

                    Console.WriteLine(consulta);
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

                Console.WriteLine("SQL " + consulta);
                datos.setearConsulta(consulta);
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
