<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdministrarMovimientos.aspx.cs" Inherits="Presentacion.AdministrarMovimientos" %>

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
            <h1 class="mdl-card__title-text">Administración de Proveedores</h1>
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
                    <asp:BoundField DataField="RazonSocial" HeaderText="Razon Social" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                    <asp:BoundField DataField="IdDocumento" HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="NumDocumento" HeaderText="Número Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="FechaDocumento" HeaderText="Fecha Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Total" HeaderText="Total"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="ID" HeaderText="Id Movimiento" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:LinkButton ID="Eliminar" runat="server" CssClass="mdl-button mdl-js-button mdl-button--icon"
                                 OnClientClick="return confirm('¿Está seguro que desea dar de baja los servicios de esta empresa?');" 
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID").ToString().TrimEnd()%>' CommandName="DEL">
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
         <div class="mdl-card__actions mdl-card--border">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Almacen/Movimiento.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Volver a Registrar</asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Almacen/MenuAlmacen.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Volver a Menú Almacén</asp:HyperLink>
        </div>
        
    </div>
</asp:Content>
