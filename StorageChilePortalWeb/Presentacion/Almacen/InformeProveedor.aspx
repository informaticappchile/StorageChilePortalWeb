﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="InformeProveedor.aspx.cs" Inherits="Presentacion.InformeProveedor" %>

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
            <h1 class="mdl-card__title-text">Informe de Movimiento de Proveedores</h1>
        </div>
        <br />
        <div style="width:100%;text-align:center;">
            <span class="mdl-list__item-primary-content">
                <i class="material-icons  mdl-list__item-avatar">search</i>
                <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <asp:DropDownList id="buscar_razon_social" runat="server" CssClass="mdl-textfield__input">
                    </asp:DropDownList>
                    <label class="mdl-textfield__label" for="buscar_razon_social">Razón Social</label>
                </span>
                <asp:Button ID="Button1" runat="server" Text="Buscar" OnClick="Button_Buscar_Click" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" />
            </span>
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
                    <asp:BoundField DataField="RazonSocial" HeaderText="Razon Social" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                    <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="NumDoc" HeaderText="Número Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="FechaDocumento" HeaderText="Fecha Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Total" HeaderText="Total"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="EstadoPago" HeaderText="Estado Pago" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
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
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Almacen/MenuAlmacen.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Volver a Menú Almacén</asp:HyperLink>
        </div>
        
    </div>
</asp:Content>
