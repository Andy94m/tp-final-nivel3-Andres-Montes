<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="catalogo_web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 0.8rem;
        }

        .perfil-card {
            background-color: #1f1f1f;
            border-radius: 12px;
            padding: 2rem;
            box-shadow: 0 0 10px rgba(0,0,0,0.3);
            color: #fff;
        }

        .perfil-img {
            width: 150px;
            height: 150px;
            object-fit: cover;
            border-radius: 50%;
            border: 3px solid #6c757d;
        }
    </style>

    <script>
        function validar() {
            const txtApellido = document.getElementById("txtApellido");
            const txtNombre = document.getElementById("txtNombre");

            let valido = true;

            if (txtNombre.value.trim() === "") {
                txtNombre.classList.add("is-invalid");
                txtNombre.classList.remove("is-valid");
                valido = false;
            } else {
                txtNombre.classList.remove("is-invalid");
                txtNombre.classList.add("is-valid");
            }

            if (txtApellido.value.trim() === "") {
                txtApellido.classList.add("is-invalid");
                txtApellido.classList.remove("is-valid");
                valido = false;
            } else {
                txtApellido.classList.remove("is-invalid");
                txtApellido.classList.add("is-valid");
            }

            return valido;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-8 perfil-card">
                <h2 class="text-center mb-4">Mi Perfil</h2>
                <div class="row">
                    <!-- Datos personales -->
                    <div class="col-md-7">
                        <div class="mb-3">
                            <label class="form-label">Email</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Nombre</label>
                            <asp:TextBox runat="server" CssClass="form-control" ClientIDMode="Static" ID="txtNombre" />
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Apellido</label>
                            <asp:TextBox ID="txtApellido" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="20" />
                        </div>
                    </div>

                    <!-- Imagen de perfil -->
                    <div class="col-md-5 text-center">
                        <label class="form-label">Imagen de Perfil</label>
                        <input type="file" id="txtImagen" runat="server" class="form-control mb-3" />
                        <asp:Image ID="imgNuevoPerfil" ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                            runat="server" CssClass="perfil-img mb-3" />
                    </div>
                </div>

                <!-- Botones -->
                <div class="text-center mt-4">
                    <%--<asp:Button Text="Guardar" CssClass="btn btn-outline-primary me-2" OnClientClick="return validar()" OnClick="btnGuardar_Click" ID="btnGuardar" runat="server" />--%>
                    <a href="/" class="btn btn-outline-secondary">Regresar</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

