<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="Administrador_Editar_Perfil.aspx.cs" Inherits="Presentacion.Administrador_Editar_Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Editar datos de usuario</h1>
        </div>
        <div class="mdl-card__supporting-text">
            <ul class="demo-list-control mdl-list">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Usuario:</label>
                        <span id="Editar_Perfil_Usuario_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Usuario" runat="server"  ReadOnly="false" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Usuario"></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ControlToValidate="Editar_Perfil_Usuario" ErrorMessage="Introduce el nombre de usuario" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegExUsuario" runat="server" ErrorMessage="No se admiten caracteres especiales o nombres muy largos o cortos" ControlToValidate="Editar_Perfil_Usuario" ValidationExpression="\w{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Nombre Completo:</label>
                        <span id="Editar_Perfil_Nombre_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Nombre" runat="server"  ReadOnly="false" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Nombre"></label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Email:</label>
                        <span id="Editar_Perfil_Email_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Email" runat="server" ReadOnly="false" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Correo"></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="Editar_Perfil_Email" ErrorMessage="Introduce el email" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="Editar_Perfil_Email" ErrorMessage="Esto no es un email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Fecha Registro:</label>
                        <span id="Editar_Perfil_Fecha_Resgistro_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Fecha_Resgistro" runat="server"  ReadOnly="true" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Nombre"></label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Fecha Ultimo Ingreso:</label>
                        <span id="Editar_Perfil_Fecha_Ingreso_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Fecha_Ingreso" runat="server"  ReadOnly="true" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Nombre"></label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Verificar Estado:</label>
                        <asp:Label ID="Editar_Perfil_Visibilidad" AssociatedControlID="Editar_Perfil_Visibilidad_Switch" runat="server" CssClass="mdl-switch mdl-js-switch mdl-js-ripple-effect">
                            <asp:CheckBox ID="Editar_Perfil_Visibilidad_Switch" ClientIDMode="Static" Enabled="true" runat="server" OnClick="onClickEvent_VisibilitySwitch()"/>
                            <asp:Label ID="Editar_Perfil_Visibilidad_Label" ClientIDMode="Static" runat="server" Text="No Verificado" CssClass="mdl-switch__label" ></asp:Label>
                        </asp:Label>
                    </span>
                </li>
                </ul>
        </div>
        <div class="mdl-card__actions mdl-card--border">
            <asp:LinkButton ID="Editar_Perfil_Guardar" runat="server" visible="true" OnClientClick="return confirm('¿Está seguro que desea editar realmente a este usuario?');" OnClick="Editar_Perfil_Guardar_Click" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <i class="material-icons">save</i>
                Guardar Cambios
            </asp:LinkButton>
            <asp:TextBox ID="Editar_Perfil_ID" runat="server" visible="true"></asp:TextBox>
        </div>
    </div>
    <!-- Pequeño script para cambiar el texto de la etiqueta de la visibilidad del perfil ya que desde ASP es imposible -->
    <script>
        function onClickEvent_VisibilitySwitch() {
            var switch_status = document.getElementById("Editar_Perfil_Visibilidad_Switch");
            var switch_label = document.getElementById("Editar_Perfil_Visibilidad_Label");
            if (switch_status.checked)
            {
                switch_label.innerHTML = "Verificado";
            }
            else
            {
                switch_label.innerHTML = "No Verificado";
            }
        }
    </script>
</asp:Content>
