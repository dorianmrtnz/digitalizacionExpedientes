<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecepcionDocumentosTest.aspx.cs" Inherits="recepciondeDocumentos.publico.RecepcionDocumentosTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 353px;
        }
        .auto-style1 {
            color: #FF0000;
        }
        .auto-style3 {
            height: 25px;
            width: 544px;
        }
        .auto-style4 {
            height: 25px;
            width: 545px;
        }
        .auto-style5 {
            height: 25px;
            width: 546px;
        }
        .auto-style6 {
            height: 25px;
            width: 548px;
        }
        .auto-style7 {
            height: 25px;
            width: 547px;
        }
        .auto-style8 {
            margin-left: 8px;
        }
        .auto-style9 {
            margin-left: 39px;
        }
        .auto-style10 {
            margin-left: 27px;
        }
        .auto-style11 {
            margin-left: 52px;
        }
        .auto-style12 {
            margin-left: 41px;
        }
        </style>
</head>
<body style="height: 101px">
    <form id="form1" runat="server">
        <div class="auto-style4">
            
            Empleado id Documento digiralizado<asp:TextBox ID="txtdocumentodigitalizado_id" runat="server" Width="86px" CssClass="auto-style8" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldocdig" runat="server" CssClass="auto-style1"></asp:Label>
            <br />
            
            
        </div>
        <div class="auto-style5">
            
            FK documento Id<asp:TextBox ID="txtFKdocumentoID" runat="server" Width="87px" CssClass="auto-style9" Visible="False"></asp:TextBox>
            <asp:Label ID="lblNomarchivo" runat="server" CssClass="auto-style1"></asp:Label>
            <br />
            
            
        </div>
        <div class="auto-style3">
            
            Nombre archivo<asp:TextBox ID="txtNombreArchivo" runat="server"></asp:TextBox>
            <asp:Label ID="lblDescripcion" runat="server" CssClass="auto-style1"></asp:Label>
            <br />
            
            
        </div>
        <div style="height: 25px; width: 276px;">
            
            Fk_EmpleadoID<asp:TextBox ID="txtFKEmpleadoID" runat="server" Width="87px" CssClass="auto-style10"></asp:TextBox>
            <br />
            
            
        </div>
        <div class="auto-style5">
            
            FkUsuarioIDCaptura<asp:TextBox ID="txtFKUsuarioIdCaptura" runat="server" Width="86px" CssClass="auto-style11"></asp:TextBox>
            <br />
            
            
        </div>
        <div class="auto-style6">
            
            Fecha Captura<asp:TextBox ID="txtFechaCaptura" runat="server" style="margin-left: 15px" Width="85px" Visible="False"></asp:TextBox>
            <br />
            
            
        </div>
        <div class="auto-style7">
            
            Fk Usuario id modifica<asp:TextBox ID="txtFKUsuarioIDModifica" runat="server" Width="87px" Visible="True" CssClass="auto-style12"></asp:TextBox>
            <br />
            
            
        </div>
            
            <br />
            
            
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Guardar" />
        <asp:Button ID="Button2" runat="server" Text="Button" />
            
            
        s</form>
</body>
</html>
