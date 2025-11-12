<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="catalogo_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/estilos-default.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="container-fluid">
        <div class="row">
            <!-- 🧱 Columna lateral izquierda para filtros -->
            <div class="col-md-3">
                <div class="filtros p-3 rounded">
                    <h5>Filtros</h5>
                    <hr />
                    <p>Opciones de filtrado irán aquí</p>
                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" id="chkSamsung" />
                        <label class="form-check-label" for="chkSamsung">Samsung</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" id="chkApple" />
                        <label class="form-check-label" for="chkApple">Apple</label>
                    </div>
                    <div class="mt-4 text-end">
                        <asp:Button ID="btnAplicarFiltros" runat="server" CssClass="btn btn-primary" Text="Aplicar" />
                    </div>
                </div>
            </div>

            <!-- 🧩 Columna principal con ordenamiento y productos -->
            <div class="col-md-9">
                <div class="mb-4 d-flex justify-content-between align-items-center flex-wrap gap-2">
                    <div class="input-group" style="max-width: 400px;">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Buscar productos..." />
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-outline-secondary" Text="🔍" />
                    </div>

                    <div class="d-flex align-items-center">
                        <label for="ddlOrden" class="form-label me-2 mb-0">Ordenar por:</label>
                        <asp:DropDownList ID="ddlOrden" runat="server" CssClass="form-select w-auto">
                            <asp:ListItem Text="Precio: mayor a menor" Value="desc" />
                            <asp:ListItem Text="Precio: menor a mayor" Value="asc" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row g-4">
                    <asp:Repeater runat="server" ID="repRepetidor" OnItemCommand="repRepetidor_ItemCommand">
                        <ItemTemplate>
                            <div class="col-12 col-sm-6 col-md-4 col-lg-3 d-flex">
                                <div class="card-producto w-100 position-relative">
                                    <div class="imagen">
                                        <img src="<%#Eval("UrlImagen") %>" alt="Imagen del producto" />
                                    </div>
                                    <div class="contenido">
                                        <div class="marca"><%#Eval("Compania") %></div>
                                        <div class="titulo"><%#Eval("Nombre") %></div>
                                        <div>
                                            <span class="precio">$<%# string.Format("{0:N2}", Eval("Precio")) %></span>
                                        </div>
                                    </div>

                                    <%--<%-- Botón invisible sobre la card solo si hay sesión --%>
                                    <% if (Session["usuario"] != null)
                                        { %>
                                    <asp:Button runat="server"
                                        CommandName="VerDetalle"
                                        CommandArgument='<%# Eval("Id") %>'
                                        Text="Ver detalle"
                                        CssClass="btn btn-primary" />
                                    <% } %>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="modalDetalle" runat="server" tabindex="-1" aria-labelledby="modalDetalleLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <%--<div class="modal-header">
                    <h5 class="modal-title" id="modalDetalleLabel">Detalle del artículo</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>--%>

                <div class="modal-body">
                    <asp:HiddenField ID="txtId" runat="server" />

                    <div class="row">
                        <div class="col-md-7">
                            <div class="mb-3">
                                <label for="txtNombre">Nombre</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                            </div>
                            <div class="mb-3">
                                <label for="txtCodigo">Codigo</label>
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
                            </div>

                            <div class="mb-3">
                                <label for="ddlTipo">Tipo</label>
                                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control" />
                            </div>

                            <div class="mb-3">
                                <label for="ddlMarca">Marca</label>
                                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control" />
                            </div>

                            <div class="mb-3">
                                <label for="txtPrecio">Precio</label>
                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                            </div>

                            <div class="mb-3">
                                <label for="txtDescripcion">Descripción</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>

                            <asp:Label ID="lblAviso" runat="server" CssClass="text-muted" />
                        </div>

                        <div class="col-md-5 text-center">
                            <label class="form-label">Imagen del artículo</label>
                            <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control mb-3" placeholder="Pegá el link de la imagen" />

                            <asp:Image ID="imgNuevoPerfil" runat="server"
                                ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                                CssClass="img-fluid rounded border mb-3"
                                Width="200px" />
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnGuardar" runat="server"
                        CssClass="btn btn-success"
                        Text="Guardar cambios"
                        OnClick="btnGuardar_Click" />
                    <button type="button" class="btn btn-outline-danger" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
