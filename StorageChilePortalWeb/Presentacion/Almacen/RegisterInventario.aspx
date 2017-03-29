<%@ Page Title="Register" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RegisterInventario.aspx.cs" Inherits="Presentacion.RegisterInventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Registrar Nuevo Usuario</h1>
        </div>
        <div class="mdl-card__supporting-text">
            <asp:Label ID="UsernameExistsError_Register" runat="server" Text="Nombre de usuario ya registrado" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
            <asp:Label ID="EmailExistsError_Register" runat="server" Text="Correo electrónico ya registrado" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
            <ul class="demo-list-control mdl-list">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">fingerprint</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="codigo_producto_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="codigo_producto_register">Código Producto</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ControlToValidate="codigo_producto_register" ErrorMessage="Introduce el nombre de usuario" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegExUsuario" runat="server" ErrorMessage="No se admiten caracteres especiales o nombres muy largos o cortos" ControlToValidate="codigo_producto_register" ValidationExpression="\w{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">group_work</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                <asp:DropDownList id="grupo_register" runat="server" CssClass="mdl-textfield__input">
                                </asp:DropDownList>
                            <label class="mdl-textfield__label" for="grupo_register">Grupo</label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">description</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="descripcion_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="descripcion_register">Decripción</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="descripcion_register" ErrorMessage="Introduce el nombre de usuario" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="No se admiten caracteres especiales o nombres muy largos o cortos" ControlToValidate="descripcion_register" ValidationExpression="\w{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">work</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                <asp:DropDownList id="proveedor_register" runat="server" CssClass="mdl-textfield__input">
                                </asp:DropDownList>
                            <label class="mdl-textfield__label" for="proveedor_register">Proveedor de Reparto</label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">shopping_basket</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="cant_min_stock_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="cant_min_stock_register">Cantidad Mínima de Stock</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cant_min_stock_register" ErrorMessage="Introduce un número por favor" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Formato de numero no es valido. Ejemplo: 12 (Solo números positivos)" ControlToValidate="cant_min_stock_register" ValidationExpression="^\d+$" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">av_timer</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                                <asp:DropDownList id="unidad_register" runat="server" CssClass="mdl-textfield__input">
                                </asp:DropDownList>
                            <label class="mdl-textfield__label" for="unidad_register">Unidad de Medida</label>
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
                                url: "RegisterInventario.aspx/VerifyCaptcha",
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
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Almacen/AdministrarProducto.aspx" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">ADMINISTRAR</asp:HyperLink>
            <!--<button type="submit" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" id="Button_Register" onclick="Button_Register_Click">
                <b>Registrar</b>
            </button>-->
        </div>
    </div>
</asp:Content>
