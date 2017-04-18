<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="MenuAlmacen.aspx.cs" Inherits="Presentacion.MenuAlmacen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Menú Almacén Virtual</h1>
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
        
            <div>
            <table>
                <tr>
                    <td >
                        <div class="panelIzq">
                            <img data-u="image" src="img/registroproveedores.png" />
                            <br />
                            <br />
                            <button id="crear_proveedor" runat="server" class="bttn-unite bttn-md bttn-danger" onServerClick="clickCreacionProveedor">Creación Proveedor</button>
                            <br />
                            <div class="box"></div>
                        </div>
                    </td>
                    <td >
                        <div class="panelDer">
                            <img data-u="image" src="img/registroinventario.png" />
                            <br />
                            <br />
                            <button id="crear_producto" runat="server" class="bttn-unite bttn-md bttn-danger" onServerClick="clickCreacionProducto">Creación Producto</button>
                            <br />
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
                </div>
        <div>
            <table>
                <tr>
                    <td >
                        <div class="panelIzq">
                            <img data-u="image" src="img/Movimiento.jpg" />
                            <br />
                            <br />
                            <button runat="server" id="movimiento" class="bttn-unite bttn-md bttn-danger" onServerClick="clickMovimientosInventario">Movimientos Inventario</button>
                            <br />
                            <div class="box"></div>
                        </div>
                    </td>
                    <td >
                        <div class="panelDer">
                            <img data-u="image" src="img/Pagoproveedores.jpg" />
                            <br />
                            <br />
                            <button runat="server" id="pago" class="bttn-unite bttn-md bttn-danger" onServerClick="clickPagoProveedores">Pago Proveedores</button>
                            <br />
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
          
        <div>
            <table>
                <tr>
                    <td >
                        <div class="panelIzq">
                            <img data-u="image" src="img/informeproveedor.png" />
                            <br />
                            <br />
                            <button runat="server" id="informe_proveedor" class="bttn-unite bttn-md bttn-danger" onServerClick="clickInformeProveedor">Informe Proveedor</button>
                            <br />
                            <div class="box"></div>
                        </div>
                    </td>
                    <td >
                        <div class="panelDer">
                            <img data-u="image" src="img/informeinventario.png" />
                            <br />
                            <br />
                            <button runat="server" id="informe_inventario" class="bttn-unite bttn-md bttn-danger" onServerClick="clickInformeInventario">Informe Inventario</button>
                            <br />
                            <div class="box"></div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
