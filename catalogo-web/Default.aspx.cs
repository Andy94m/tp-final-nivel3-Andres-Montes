using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace catalogo_web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulos = negocio.listarConSP();

            if (!IsPostBack)
            {
                repRepetidor.DataSource = ListaArticulos;
                repRepetidor.DataBind();

                cargarDropDownList();
            }
        }
        protected void repRepetidor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Articulo aux = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            Usuario usuario = (Usuario)Session["usuario"];
            bool esAdmin = usuario != null && usuario.Admin == true;
            cargarDropDownList();

            if (e.CommandName == "VerDetalle")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                ViewState["IdArticulo"] = id;
                aux = negocio.buscarId(id);

                Debug.WriteLine("ID del artículo seleccionado: " + id);
                Debug.WriteLine("Nombre: " + aux.Nombre);
                Debug.WriteLine("Marca: " + aux.Tipo.Descripcion);

                txtNombre.Text = aux.Nombre;
                txtCodigo.Text = aux.Cod;
                ddlTipo.SelectedValue = aux.Tipo.Id.ToString();
                ddlMarca.SelectedValue = aux.Compania.Id.ToString();
                txtDescripcion.Text = aux.Descripcion;
                txtPrecio.Text = aux.Precio.ToString("N2");
                txtUrlImagen.Text = aux.UrlImagen;

                imgNuevoPerfil.ImageUrl = string.IsNullOrEmpty(aux.UrlImagen)
                    ? "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                    : aux.UrlImagen;

                txtNombre.Enabled = esAdmin;
                txtCodigo.Enabled = esAdmin;
                ddlTipo.Enabled = esAdmin;
                ddlMarca.Enabled = esAdmin;
                txtDescripcion.Enabled = esAdmin;
                txtPrecio.Enabled = esAdmin;
                txtUrlImagen.Enabled = esAdmin;
                btnGuardar.Visible = esAdmin;

                imgNuevoPerfil.ImageUrl = string.IsNullOrEmpty(aux.UrlImagen) ? "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg" : aux.UrlImagen;

                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", $"var modal = new bootstrap.Modal(document.getElementById('{modalDetalle.ClientID}')); modal.show();", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo actualizado = new Articulo();

                actualizado.Id = (int)ViewState["IdArticulo"];
                Debug.WriteLine("Id del modal: " + actualizado.Id);
                actualizado.Nombre = txtNombre.Text;
                actualizado.Cod = txtCodigo.Text;
                actualizado.Descripcion = txtDescripcion.Text;
                actualizado.Precio = decimal.Parse(txtPrecio.Text);
                actualizado.UrlImagen = txtUrlImagen.Text;

                actualizado.Tipo = new Categorias { Id = int.Parse(ddlTipo.SelectedValue) };
                actualizado.Compania = new Marcas { Id = int.Parse(ddlMarca.SelectedValue) };

                negocio.modificarConSP(actualizado);

                ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModal",
                    $"var modal = bootstrap.Modal.getInstance(document.getElementById('{modalDetalle.ClientID}')); modal.hide();", true);
            }
        }

        private void cargarDropDownList()
        {
            MarcasNegocio marcasNegocio = new MarcasNegocio();
            List<Marcas> listaMarcas = marcasNegocio.listar();
            ddlMarca.DataSource = listaMarcas;
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();

            CategoriasNegocio categoriasNegocio = new CategoriasNegocio();
            List<Categorias> listaCategorias = categoriasNegocio.listar();
            ddlTipo.DataSource = listaCategorias;
            ddlTipo.DataTextField = "Descripcion";
            ddlTipo.DataValueField = "Id";
            ddlTipo.DataBind();
        }

    }
}