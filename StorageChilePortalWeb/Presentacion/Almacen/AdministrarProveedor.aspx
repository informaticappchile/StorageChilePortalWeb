<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdministrarProveedor.aspx.cs" Inherits="Presentacion.AdministrarProveedor" %>

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
                >
                <EmptyDataTemplate>
                    No se han encontrado datos.
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="RazonSocial" HeaderText="Nombre Empresa" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                    <asp:BoundField DataField="Rut" HeaderText="Rut"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Vendedor" HeaderText="Vendedor"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Ciudad" HeaderText="Ciudad"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Fono" HeaderText="Fono"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:TemplateField HeaderText="Modificar">
                        <ItemTemplate>
                            <a href="/Almacen/Editar_Proveedor.aspx?ID=<%#Eval("RazonSocial") %>">
                                <i class="material-icons" >update</i>
                            </a>
                        </ItemTemplate>
                        <ItemStyle Width="24" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
                <PagerSettings PageButtonCount="4" />
            </asp:GridView>
        </div>
        <br />
         <div class="mdl-card__actions mdl-card--border">
            <asp:LinkButton ID="ExportarExcel" runat="server" visible="true" OnClick="ClickExportToExcel" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <img data-u="image" src="img/excelv2.png" class="icono-web"/>
                Exportar a Excel
            </asp:LinkButton>
            <asp:LinkButton ID="ExportarPdf" runat="server" visible="true" OnClick="ClickExportToPdf" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <img data-u="image" src="img/PDF-48.png" class="icono-web"/>
                Exportar a Pdf
            </asp:LinkButton>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Almacen/RegisterProveedor.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Volver a Registrar</asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Almacen/MenuAlmacen.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Volver a Menú Almacén</asp:HyperLink>
        </div>
        
    </div>
</asp:Content>
