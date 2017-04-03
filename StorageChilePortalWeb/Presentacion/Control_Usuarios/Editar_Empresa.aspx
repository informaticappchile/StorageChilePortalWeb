<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="Editar_Empresa.aspx.cs" Inherits="Presentacion.Editar_Empresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Editar datos de Empresa</h1>
        </div>
        <div class="mdl-card__supporting-text">
            <ul class="demo-list-control mdl-list">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Nombre Empresa:</label>
                        <span id="Editar_Nombre_Empresa_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Nombre_Empresa" runat="server"  ReadOnly="false" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Usuario"></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ControlToValidate="Editar_Nombre_Empresa" ErrorMessage="Introduce el nombre de usuario" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Rut Empresa:</label>
                        <span id="Editar_Rut_Empresa_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Rut_Empresa" runat="server"  ReadOnly="true" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Nombre"></label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Email:</label>
                        <span id="Editar_Email_Empresa_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Email_Empresa" runat="server" ReadOnly="false" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Correo"></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="Editar_Email_Empresa" ErrorMessage="Introduce el email" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="Editar_Email_Empresa" ErrorMessage="Esto no es un email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Fecha Registro:</label>
                        <span id="Editar_Fecha_Resgistro_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Fecha_Resgistro" runat="server"  ReadOnly="true" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Nombre"></label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Servicio Almacen:</label>
                        <asp:Label ID="Editar_Empresa_ServicioAlmacen" AssociatedControlID="Editar_Empresa_ServicioAlmacen_Switch" runat="server" CssClass="mdl-switch mdl-js-switch mdl-js-ripple-effect">
                            <asp:CheckBox ID="Editar_Empresa_ServicioAlmacen_Switch" ClientIDMode="Static" Enabled="true" runat="server" OnClick="onClickEvent_ServicioAlmacen_Switch()"/>
                            <asp:Label ID="Editar_Empresa_ServicioAlmacen_Label" ClientIDMode="Static" runat="server" Text="No Activado" CssClass="mdl-switch__label" ></asp:Label>
                        </asp:Label>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Servicio Bodega:</label>
                        <asp:Label ID="Editar_Empresa_ServicioBodega" AssociatedControlID="Editar_Empresa_ServicioBodega_Switch" runat="server" CssClass="mdl-switch mdl-js-switch mdl-js-ripple-effect">
                            <asp:CheckBox ID="Editar_Empresa_ServicioBodega_Switch" ClientIDMode="Static" Enabled="true" runat="server" OnClick="onClickEvent_ServicioBodega_Switch()"/>
                            <asp:Label ID="Editar_Empresa_ServicioBodega_Label" ClientIDMode="Static" runat="server" Text="No Activado" CssClass="mdl-switch__label" ></asp:Label>
                        </asp:Label>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Servicio Digitalización:</label>
                        <asp:Label ID="Editar_Empresa_ServicioDigitalizacion" AssociatedControlID="Editar_Empresa_ServicioDigitalizacion_Switch" runat="server" CssClass="mdl-switch mdl-js-switch mdl-js-ripple-effect">
                            <asp:CheckBox ID="Editar_Empresa_ServicioDigitalizacion_Switch" ClientIDMode="Static" Enabled="true" runat="server" OnClick="onClickEvent_ServicioDigitalizacion_Switch()"/>
                            <asp:Label ID="Editar_Empresa_ServicioDigitalizacion_Label" ClientIDMode="Static" runat="server" Text="No Activado" CssClass="mdl-switch__label" ></asp:Label>
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
        </div>
        <div class="mdl-card__actions mdl-card--border">
            <asp:LinkButton ID="Editar_Perfil_Guardar" runat="server" visible="true" OnClientClick="return confirm('¿Está seguro que desea editar realmente a esta Empresa?');" OnClick="Editar_Perfil_Guardar_Click" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <i class="material-icons">save</i>
                Guardar Cambios
            </asp:LinkButton>
            <asp:TextBox ID="Editar_Perfil_ID" runat="server" visible="true"></asp:TextBox>
        </div>
    </div>
    <script>
        function onClickEvent_ServicioAlmacen_Switch() {
            var switch_status = document.getElementById("Editar_Empresa_ServicioAlmacen_Switch");
            var switch_label = document.getElementById("Editar_Empresa_ServicioAlmacen_Label");
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
            var switch_status = document.getElementById("Editar_Empresa_ServicioBodega_Switch");
            var switch_digi = document.getElementById("Editar_Empresa_ServicioDigitalizacion_Switch");
            var switch_label = document.getElementById("Editar_Empresa_ServicioBodega_Label");
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
            var switch_status = document.getElementById("Editar_Empresa_ServicioDigitalizacion_Switch");
            var switch_label = document.getElementById("Editar_Empresa_ServicioDigitalizacion_Label");
            if (switch_status.checked) {
                switch_label.innerHTML = "Activado";
            }
            else
            {
                switch_label.innerHTML = "No Activado";
            }
        }
    </script>
</asp:Content>
