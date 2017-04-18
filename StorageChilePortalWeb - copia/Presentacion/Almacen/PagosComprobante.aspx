<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="PagosComprobante.aspx.cs" Inherits="Presentacion.PagosComprobante" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
    <style>
            img {
                max-width: 100%;
                height: auto;
            }
            .box
            {
                width:20px;
                height:20px;
            }
            .contenedor
            {   
                align-content:center;
                display:grid;
            }
            .panelIzq
            {
                padding: 0px;
                margin: 0px auto 0px auto;
                float: left;
                text-align: center;
                height: auto;
                max-width:100%;
            }

            .panelDer
            {
                padding: 0px;
                margin: 0px auto 0px auto;
                float: right;
                height: auto;
                text-align: center;
                max-width:100%;
            }
            ImageButton
            {
                border:hidden;
            }
         </style>
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Comprobante de Pago</h1>
        </div>
        
        <div style="overflow-x:auto">
            <table align="center">
                <tr>
                    <td >
                        <div class="panelIzq">
                            <div class="mdl-card__supporting-text">
                                <asp:Label ID="NotNumChequeError_Register" runat="server" Text="No se ha ingresado un número de cheque" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">today</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="fecha_pago_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="fecha_pago_register">Fecha Comprobante</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">assignment_turned_in</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="tipo_pago_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="tipo_pago_register">Tipo Pago</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">chevron_right</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="num_cheque_register" runat="server" Visible="false" CssClass="mdl-textfield__input" ReadOnly="true">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="num_cheque_register">N° Cheque</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="num_cheque_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegExUsuario" runat="server" ErrorMessage="No se admiten más de 10 digitos" ControlToValidate="num_cheque_register" ValidationExpression="^[\s\S]{0,10}$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">work</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="razon_social_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="razon_social_register">Razón Social</label>
                                            </span>
                                        </span>
                                    </li>
                                </ul>
                            </div>
                            <div class="box"></div>
                        </div>
                    </td>
                    <td >
                        <div class="panelDer">
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
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
                </Columns>
                <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
                <PagerSettings PageButtonCount="4" />
            </asp:GridView>
        </div>
        <br />
        <div class="mdl-card__actions mdl-card--border">
            <asp:LinkButton ID="Editar_Perfil_Guardar" runat="server" visible="true" OnClientClick="return confirm('¿Está seguro que desea imprimir este pago?');" OnClick="clickGuardar" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <i class="material-icons">save</i>
                Imprimir Pago
            </asp:LinkButton>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Almacen/MenuAlmacen.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Volver a Menú Almacén</asp:HyperLink>
        </div>
        
    </div>
</asp:Content>
