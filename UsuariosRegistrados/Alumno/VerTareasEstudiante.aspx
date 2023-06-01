<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerTareasEstudiante.aspx.cs" Inherits="UsuariosRegistrados.Alumno.VerTareasEstudiante" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tu Página</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Ver tareas de estudiante</h2>
            <asp:DropDownList ID="ddlAsignaturas" runat="server" DataTextField="nombre" DataValueField="codigo" OnSelectedIndexChanged="ddlAsignaturas_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <br />
            <br />
            <asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvTareas_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="codigo" HeaderText="Código" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="hEstimadas" HeaderText="Horas Estimadas" />
                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Seleccionar" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
        </div>
        <br />
        <div>
            <asp:HyperLink ID="hrefLogout" runat="server" NavigateUrl="~/Inicio.aspx" OnClick="hrefLogout_OnClick" Text="Logout"></asp:HyperLink>
        </div>
    </form>
</body>
</html>