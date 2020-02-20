<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="recepciondeDocumentos.publico.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-left: 46px;
        }
        .auto-style2 {
            margin-left: 0px;
        }
        .auto-style3 {
            margin-left: 75px;
        }
        .auto-style4 {
            margin-left: 101px;
        }
        .auto-style5 {
            margin-left: 157px;
        }
    </style>
</head>
<body style="height: 20px; width: 482px">
    <form id="form1" runat="server">
        <div>
            documentoDigitalizadoEmpleado_id
            <asp:TextBox ID="txtDocDigEmpleadoID" runat="server"></asp:TextBox>
        </div>
        <div>
            FK Documento ID  <asp:DropDownList ID="ddlDocumeto" runat="server" OnSelectedIndexChanged="ddlDocumeto_SelectedIndexChanged"></asp:DropDownList>
            <asp:TextBox ID="txtFKDocumentoID" runat="server" CssClass="auto-style1"></asp:TextBox>
        </div>
        <div>
            Nombre Archivo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtNombreArchivo" runat="server"></asp:TextBox>
        </div>
        <div>
            FK Empleado ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtFKEmpleadoID" runat="server" CssClass="auto-style2"></asp:TextBox>
        </div>
        <div>
            FK Usuario ID Captura&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtFKUsuarioIDCaptura" runat="server" CssClass="auto-style2" ></asp:TextBox>
        </div>
        <div>
            Fecha Captura&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtFechaCaptura" runat="server" CssClass="auto-style3"></asp:TextBox>
        </div>
        <div>
            FK Usuario ID Modifica<asp:TextBox ID="txtFKUsuarioIDModifica" runat="server" CssClass="auto-style3"></asp:TextBox>
        </div>
        <div>
            Fecha Modificacion<asp:TextBox ID="txtFechaModificacion" runat="server" CssClass="auto-style4"></asp:TextBox>
        </div>
        <div>
            Ultima Act<asp:TextBox ID="txtUltimaAct" runat="server" CssClass="auto-style5"></asp:TextBox>
        </div>
        <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Guardar" />
    </form>
</body>
</html>
