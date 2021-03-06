﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Contactos.aspx.cs" Inherits="Presentacion.Contactos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Contactos</h1>
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
                display:grid;
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
            ImageButton
            {
                border:hidden;
            }
         </style>
        
            <table>
                <tr>
                    <td >
                        <div class="panelIzq">
                            <img data-u="image" src="img/soportetecnico.png" />
                            <br />
                            <br />
                            <button runat="server" class="bttn-unite bttn-md bttn-danger" onServerClick="clickSoporte">Ir a Soporte</button>
                            <br />
                            <div class="box"></div>
                        </div>
                    </td>
                    <td >
                        <div class="panelDer">
                            <img data-u="image" src="img/contactos.png" />
                            <br />
                            <br />
                            <button runat="server" class="bttn-unite bttn-md bttn-danger" onServerClick="clickContacto">Contáctanos</button>
                            <br />
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
          
    </div>
</asp:Content>
