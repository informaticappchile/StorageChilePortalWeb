<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Movimientos.aspx.cs" Inherits="Presentacion.Movimientos" %>

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
            <h1 class="mdl-card__title-text">Movimientos</h1>
        </div>
        
        <div style="overflow-x:auto">
            <table align="center">
                <tr>
                    <td >
                        <div class="panelIzq">
                            <div class="mdl-card__supporting-text">
                                <asp:Label ID="NotResponsableError_Register" runat="server" Text="No se ha ingresado un Responsable" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">assignment_turned_in</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                    <asp:DropDownList id="tipo_mov_register" runat="server" AutoPostBack="true" CssClass="mdl-textfield__input" OnDataBinding="tipoMovChangeIndex" OnTextChanged="tipoMovChangeIndex">
                                                    </asp:DropDownList>
                                                <label class="mdl-textfield__label" for="tipo_mov_register">Tipo Movimiento</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">work</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                    <asp:DropDownList id="razon_social_register" runat="server" CssClass="mdl-textfield__input" AutoPostBack="true" OnDataBinding="razonSocialChangeIndex" OnTextChanged="razonSocialChangeIndex">
                                                    </asp:DropDownList>
                                                <label class="mdl-textfield__label" for="razon_social_register">Razón Social</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">person</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="responsable_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="responsable_register">Responsable</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">note</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                    <asp:DropDownList id="tipo_doc_register" runat="server" CssClass="mdl-textfield__input">
                                                    </asp:DropDownList>
                                                <label class="mdl-textfield__label" for="tipo_doc_register">Tipo Documento</label>
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
                            <div class="mdl-card__supporting-text">
                                <asp:Label ID="NotNumDocError_Register" runat="server" Text="No se ha ingresado un número de documento" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <asp:Label ID="NotFechaDocError_Register" runat="server" Text="No se ha ingresado una fecha de documento" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">chevron_right</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="num_doc_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="false"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="num_doc_register">N° Documento</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="num_doc_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">today</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="fecha_actual_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="fecha_actual_register">Fecha Actual</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">today</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="fecha_doc_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="false"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="fecha_doc_register">Fecha Documento</label>
                                                <asp:RegularExpressionValidator ID="RegExUsuario" runat="server" ErrorMessage="Solo se admiten fechas con formato: dd-mm-yyyy. Ejemplo: 22-01-2017" ControlToValidate="fecha_doc_register" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">place</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                    <asp:DropDownList id="area_register" runat="server" CssClass="mdl-textfield__input">
                                                    </asp:DropDownList>
                                                <label class="mdl-textfield__label" for="area_register">Área</label>
                                            </span>
                                        </span>
                                    </li>
                                </ul>
                                </div>
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
                </div>
                <div style="overflow-x:auto">
            <table align="center">
                <tr>
                    <td >
                        <div class="panelIzq">
                           <div class="mdl-card__supporting-text">
                                <asp:Label ID="Label4" runat="server" Text="No se ha ingresado un número de documento" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text="No se ha ingresado una fecha de documento" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">fingerprint</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                    <asp:DropDownList id="descripcion_register" runat="server" CssClass="mdl-textfield__input" AutoPostBack="true" OnDataBinding="descripcionChangeIndex" OnTextChanged="descripcionChangeIndex">
                                                    </asp:DropDownList>
                                                <label class="mdl-textfield__label" for="descripcion_register">Descripción</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">description</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="cod_prod_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="descripcion_register">Código Producto</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">group_work</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="grupo_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="grupo_register">Grupo Producto</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">av_timer</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="unidad_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="unidad_register">Unidad Medida</label>
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
                           <div class="mdl-card__supporting-text">
                                <asp:Label ID="NotPositiveNumError" runat="server" Text="No se ha ingresado un número mayor a 0 en precio o cantidad" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">shopping_basket</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="cant_register" runat="server" CssClass="mdl-textfield__input">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="cant_register">Cantidad</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="cant_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">monetization_on</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="precio_register" runat="server" CssClass="mdl-textfield__input">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="precio_register">Precio Unitario</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="precio_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">remove_red_eye</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="obs_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="obs_register">Observaciones</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <button runat="server" class="bttn-unite bttn-md bttn-danger" onServerClick="clickIngresarMovimiento">Ingresar Movimiento</button>
                                    </li>
                                </ul>
                                </div>
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--Convertidor de Unidades Inicio-->
         <div style="overflow-x:auto;">
            <table align="center">
                <tr>
                    <td >
                        <div class="panelIzq">
                           <div class="mdl-card__supporting-text">
                                <asp:Label ID="Label6" runat="server" Text="Convertidor de Unidades" Visible="true" CssClass="mdl-card__subtitle-text mdl-color-text--black"></asp:Label>
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">shopping_basket</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="unidadtxt" ReadOnly="true" runat="server" CssClass="mdl-textfield__input">1</asp:TextBox>
                                                <label class="mdl-textfield__label" for="cant_register">Unidad</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="cant_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">shopping_basket</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="equivalenciatxt" runat="server" CssClass="mdl-textfield__input">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="cant_register">Equivalencia</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="cant_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
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
                           <div class="mdl-card__supporting-text">
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">shopping_basket</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="unidadestxt" runat="server" CssClass="mdl-textfield__input">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="cant_register">Unidades</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="cant_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">monetization_on</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="preciotxt" runat="server" CssClass="mdl-textfield__input">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="precio_register">Precio Por Unidad</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="precio_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <button runat="server" class="bttn-unite bttn-md bttn-danger" onserverclick="clickConvertir">Calcular</button>
                                    </li>
                                </ul>
                                </div>
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--Convertidor de Unidades Fin-->
        <br />
        <div>
            <asp:GridView HorizontalAlign="Center" ID="Responsive" runat="server" ARowStyle-Wrap="false"
            CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp" AutoGenerateColumns="false"
                >
                <EmptyDataTemplate>
                    No se han encontrado datos.
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="CodProducto" HeaderText="Codigo Producto" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Grupo" HeaderText="Grupo Producto"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Precio" HeaderText="Precio Unitario"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                </Columns>
                <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
                <PagerSettings PageButtonCount="4" />
            </asp:GridView>
        </div>
        <br />
        <div style="overflow-x:auto">
            <table align="center" >
                <tr>
                    <td >
                        <div class="panelIzq">
                            <div class="mdl-card__supporting-text">
                                <asp:Label ID="Label1" runat="server" Text="No se ha ingresado un Responsable" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">monetization_on</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="neto_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="neto_register">Neto</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">monetization_on</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="iva_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="iva_register">IVA</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">monetization_on</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="ila_register" runat="server" CssClass="mdl-textfield__input" >0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="responsable_register">ILA</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="ila_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
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
                            <div class="mdl-card__supporting-text">
                                <asp:Label ID="Label2" runat="server" Text="No se ha ingresado un número de documento" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <asp:Label ID="Label3" runat="server" Text="No se ha ingresado una fecha de documento" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">monetization_on</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="flete_register" runat="server" CssClass="mdl-textfield__input">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="flete_register">Flete</label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="flete_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">monetization_on</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="total_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true">0</asp:TextBox>
                                                <label class="mdl-textfield__label" for="total_register">Total</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <button runat="server" class="bttn-unite bttn-md bttn-danger" onServerClick="clickCalcular">Calcular</button>
                                    </li>
                                </ul>
                                </div>
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <br />
            <!--</ContentTemplate>
            
        </asp:UpdatePanel>-->
        
        <div class="mdl-card__actions mdl-card--border">
            <asp:LinkButton ID="Editar_Perfil_Guardar" runat="server" visible="true" OnClientClick="return confirm('¿Está seguro que desea realizar este movimiento?');" OnClick="clickGuardar" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <i class="material-icons">save</i>
                Realizar Movimiento
            </asp:LinkButton>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Almacen/AdministrarMovimientos.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">ADMINISTRAR</asp:HyperLink>
        </div>
    </div>
</asp:Content>
