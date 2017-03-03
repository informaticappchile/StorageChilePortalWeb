﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ArchivosUsuario.aspx.cs" Inherits="Presentacion.WebForm1" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style >
        .button-folder {
            padding: 10px , 0px , -20px , 0px;
            border: 1px solid black;
            background-image: url('/CSS/ic_folder_black_24dp_1x.png');
            background-repeat: no-rpeeat;
            background-position: center top;
            background-attachment: fixed;
            background-origin: content;
    
        }
    </style>
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Mis archivos</h1>
        </div>
        <div id ="container" runat="server" visible ="true">

        </div>

        <asp:GridView ID="GridViewMostrarArchivos" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewMostrarArchivos_RowDataBound" 
            CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp listado-archivos" Visible ="false">
            <Columns>

                <asp:BoundField DataField="NombreAsociado" HeaderText="Nombre"  />

                <asp:BoundField DataField="RutAsociado" HeaderText="Rut"  />

                <asp:BoundField DataField="ArchivoAsociado" HeaderText="Archivo"  />

                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:Image ID="icono_fichero" runat="server" CssClass="icono-listado-archvios"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Descargar">
                    <ItemTemplate>
                        <asp:LinkButton ID="Descarga_Boton" runat="server" OnClick="Descarga_Boton_Click" CssClass="mdl-button mdl-js-button mdl-button--icon mdl-button--colored">
                            <i class="material-icons">file_download</i>
                            <asp:HyperLink ID="Descarga" runat="server" Text=""></asp:HyperLink>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
        </asp:GridView>
    </div>
</asp:Content>
