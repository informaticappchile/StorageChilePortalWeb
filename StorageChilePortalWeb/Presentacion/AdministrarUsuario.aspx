﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuario.aspx.cs" Inherits="Presentacion.AdministrarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable = no"/>
    <link href="CSS/basic.css" type="text/css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.responsivetable.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setupResponsiveTables();
        });

        function setupResponsiveTables() {
            $('.mdl-data-table').responsiveTable();
            $('.mdl-js-data-table').responsiveTable();
            $('.mdl-shadow--2dp').responsiveTable();
        }
    </script>
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Administración de Usuario</h1>
        </div>
        <br />
        <div>
            <asp:GridView HorizontalAlign="Center" ID="Responsive" runat="server" ARowStyle-Wrap="false"
            CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp" AutoGenerateColumns="false"
                OnRowCommand="Responsive_RowCommand">
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
                            <a href="Administrador_Editar_Perfil.aspx?ID=<%#Eval("NombreUsu") %>">
                                <i class="material-icons" >update</i>
                            </a>
                        </ItemTemplate>
                        <ItemStyle Width="24" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:LinkButton ID="Eliminar" runat="server" CssClass="mdl-button mdl-js-button mdl-button--icon"
                                 OnClientClick="return confirm('¿Está seguro que desea eliminar realmente a este usuario?');" 
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"NombreUsu").ToString().TrimEnd()%>' CommandName="DEL">
                                <i class="material-icons">delete</i>
                            </asp:LinkButton>        
                        </ItemTemplate>
                    </asp:TemplateField>                
                </Columns>
                <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
                <PagerSettings PageButtonCount="4" />
            </asp:GridView>
        </div>
        <br />
        <div>
            
        </div>
        
    </div>
</asp:Content>
