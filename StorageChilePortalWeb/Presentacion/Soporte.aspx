<%@ Page Title="Soporte" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Soporte.aspx.cs" Inherits="Presentacion.Soporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Soporte</h1>
        </div>
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
         </style>
        <table>
                <tr>
                    <td >
                        <div class="mdl-card__supporting-text">
                            <ul class="demo-list-control mdl-list">
                                <li class="mdl-list__item">
                                    <span class="mdl-list__item-primary-content">
                                        <i class="material-icons  mdl-list__item-avatar">email</i>
                                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                            <asp:TextBox ID="correo_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                                            <!--<input class="mdl-textfield__input" type="password" id="userpass-login-input">-->
                                            <label class="mdl-textfield__label" for="userpass-login-input">Email</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorcorreo" runat="server" ControlToValidate="correo_register" ErrorMessage="Introduce el email" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="correo_register" ErrorMessage="Esto no es un email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                                        </span>
                                    </span>
                                </li>
                                <li class="mdl-list__item">
                                    <span class="mdl-list__item-primary-content">
                                        <i class="material-icons  mdl-list__item-avatar">description</i>
                                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="5" CssClass="mdl-textfield__input"></asp:TextBox>
                                            <!--<input class="mdl-textfield__input" type="password" id="userpass-login-input">-->
                                            <label class="mdl-textfield__label" for="userpass-login-input">Mensaje</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="correo_register" ErrorMessage="Introduce el mensaje" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                                        </span>
                                    </span>
                                </li>
                            </ul>
                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                            <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit"
                            async defer></script>
                            <script type="text/javascript">
                                var onloadCallback = function () {
                                    grecaptcha.render('dvCaptcha', {
                                        'sitekey': '6LfZ-RUUAAAAAGrnxFF7Z4LCovzUAdbNyLMeboFz',
                                        'callback': function (response) {
                                            $.ajax({
                                                type: "POST",
                                                url: "Soporte.aspx/VerifyCaptcha",
                                                data: "{response: '" + response + "'}",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                success: function (r) {
                                                    var captchaResponse = jQuery.parseJSON(r.d);
                                                    if (captchaResponse.success) {
                                                        $("[id*=txtCaptcha]").val(captchaResponse.success);
                                                        $("[id*=rfvCaptcha]").hide();
                                                    } else {
                                                        $("[id*=txtCaptcha]").val("");
                                                        $("[id*=rfvCaptcha]").show();
                                                        var error = captchaResponse["error-codes"][0];
                                                        $("[id*=rfvCaptcha]").html("RECaptcha error. " + error);
                                                    }
                                                }
                                            });
                                        }
                                    });
                                };
                            </script>
                            <div id="dvCaptcha">
                            </div>
                            <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                            <asp:RequiredFieldValidator ID = "rfvCaptcha" ErrorMessage="Ingrese el Captcha." ControlToValidate="txtCaptcha"
                                runat="server" ForeColor = "Red" Display = "Dynamic" />
                        </div>
                </td>
                <td >
                    <div class="panelDer">
                        <img data-u="image" src="img/soporte1.jpg" />
                    </div>
                </td>
            </tr>
        </table>
        <div class="mdl-card__actions mdl-card--border">
            <asp:Button ID="Button_Register" runat="server" Text="Enviar" OnClick="Button_Register_Click" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" />
            <!--<button type="submit" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" id="Button_Register" onclick="Button_Register_Click">
                <b>Registrar</b>
            </button>-->
        </div>
    </div>
</asp:Content>
