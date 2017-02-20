<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdmistrarUsuario.aspx.cs" Inherits="Presentacion.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Administración de Usuario</h1>
        </div>
        <asp:GridView ID="GridViewMostrarTodo" runat="server" AutoGenerateColumns="False" CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp">
            <Columns>
                <asp:BoundField DataField="nombre" HeaderText="Nombre Archivo" />
                <asp:BoundField DataField="ID" HeaderText="Descripcion" />
                <asp:BoundField DataField="descripcion" HeaderText="Fecha creacion" />
                <asp:BoundField HeaderText="Propietario" />
            </Columns>
            <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
        </asp:GridView>
    </div>
</asp:Content>
