<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="catalogo_web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /* Separador vertical en pantallas grandes */
        @media (min-width: 768px) {
            .separator {
                border-left: 4px solid #dee2e6;
                height: auto;
            }
        }

        /* Separador horizontal en pantallas pequeñas */
        @media (max-width: 767.98px) {
            .separator {
                border-left: none;
                border-top: 4px solid #dee2e6;
                margin: 2rem 0;
            }
        }
    </style>

    <div class="container">
        <div class="row justify-content-center">
            <!-- Columna Login -->
            <div class="col-md-5 d-flex flex-column align-items-center">
                <h2>Login</h2>
                <hr />
                <div class="mb-3">
                    <label class="form-label">Email:</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmailLogin" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Password:</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPasswordLogin" TextMode="Password" />
                </div>
                <div class="d-flex justify-content-center gap-2 mt-3">
                    <asp:Button Text="Ingresar" CssClass="btn btn-outline-success" ID="btnLogin" OnClick="btnLogin_Click" runat="server" />

                    <a class="btn btn-outline-danger" href="/">Volver</a>
                </div>

            </div>

            <!-- Separador visual -->
            <div class="col-md-1 separator"></div>

            <!-- Columna Registro -->
            <div class="col-md-5 ps-md-4">

                <h2>Registro</h2>
                <hr />
                <div class="mb-3">
                    <label class="form-label">Nombre:</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNombreRegistro" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Apellido:</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtApellidoRegistro" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Email:</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmailRegistro" TextMode="Email" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Password:</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPasswordRegistro" TextMode="Password" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Confirmar password:</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TextBox1" TextMode="Password" />
                </div>
                <asp:Button Text="Registrarse" CssClass="btn btn-outline-primary me-2" ID="btnRegistro" OnClick="btnRegistro_Click" runat="server" />
                <a class="btn btn-outline-secondary" href="/">Cancelar</a>

                <asp:Literal ID="litAlertaLogin" runat="server" />
                <asp:Literal ID="Literal1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
