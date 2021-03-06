﻿<%@ Page Title="Login" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Restablecer_Password.aspx.cs" Inherits="Presentacion.Restablecer_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">¡Bienvenido! Nos alegramos de verte</h1>            
        </div>
        <div class="mdl-card__supporting-text">
            
            <asp:Label ID="UserNotification_RP" runat="server" Text="Para restablecer su contraseña escriba su nombre de usuario o su correo." Visible="true" CssClass="mdl-card__subtitle-text mdl-color-text--blue"> </asp:Label>
            <asp:Label ID="WrongUserError_RP" runat="server" Text="usuario o email incorrecto vuelva a intentar." Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
            <ul class="demo-list-control mdl-list">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">person</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="username_rp_input" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="username-login-input">Usuario</label>
                            <asp:RegularExpressionValidator ID="RegExUsuario" runat="server" ErrorMessage="No se admiten caracteres especiales o nombres muy largos o cortos" ControlToValidate="username_rp_input" ValidationExpression="\w{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">email</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="correo_rp_input" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <!--<input class="mdl-textfield__input" type="password" id="userpass-login-input">-->
                            <label class="mdl-textfield__label" for="userpass-login-input">Email</label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="correo_rp_input" ErrorMessage="Esto no es un email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
            </ul>
        </div>
        <div class="mdl-card__actions mdl-card--border">
            <asp:Button ID="Button_RP" runat="server" Text="Enviar" OnClick="Button_RP_Click" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" />
        </div>
    </div>
</asp:Content>