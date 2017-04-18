<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="Editar_Perfil.aspx.cs" Inherits="Presentacion.Editar_Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Actualiza tus datos</h1>
        </div>
        <div class="mdl-card__supporting-text">
            <ul class="demo-list-control mdl-list">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Usuario:</label>
                        <span id="Editar_Perfil_Usuario_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Usuario" runat="server"  ReadOnly="True" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Usuario"></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ControlToValidate="Editar_Perfil_Usuario" ErrorMessage="Introduce el nombre de usuario" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegExUsuario" runat="server" ErrorMessage="No se admiten caracteres especiales o nombres muy largos o cortos" ControlToValidate="Editar_Perfil_Usuario" ValidationExpression="\w{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Nombre:</label>
                        <span id="Editar_Perfil_Nombre_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Nombre" runat="server"  ReadOnly="True" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Nombre"></label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Email:</label>
                        <span id="Editar_Perfil_Email_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Email" runat="server" ReadOnly="True" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Correo"></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="Editar_Perfil_Email" ErrorMessage="Introduce el email" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="Editar_Perfil_Email" ErrorMessage="Esto no es un email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Contraseña:</label>
                        <span id="Editar_Perfil_Contraseña_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Contraseña" runat="server" ReadOnly="True" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Contrasena"></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorcontraseña" runat="server" ErrorMessage="Introduce la contraseña" ControlToValidate="Editar_Perfil_Contraseña" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegExLongitudContraseña" runat="server" ErrorMessage="Debe de tener entre 4 y 30 caracteres" ControlToValidate="Editar_Perfil_Contraseña" ValidationExpression="\S{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                </ul>
        </div>
        <div class="mdl-card__actions mdl-card--border">
            <asp:LinkButton ID="Editar_Perfil_Editar" runat="server" visible="true" OnClick="Editar_Perfil_Editar_Click" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <i class="material-icons">edit</i>
                Editar Datos
            </asp:LinkButton>
            <asp:LinkButton ID="Editar_Perfil_Guardar" runat="server" visible="false" OnClick="Editar_Perfil_Guardar_Click" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <i class="material-icons">save</i>
                Guardar Cambios
            </asp:LinkButton>
            <asp:TextBox ID="Editar_Perfil_ID" runat="server" visible="false"></asp:TextBox>
        </div>
    </div>
</asp:Content>
