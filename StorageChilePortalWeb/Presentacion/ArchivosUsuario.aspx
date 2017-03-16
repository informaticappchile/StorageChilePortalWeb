<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ArchivosUsuario.aspx.cs" Inherits="Presentacion.WebForm1" EnableEventValidation="false" %>

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
    <style >
        .button-folder {
            color:black;
            background: url('/CSS/Folder-96.png') no-repeat center center;
            height:100px;
            width:100px;
            background-position:center;
            margin-left: 50px;
            padding-top:77px;
        }

        .button-folder:not([disabled]):hover {
            color:black;
            background: url('/CSS/Open Folder-96.png') no-repeat center center;
            height:100px;
            width:100px;
            background-position:center;
            padding-top:77px;
        }

        .button-folder[disabled] {
            color : rgba(0, 0, 0, 0.26);
            background-color: transparent;
        }

        .button-folder-filtrado {
            color:black;
            background: url('/CSS/Folder-97.png') no-repeat center center;
            height:100px;
            width:100px;
            background-position:center;
            margin-left: 50px;
            padding-top:77px;
        }

        .button-folder-filtrado:not([disabled]):hover {
            color:black;
            background: url('/CSS/Open Folder-97.png') no-repeat center center;
            height:100px;
            width:100px;
            background-position:center;
            padding-top:77px;
        }

        .button-folder-filtrado[disabled] {
            color : rgba(0, 0, 0, 0.26);
            background-color: transparent;
        }
    </style>

    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Mi bodega</h1>
        </div>
        <div style="width:100%;text-align:center;">
            <span class="mdl-list__item-primary-content">
                <i class="material-icons  mdl-list__item-avatar">search</i>
                <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <asp:TextBox ID="buscar_Rut" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                    <label class="mdl-textfield__label" for="userpass-login-input">Rut</label>
                </span>
                <asp:Button ID="Button1" runat="server" Text="Buscar" OnClick="Button_Buscar_Click" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" />
            </span>
        </div>

        <div id ="container" runat="server" visible ="true">

        </div>

        <asp:GridView ID="GridViewMostrarArchivos" runat="server" AutoGenerateColumns="False" OnRowCommand="Responsive_RowCommand" OnRowDataBound="GridViewMostrarArchivos_RowDataBound" 
            CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp listado-archivos" Visible ="false">
            <Columns>

                <asp:BoundField DataField="NombreAsociado" HeaderText="Nombre"  />

                <asp:BoundField DataField="RutAsociado" HeaderText="Rut"  />

                <asp:BoundField DataField="ArchivoAsociado" HeaderText="Archivo"  />

                <asp:TemplateField HeaderText="Formato">
                    <ItemTemplate>
                        <asp:Image ID="icono_fichero" runat="server" CssClass="icono-listado-archvios"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Descargar">
                    <ItemTemplate>
                        <asp:LinkButton ID="Descarga_Boton" runat="server" CssClass="mdl-button mdl-js-button mdl-button--icon mdl-button--colored"
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ArchivoAsociado").ToString().TrimEnd()%>' CommandName="DOWNLOAD">
                            <i class="material-icons">file_download</i>
                            <asp:HyperLink ID="Descarga" runat="server" Text=""></asp:HyperLink>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
        </asp:GridView>
        <div id="botonesPie" runat="server" class="mdl-card__actions mdl-card--border" visible="false">
            <asp:Button ID="Button_Volver" runat="server" Text="Volver" OnClick="Button_Volver_Click" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect"/>
        </div>
    </div>
</asp:Content>
