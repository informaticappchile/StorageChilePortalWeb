<%@ Page Title="Register" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RegisterProveedor.aspx.cs" Inherits="Presentacion.RegisterProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Registrar Nuevo Proveedor</h1>
        </div>
        <div class="mdl-card__supporting-text">
            <asp:Label ID="UsernameExistsError_Register" runat="server" Text="Proveedor ya registrado" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
            <asp:Label ID="EmailExistsError_Register" runat="server" Text="Correo electrónico ya registrado" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
            <ul class="demo-list-control mdl-list">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">person</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="vendedor_name_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="vendedor_name_register">Vendedor</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ControlToValidate="vendedor_name_register" ErrorMessage="Introduce el nombre del vendedor" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">work</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="razon_social_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="razon_social_register">Razón Social</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="razon_social_register" ErrorMessage="Introduce una razón social" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">art_track</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="rut_empresa_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="rut_empresa_register">Rut Empresa</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rut_empresa_register" ErrorMessage="Introduce un rut por favor" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Formato de rut no valido. Ejemplo: 11222333-9" ControlToValidate="rut_empresa_register" ValidationExpression="\b\d{1,8}\-[K|k|0-9]" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">edit_location</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="direccion_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="direccion_register">Dirección</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="direccion_register" ErrorMessage="Introduce una dirección" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">location_city</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                <asp:DropDownList id="ciudad_register" runat="server" CssClass="mdl-textfield__input">
                                </asp:DropDownList>
                            <label class="mdl-textfield__label" for="DropDownList1">Cuidad</label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">local_phone</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="fono_register" runat="server" CssClass="mdl-textfield__input">+56</asp:TextBox>
                            <label class="mdl-textfield__label" for="fono_register">Fono</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="fono_register" ErrorMessage="Introduce un fono por favor" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Formato de fono no valido. Ejemplo: +56988888888" ControlToValidate="fono_register" ValidationExpression="^(0056|\+56)?(\d\d\d)-? ?(\d\d)-? ?(\d)-? ?(\d)-? ?(\d\d)$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
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
                                url: "RegisterProveedor.aspx/VerifyCaptcha",
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
        <div class="mdl-card__actions mdl-card--border">
            <asp:Button ID="Button_Register" runat="server" Text="Registrar" OnClick="Button_Register_Click" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Almacen/AdministrarProveedor.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">ADMINISTRAR</asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Almacen/MenuAlmacen.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Volver a Menú Almacén</asp:HyperLink>
            
            <!--<button type="submit" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" id="Button_Register" onclick="Button_Register_Click">
                <b>Registrar</b>
            </button>-->
        </div>
    </div>
</asp:Content>
