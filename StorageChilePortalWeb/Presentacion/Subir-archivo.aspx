<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Subir-archivo.aspx.cs" Inherits="Presentacion.SubirArchivo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Subir archivo</h1>
        </div>
        <div class="mdl-card__supporting-text">
            <ul class="demo-list-control mdl-list">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">person</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="nombre_sa_input" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="nombre_sa_input">Usuario</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ControlToValidate="nombre_sa_input" ErrorMessage="Introduce el nombre de usuario" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegExUsuario" runat="server" ErrorMessage="No se admiten caracteres especiales o nombres muy largos o cortos" ControlToValidate="nombre_sa_input" ValidationExpression="\w{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">art_track</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="rut_sa_input" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="rut_sa_input">Rut</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rut_sa_input" ErrorMessage="Introduce un rut por favor" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Formato de rut no valido. Ejemplo: 11222333-4" ControlToValidate="rut_sa_input" ValidationExpression="\b\d{1,8}\-[K|k|0-9]" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons  mdl-list__item-avatar">folder</i>
                        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                            <asp:TextBox ID="contenedor_sa_inpu" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label" for="contenedor_sa_inpu">Contenedor</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="contenedor_sa_inpu" ErrorMessage="Introduce el nombre de usuario" CssClass="mdl-textfield__error"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="No se admiten caracteres especiales o nombres muy largos o cortos" ControlToValidate="contenedor_sa_inpu" ValidationExpression="\w{4,30}" CssClass="mdl-textfield__error"></asp:RegularExpressionValidator>
                        </span>
                    </span>
                </li>
            </ul>
        </div>
        <style type="text/css">

    #Background
    {
        position: fixed; 
        top: 0px; 
        bottom: 0px; 
        left: 0px; 
        right: 0px; 
        overflow: hidden; 
        padding: 0; 
        margin: 0; 
        background-color: #F0F0F0; 
        filter: alpha(opacity=80); 
        opacity: 0.8; 
        z-index: 100000;
    }
        
    #ProgressS
    {
        position: fixed;
        top: 40%; 
        left: 40%; 
        height:20%; 
        width:20%; 
        z-index: 100001;  
        background-color: #FFFFFF; 
        border:1px solid Gray; 
        background-image: url('img/loading2.gif'); 
        background-repeat: no-repeat; 
        background-position:center;
    }

    </style>
        <div>
            <div class="text-escoge">
                <label>Escoge el archivo a subir:</label>
            </div>
            <div class="mdl-card__actions mdl-card--border">
                <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
                
                <asp:AsyncFileUpload ID="FileUpload1" runat="server" ClientIDMode="AutoID" PersistFile="true" CssClass="mdl-button mdl-js-button mdl-button--primary"/>
                <br />
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Button_Upload_Click" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <i class="material-icons">publish</i>
                Subir
                </asp:LinkButton>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"  DynamicLayout="true" >
    <ProgressTemplate>
        <div id="Background"></div>
        <div id="ProgressS">
            <h6> <p style="text-align:center"> <b>Procesando Datos, Espere por favor... <br /></b> </p> </h6>
        </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
        
            </div>
        </div>
            <div id="contProgress" runat="server" style="text-align:center;align-items:center">
            <div class="progress progress-striped">
                      <div id="progress1" runat="server" class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 40%">
                        <span>40% Complete </span>
                      </div>
                       
                    </div>
            </div>
        
                
        
        
        </div>
</asp:Content>

