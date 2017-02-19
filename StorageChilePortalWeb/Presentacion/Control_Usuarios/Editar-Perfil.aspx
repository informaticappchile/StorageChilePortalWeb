<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Editar-Perfil.aspx.cs" Inherits="Presentacion.Formulario_web1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">PERFIL</h1>
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
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Edad:</label>
                        <span id="Editar_Perfil_Edad_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Edad" runat="server" ReadOnly="True" TextMode="Number" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Perfil_Edad"></label>
                            <asp:RangeValidator ID="RangoEdadValidator" runat="server" ErrorMessage="Edad incorrecta" MinimumValue="0" MaximumValue="100" Type="Integer" ControlToValidate="Editar_Perfil_Edad" CssClass="mdl-textfield__error"></asp:RangeValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content" id="Editar_Perfil_Genero">
                        <label class="etiqueta-editar-perfil">Género:</label>
                        <label id="Editar_Perfil_Hombre_Label" class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="ContentPlaceHolder1_Editar_Perfil_Hombre" runat="server">
                            <asp:RadioButton ID="Editar_Perfil_Hombre" Enabled="False" runat="server" GroupName="genero" />
                            <span class="mdl-radio__label">Hombre</span>
                        </label>
                        <label id="Editar_Perfil_Mujer_Label" class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="ContentPlaceHolder1_Editar_Perfil_Mujer" runat="server">
                            <asp:RadioButton ID="Editar_Perfil_Mujer" Enabled="False" runat="server" GroupName="genero" />
                            <span class="mdl-radio__label">Mujer</span>
                        </label>
                        <label id="Editar_Perfil_NoMostrar_Label" class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="ContentPlaceHolder1_Editar_Perfil_NoMostrar" runat="server">
                            <asp:RadioButton ID="Editar_Perfil_NoMostrar" Enabled="False" runat="server" GroupName="genero" />
                            <span class="mdl-radio__label">No mostrar</span>
                        </label>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Localidad:</label>
                        <span id="Editar_Perfil_Localidad_Span" class="mdl-textfield mdl-js-textfield" runat="server">
                            <asp:TextBox ID="Editar_Perfil_Localidad" runat="server" ReadOnly="True" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="ContentPlaceHolder1_Editar_Pefil_Localidad"></label>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <label class="etiqueta-editar-perfil">Visibilidad de perfil:</label>
                        <asp:Label ID="Editar_Perfil_Visibilidad" AssociatedControlID="Editar_Perfil_Visibilidad_Switch" runat="server" CssClass="mdl-switch mdl-js-switch mdl-js-ripple-effect">
                            <asp:CheckBox ID="Editar_Perfil_Visibilidad_Switch" ClientIDMode="Static" Enabled="false" runat="server" OnClick="onClickEvent_VisibilitySwitch()"/>
                            <asp:Label ID="Editar_Perfil_Visibilidad_Label" ClientIDMode="Static" runat="server" Text="Privado" CssClass="mdl-switch__label"></asp:Label>
                        </asp:Label>
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

    <!-- Pequeño script para cambiar el texto de la etiqueta de la visibilidad del perfil ya que desde ASP es imposible -->
    <script>
        function onClickEvent_VisibilitySwitch() {
            var switch_status = document.getElementById("Editar_Perfil_Visibilidad_Switch");
            var switch_label = document.getElementById("Editar_Perfil_Visibilidad_Label");
            if (switch_status.checked) switch_label.innerHTML = "Público";
            else switch_label.innerHTML = "Privado";
        }
    </script>
</asp:Content>
