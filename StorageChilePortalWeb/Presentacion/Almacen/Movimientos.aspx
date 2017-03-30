<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Movimientos.aspx.cs" Inherits="Presentacion.Movimientos" %>

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
        <!--<asp:ScriptManager runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>-->
        <div>
            <table class="mdl-data-table mdl-js-data-table mdl-shadow--2dp">
                <tr>
                    <td >
                        <div class="panelIzq">
                            <div class="mdl-card__supporting-text">
                                <asp:Label ID="NotResponsableError_Register" runat="server" Text="No se ha ingresado un Responsable" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
                                <ul class="demo-list-control mdl-list">
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">group_work</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                    <asp:DropDownList id="tipo_mov_register" runat="server" CssClass="mdl-textfield__input">
                                                    </asp:DropDownList>
                                                <label class="mdl-textfield__label" for="tipo_mov_register">Tipo Movimineto</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">group_work</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                    <asp:DropDownList id="razon_social_register" runat="server" CssClass="mdl-textfield__input">
                                                    </asp:DropDownList>
                                                <label class="mdl-textfield__label" for="razon_social_register">Razon Social</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">fingerprint</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="responsable_register" runat="server" CssClass="mdl-textfield__input" Enabled="false"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="responsable_register">Responsable</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">group_work</i>
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
                                            <i class="material-icons  mdl-list__item-avatar">fingerprint</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="num_doc_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="false"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="num_doc_register">N° Documento</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">fingerprint</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="fecha_actual_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="true"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="fecha_actual_register">Fecha Actual</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">fingerprint</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                <asp:TextBox ID="fecha_doc_register" runat="server" CssClass="mdl-textfield__input" ReadOnly="false"></asp:TextBox>
                                                <label class="mdl-textfield__label" for="fecha_doc_register">Fecha Documento</label>
                                            </span>
                                        </span>
                                    </li>
                                    <li class="mdl-list__item">
                                        <span class="mdl-list__item-primary-content">
                                            <i class="material-icons  mdl-list__item-avatar">group_work</i>
                                            <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                                    <asp:DropDownList id="area_register" runat="server" CssClass="mdl-textfield__input">
                                                    </asp:DropDownList>
                                                <label class="mdl-textfield__label" for="area_register">Área</label>
                                            </span>
                                        </span>
                                    </li>
                                </ul>
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
                </div>
        <div>
            <table>
                <tr>
                    <td >
                        <div class="panelIzq">
                           
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
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                    <asp:BoundField DataField="CodProducto" HeaderText="Codigo Producto"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="CantMinStock" HeaderText="Email"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="Grupo" HeaderText="Grupo Producto"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad de Medida"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                    <asp:TemplateField HeaderText="Modificar">
                        <ItemTemplate>
                            <a href="/Almacen/Editar_Producto.aspx?ID=<%#Eval("CodProducto") %>">
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
        <div>
            
        </div>
            <!--</ContentTemplate>
            
        </asp:UpdatePanel>-->

        
    </div>
</asp:Content>
