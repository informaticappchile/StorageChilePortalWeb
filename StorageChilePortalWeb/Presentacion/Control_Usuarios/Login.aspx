<%@ Page Title="Login" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Prensentacion.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">¡Bienvenido! Nos alegramos de verte</h1>            
        </div>
        <div class="mdl-card__supporting-text">
            <asp:Label ID="WrongPasswordError_Login" runat="server" Text="Contraseña Incorrecta" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
            <asp:Label ID="UserNotExistsError_Login" runat="server" Text="Usuario no encontrado" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
            <asp:Label ID="UserNotVerifiedError_Login" runat="server" Text="Usuario no verificado. Comprueba tu correo" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"> </asp:Label>
            <ul class="demo-list-control mdl-list">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">person</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="username_login_input" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="username-login-input">Usuario</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ControlToValidate="username_login_input" ErrorMessage="Introduce el nombre de usuario" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegExUsuario" runat="server" ErrorMessage="No se admiten caracteres especiales o nombres muy largos o cortos" ControlToValidate="username_login_input" ValidationExpression="\w{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">vpn_key</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="password_login_input" runat="server" CssClass="mdl-textfield__input" TextMode="Password"></asp:TextBox>
                            <label class="mdl-textfield__label" for="userpass-login-input">Contraseña</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="Introduce la contraseña" ControlToValidate="password_login_input" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                        </span>
                    </span>
                </li>
            </ul>
        </div>
        <div class="mdl-card__actions mdl-card--border">
            <asp:Button ID="Button_Login" runat="server" Text="Iniciar Sesión" OnClick="Button_Login_Click" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" />
        </div>
    </div>
</asp:Content>