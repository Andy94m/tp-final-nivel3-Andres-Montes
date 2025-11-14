using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using negocio;
using Negocio;

namespace catalogo_web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            litAlertaLogin.Text = ""; // Limpia alertas previas

            string email = txtEmailLogin.Text.Trim();
            string password = txtPasswordLogin.Text.Trim();

            // Validación de campos
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                mostrarAlertaLogin("⚠️ Por favor completá todos los campos.", "danger");
                return;
            }

            Usuario user = new Usuario
            {
                Email = email,
                Pass = password
            };

            UserNegocio negocio = new UserNegocio();
            bool loginExitoso = negocio.Login(user);

            if (loginExitoso)
            {
                Session["usuario"] = user;
                Response.Redirect("Default.aspx");
            }
            else
            {
                mostrarAlertaLogin("❌ Credenciales incorrectas. Verificá tu email y contraseña.", "danger");
            }
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Literal1.Text = "";

            string nombre = txtNombreRegistro.Text.Trim();
            string apellido = txtApellidoRegistro.Text.Trim();
            string email = txtEmailRegistro.Text.Trim();
            string pass = txtPasswordRegistro.Text.Trim();
            string confirm = TextBox1.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirm))
            {
                mostrarAlertaRegistro("⚠️ Todos los campos son obligatorios.", "danger");
                return;
            }

            if (pass != confirm)
            {
                mostrarAlertaRegistro("❌ Las contraseñas no coinciden.", "danger");
                return;
            }

            Usuario nuevo = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Pass = pass,
                Admin = false
            };

            UserNegocio negocio = new UserNegocio();
            int id = negocio.insertarNuevo(nuevo);

            if (id > 0)
            {
                nuevo.Id = id;
                Session["usuario"] = nuevo;
                Response.Redirect("Default.aspx");
            }
            else
            {
                mostrarAlertaRegistro("❌ No se pudo registrar el usuario. Intentalo nuevamente.", "danger");
            }
        }

        private void mostrarAlertaLogin(string mensaje, string tipo)
        {
            litAlertaLogin.Text = $@"
                <div class='alert alert-{tipo} alert-dismissible fade show mt-3 w-100' role='alert'>
                    {mensaje}
                    <button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Cerrar'></button>
                </div>";
        }

        private void mostrarAlertaRegistro(string mensaje, string tipo)
        {
            Literal1.Text = $@"
                <div class='alert alert-{tipo} alert-dismissible fade show mt-3 w-100' role='alert'>
                    {mensaje}
                    <button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Cerrar'></button>
                </div>";
        }
    }
}
