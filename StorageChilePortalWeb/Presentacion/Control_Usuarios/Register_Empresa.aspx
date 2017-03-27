<%@ Page Title="Register" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Register_Empresa.aspx.cs" Inherits="Presentacion.Register_Empresa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Registrar Nuevo Usuario</h1>
        </div> 
        <div class="mdl-card__supporting-text">
            <asp:Label ID="UsernameExistsError_Register" runat="server" Text="Nombre de empresa ya registrado" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
            <asp:Label ID="EmailExistsError_Register" runat="server" Text="Correo electrónico ya registrado" Visible="false" CssClass="mdl-card__subtitle-text mdl-color-text--red"></asp:Label>
            <ul class="demo-list-control mdl-list">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">domain</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="nombre_empresa_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="nombre_empresa_register">Nombre Empresa</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ControlToValidate="nombre_empresa_register" ErrorMessage="Introduce el nombre de usuario" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegExUsuario" runat="server" ErrorMessage="No se admiten caracteres especiales o nombres muy largos o cortos" ControlToValidate="nombre_empresa_register" ValidationExpression="\w{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">art_track</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="rut_empresa_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="rut_empresa_register">Rut</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rut_empresa_register" ErrorMessage="Introduce un rut por favor" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Formato de rut no valido. Ejemplo: 11222333-9" ControlToValidate="rut_empresa_register" ValidationExpression="\b\d{1,8}\-[K|k|0-9]" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">email</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="correo_empresa_register" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <!--<input class="mdl-textfield__input" type="password" id="userpass-login-input">-->
                            <label class="mdl-textfield__label" for="correo_empresa_register">Email</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorcorreo" runat="server" ControlToValidate="correo_empresa_register" ErrorMessage="Introduce el email" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="correo_empresa_register" ErrorMessage="Esto no es un email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Servicio Almacen:</label>
                        <asp:Label ID="Registro_Empresa_ServicioAlmacen" AssociatedControlID="Registro_Empresa_ServicioAlmacen_Switch" runat="server" CssClass="mdl-switch mdl-js-switch mdl-js-ripple-effect">
                            <asp:CheckBox ID="Registro_Empresa_ServicioAlmacen_Switch" ClientIDMode="Static" Enabled="true" runat="server" OnClick="onClickEvent_ServicioAlmacen_Switch()"/>
                            <asp:Label ID="Registro_Empresa_ServicioAlmacen_Label" ClientIDMode="Static" runat="server" Text="No Activado" CssClass="mdl-switch__label" ></asp:Label>
                        </asp:Label>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Servicio Bodega:</label>
                        <asp:Label ID="Registro_Empresa_ServicioBodega" AssociatedControlID="Registro_Empresa_ServicioBodega_Switch" runat="server" CssClass="mdl-switch mdl-js-switch mdl-js-ripple-effect">
                            <asp:CheckBox ID="Registro_Empresa_ServicioBodega_Switch" ClientIDMode="Static" Enabled="true" runat="server" OnClick="onClickEvent_ServicioBodega_Switch()"/>
                            <asp:Label ID="Registro_Empresa_ServicioBodega_Label" ClientIDMode="Static" runat="server" Text="No Activado" CssClass="mdl-switch__label" ></asp:Label>
                        </asp:Label>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Servicio Digitalización:</label>
                        <asp:Label ID="Registro_Empresa_ServicioDigitalizacion" AssociatedControlID="Registro_Empresa_ServicioDigitalizacion_Switch" runat="server" CssClass="mdl-switch mdl-js-switch mdl-js-ripple-effect">
                            <asp:CheckBox ID="Registro_Empresa_ServicioDigitalizacion_Switch" ClientIDMode="Static" Enabled="true" runat="server" OnClick="onClickEvent_ServicioDigitalizacion_Switch()"/>
                            <asp:Label ID="Registro_Empresa_ServicioDigitalizacion_Label" ClientIDMode="Static" runat="server" Text="No Activado" CssClass="mdl-switch__label" ></asp:Label>
                        </asp:Label>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <style type="text/css">
                        .preview-web
                        {
                            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);
                            max-height: 100px;
                            max-width: 100px;
                        }
                    </style>
                    <span class="mdl-list__item-primary-content">
                        <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.3.js"></script>
                        <script type="text/javascript">  
  
                            function showimagepreview(input) {  
  
                                if (input.files && input.files[0]) {  
                                    var reader = new FileReader();  
                                    reader.onload = function (e) {
                                        //$('#id*=img').attr('src', e.target.result);
                                        document.getElementsByTagName("img")[3].setAttribute("src", e.target.result);
                                    }
                                    reader.readAsDataURL(input.files[0]);  
                                }  
                            }  
  
                        </script>
                        <label class="etiqueta-editar-perfil">Logo Empresa:</label>
                        <asp:FileUpload ID="FileUpload1" runat="server" class="mdl-button mdl-js-button mdl-button--primary"
                        onchange="showimagepreview(this)" /> 
                        <asp:Image runat="server" ID="preview" CssClass="preview-web" ImageUrl="~/img/icono-mb.png"/>
                        <br />
                        <asp:RegularExpressionValidator ID="uplValidator" runat="server" ControlToValidate="FileUpload1"
                        ErrorMessage="Solo se permite formato .png" 
                        ValidationExpression="(.+\.([Pp][Nn][Gg]))"></asp:RegularExpressionValidator>
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
                                url: "Register.aspx/VerifyCaptcha",
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
        <!-- Pequeño script para cambiar el texto de la etiqueta de la visibilidad del perfil ya que desde ASP es imposible -->
    <script>
        function onClickEvent_ServicioAlmacen_Switch() {
            var switch_status = document.getElementById("Registro_Empresa_ServicioAlmacen_Switch");
            var switch_label = document.getElementById("Registro_Empresa_ServicioAlmacen_Label");
            if (switch_status.checked)
            {
                switch_label.innerHTML = "Activado";
            }
            else
            {
                switch_label.innerHTML = "No Activado";
            }
        }
        function onClickEvent_ServicioBodega_Switch() {
            var switch_status = document.getElementById("Registro_Empresa_ServicioBodega_Switch");
            var switch_digi = document.getElementById("Registro_Empresa_ServicioDigitalizacion_Switch");
            var switch_label = document.getElementById("Registro_Empresa_ServicioBodega_Label");
            if (switch_status.checked) {
                switch_label.innerHTML = "Activado";
                //switch_digi.click;
            }
            else
            {
                switch_label.innerHTML = "No Activado";
            }
        }
        function onClickEvent_ServicioDigitalizacion_Switch() {
            var switch_status = document.getElementById("Registro_Empresa_ServicioDigitalizacion_Switch");
            var switch_label = document.getElementById("Registro_Empresa_ServicioDigitalizacion_Label");
            if (switch_status.checked) {
                switch_label.innerHTML = "Activado";
            }
            else
            {
                switch_label.innerHTML = "No Activado";
            }
        }
    </script>
        <div class="mdl-card__actions mdl-card--border">
            <asp:Button ID="Button_Register" runat="server" Text="Registrar" OnClick="Button_Register_Click" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" />
            <!--<button type="submit" class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" id="Button_Register" onclick="Button_Register_Click">
                <b>Registrar</b>
            </button>-->
        </div>
    </div>
</asp:Content>
