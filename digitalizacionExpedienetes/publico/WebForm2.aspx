<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="recepciondeDocumentos.publico.WebForm2" %>


<%--<!DOCTYPE html>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>--%>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>--%>
    <link href="../cssb/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/formato.css" />
    <link rel="stylesheet" type="text/css" href="../css/utilerias.css" />

    <style type="text/css">
        .normalizar {
            max-width: 60rem;
            margin-left: auto;
            margin-right: auto;
        }
    </style>
    <style type="text/css">
        #GridView1 tr.rowHover:hover {
            background-color: aquamarine;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        .auto-style4 {
            width: 257px;
            height: 53px;
        }

        .auto-style5 {
            height: 23px;
            width: 33px;
        }

        .auto-style7 {
            width: 241px;
            height: 23px;
        }

        .auto-style8 {
            width: 241px;
        }

        .auto-style9 {
            left: 0px;
            top: 50px;
            width: 1110px;
            margin-left: 2px;
        }

        .auto-style10 {
            height: 376px;
        }

        .auto-style11 {
            width: 1233px;
        }

        .auto-style13 {
            position: relative;
            left: 148px;
            top: 2px;
        }
    </style>
    <style type="text/css">
        .contenedor_documentos {
            display: flex;
            flex-direction: row;
            justify-content: flex-start;
        }
    </style>
    <script src="../js/jquery-3.4.1.min.js"></script>


    <%--</head>--%>

    <%--<body >--%>


    <asp:ScriptManagerProxy runat="server">
        <Scripts>
            <%--<asp:ScriptReference Path="../js/jquery-3.4.1.min.js"></asp:ScriptReference>
            <asp:ScriptReference Path="../js/utilerias.js"></asp:ScriptReference>
            <asp:ScriptReference Path="../jsb/bootstrap.min.js"></asp:ScriptReference>--%>
        </Scripts>
    </asp:ScriptManagerProxy>

    <br />
    <br />

    <div class="text-center">
        <br />
        <div class="col-12 rounded" style="width: 900px; padding-left: 300px">

            <!-- Formulario de busqueda -->
            <div id="Acceso" class="modal-content">
                <div class="">

                    <div class="text-center mdb-color" style="margin-top: 50px">
                        <h3 class="title" style="color: white">CONSULTE SUS DOCUMENTOS </h3>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <form class="col-12" style="position: center;">
                                <h4 class="">INGRESA TU DATOS</h4>

                                <div class="form-group w-75" style="margin: auto; margin-bottom: 10px">
                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" placeholder="Nombres"></asp:TextBox>
                                </div>

                                <div class="form-group w-75" style="margin: auto; margin-bottom: 10px">
                                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" placeholder="Apellidos"></asp:TextBox>
                                </div>

                                <div class="form-group w-75" style="margin: auto; margin-bottom: 10px">
                                    <asp:TextBox ID="txtRFC" runat="server" CssClass="form-control" placeholder="RFC"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:Button ID="btn_Buscar" runat="server" CssClass="btn btn-default dropdown-toggle"
                                        Text="Buscar" OnClick="BtnEnviar_Click" Height="40px" Width="80px" OnClientClick="ocultaracceso()" />
                                </div>


                            </form>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_Buscar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <!-- Formulario de nueva consulta -->
            <div class="float" style="position: relative; justify-content: space-around;">

                <div id="divbtnNuevaConsulta" style="display: none; position: absolute; padding-right: 500px; padding-top: 1rem; padding-left: 2rem" class="col-lg-12 coll-md-12">

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                        <ContentTemplate>
                            <asp:Button ID="btnNuevaConsulta" runat="server" Text="Nueva Consulta" CssClass="btn btn-default dropdown-toggle" OnClick="btnNuevaConsulta_Click" OnClientClick="mostraracceso()" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_Buscar" EventName="Click" />
                        </Triggers>

                    </asp:UpdatePanel>
                </div>
                 
                <br />

                <div class="d-flex">
                    <div class="flex-row" id="GridMostrar" style="position: relative; padding-left: 10px; margin-top: 4rem">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" Style="align-content: center;" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ClientIDMode="Static" RowStyle-CssClass="rowHover" CellPadding="4" ForeColor="#333333" GridLines="None" AutoPostBack="True" DataKeyNames="empleado_id, nombres, apellidos, CURP, RFC, correo, telefono, domicilio, imagen_perfil" EmptyDataText="No se han encontrado registros." Width="617px">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" CssClass="rowHover" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>

                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:recepcionDocumentos %>" SelectCommand="SELECT [imagen_perfil], [empleado_id], [nombres], [apellidos], [CURP], [RFC], [correo], [telefono], [domicilio] FROM [Empleados]"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:recepcionDocumentos %>" SelectCommand="SELECT * FROM [Empleados]"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:recepcionDocumentos %>" SelectCommand="SELECT * FROM [Empleados]"></asp:SqlDataSource>

                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Buscar" EventName="Click" />
                            </Triggers>

                        </asp:UpdatePanel>
                    </div>

                    <!-- Informacion del empleado -->
                    <asp:UpdatePanel ID="PoblarLabels" runat="server" style="padding-top: 3rem; margin-left: 20px">
                        <ContentTemplate>
                            <div id="empleadoInfo" class="border" runat="server" style="display: none; position: relative; max-width: 500px; height: 300px; margin-top: 1rem">

                                <div class="d-flex flex-row bd-highlight mb-3">

                                    <div class="p-2 bd-highlight">
                                        <div class="row">
                                            <div class="col-md-4 col-md-offset-4">

                                                <asp:Image ID="imgPreview" Width="200px" runat="server" ImageUrl="../EmpleadosFotos/empleadopic.png" />
                                                <br />
                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" Width="200px" />

                                                <asp:Button ID="btnSubir" runat="server" Text="Guardar" CssClass="btn btn-default dropdown-toggle" />
                                            </div>
                                        </div>
                                    </div>

                                    <div id="tabla1" class="" style="padding-bottom: 10px">

                                        <div class="">
                                            <br />
                                            <div class="auto-style8 d-flex justify-content-start">
                                                Id Empleado:
                                            <asp:Label ID="lbl_idempleado" runat="server" ForeColor="#666699" Style="margin-left: 0.4rem"></asp:Label>
                                            </div>

                                            <div />
                                            <div>
                                                <div class="auto-style8 d-flex justify-content-start">
                                                    Nombre:
                                            <asp:Label ID="lbl_nombre" runat="server" ForeColor="#666699" Style="margin-left: 0.4rem"></asp:Label>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="auto-style8 d-flex justify-content-start">
                                                    Apellidos:
                                            <asp:Label ID="lbl_apellidos" runat="server" EnableTheming="True" ForeColor="#666699" Style="margin-left: 0.4rem"></asp:Label>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="auto-style8 d-flex justify-content-start">
                                                    CURP:
                                            <asp:Label ID="lbl_CURP" runat="server" ForeColor="#666699" Style="margin-left: 0.4rem"></asp:Label>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="auto-style8 d-flex justify-content-start">
                                                    RFC: 
                                            <asp:Label ID="lbl_RFC" runat="server" ForeColor="#666699" Style="margin-left: 0.4rem"></asp:Label>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="auto-style8 d-flex justify-content-start">
                                                    Correo: 
                                            <asp:Label ID="lbl_Correo" runat="server" ForeColor="#666699" Style="margin-left: 0.4rem"></asp:Label>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="auto-style8 d-flex justify-content-start">
                                                    Telefono: 
                                            <asp:Label ID="lbl_telefono" runat="server" ForeColor="#666699" Style="margin-left: 0.4rem"></asp:Label>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="auto-style8 d-flex justify-content-start">
                                                    Domicilio: 
                                            <asp:Label ID="lbl_domicilio" runat="server" ForeColor="#666699" Style="margin-left: 0.4rem"></asp:Label>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="flex-column">
                                <div>
                                    <h3>Documentos</h3>
                                </div>

                                <div id="Div1" class="d-flex border" runat="server" style="display: block; position: center; max-width: 1600px;">

                                    <div>
                                        <asp:DropDownList ID="ddlDocumento" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <%--<div class="dropdown" style="margin-left:1rem">
                                        <button class="btn btn-default dropdown-toggle" type="button" id="menu1" data-toggle="dropdown">
                                            Tipo
                                        <span class="caret"></span>
                                        </button>
                                        <ul class="dropdown-menu" role="menu" aria-labelledby="menu1">
                                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Identificacion</a></li>
                                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Comprobante De Domicilio</a></li>
                                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Curp</a></li>
                                            <li role="presentation" class="divider"></li>

                                        </ul>
                                    </div>--%>

                                    <div class="flex-row" style="margin-left: 1rem">
                                        <asp:FileUpload ID="FileUpload2" runat="server" class="" Text="Archivo" />
                                    </div>

                                    <div class="flex-row" style="margin-left: 1rem">
                                        <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="../images/upload.png" Height="35px" Width="45px" CssClass="btn btn-default dropdown-toggle" />
                                    </div>

                                </div>

                            </div>
                        </ContentTemplate>

                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnSubir" EventName="Click" />
                        </Triggers>

                    </asp:UpdatePanel>

                </div>
            </div>

        </div>
    </div>



    <script type="text/javascript">

        function ocultaracceso() {

            var txtnom = $('#<%=txtNombres.ClientID%>').val();
            var txtapell = $('#<%=txtApellidos.ClientID%>').val();
            var txtrfc = $('#<%=txtRFC.ClientID%>').val();

            if (txtnom == "" && txtapell == "" && txtrfc == "") {
                //$('#Acceso').show();

            } else {
                $('#Acceso').fadeOut();

                $('#divbtnNuevaConsulta').fadeIn();
            }

        }
    </script>

    <script type="text/javascript">
        function mostrar_empleadoInfo() {
            $('#empleadoInfo').fadeIn();
            $('#GridMostrar').fadeOut();
        }
    </script>

</asp:Content>

