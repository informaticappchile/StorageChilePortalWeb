<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuario.aspx.cs" Inherits="Presentacion.AdministrarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Administración de Usuario</h1>
        </div>
        <br />
        <asp:GridView HorizontalAlign="Center" ID="Responsive" runat="server" AutoGenerateColumns="false" CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp" CellSpacing="0" AllowPaging="True" PageSize="10">
                <EmptyDataTemplate>
                    No se han encontrado datos.
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="NombreUsu" HeaderText="UserName" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre Completo"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Correo" HeaderText="Email"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Registro"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="UltimoIngreso" HeaderText="Fecha de Último Ingreso"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Verified" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                    <asp:TemplateField HeaderText="Modificar">
                        <ItemTemplate>
                            <a href="ModificarUsuarioAdmin.aspx?ID=<%#Eval("NombreUsu") %>">
                                <i class="material-icons" >update</i>
                            </a>
                        </ItemTemplate>
                        <ItemStyle Width="24" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:LinkButton ID="Eliminar" runat="server" CssClass="mdl-button mdl-js-button mdl-button--icon">
                                <i class="material-icons">delete</i>
                            </asp:LinkButton>        
                        </ItemTemplate>
                    </asp:TemplateField>                
                </Columns>
                <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
                <PagerSettings PageButtonCount="4" />
            </asp:GridView>
    </div>
</asp:Content>
