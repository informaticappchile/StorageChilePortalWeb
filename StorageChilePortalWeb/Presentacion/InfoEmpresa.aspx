﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="InfoEmpresa.aspx.cs" Inherits="Presentacion.InfoEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Socios de Storage Chile</h1>
        </div>
        <style>
            img {
                width: 100%;
                height: auto;
            }
         </style>
        <div>
            <img data-u="image" src="img/Foto1.jpg"/>
        </div>
        <div>
            <img data-u="image" src="img/foto2.jpg"/>
        </div>
    </div>
</asp:Content>
