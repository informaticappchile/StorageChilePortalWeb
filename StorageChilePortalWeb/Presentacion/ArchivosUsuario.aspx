<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ArchivosUsuario.aspx.cs" Inherits="Presentacion.WebForm1" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">¡Mis archivos!</h1>
        </div>
        <div id ="container" runat="server" visible ="true">

        </div>

        <asp:GridView ID="GridViewMostrarArchivos" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewMostrarArchivos_RowDataBound" 
            CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp listado-archivos" Visible ="false">
            <Columns>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:Image ID="icono_fichero" runat="server" CssClass="icono-listado-archvios"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Archivo" HeaderText="Archivo"  />

                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="Descarga_Boton" runat="server" OnClick="Descarga_Boton_Click" CssClass="mdl-button mdl-js-button mdl-button--icon mdl-button--colored">
                            <i class="material-icons">file_download</i>
                            <asp:HyperLink ID="Descarga" runat="server" Text=""></asp:HyperLink>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="Borrar_Boton" runat="server" OnClick="Borrar_Click" CssClass="mdl-button mdl-js-button mdl-button--icon mdl-button--colored">
                            <i class="material-icons">delete</i>
                            <asp:HyperLink ID="Borra" runat="server" Visible="false"></asp:HyperLink>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
        </asp:GridView>
    </div>
</asp:Content>
