<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstanciarTarea.aspx.cs" Inherits="UsuariosRegistrados.Alumno.InstanciarTarea" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tu Página</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Tarea Genérica</h2>
            <label>Usuario:</label>
            <asp:TextBox ID="txtUsuario" runat="server" Enabled="false"></asp:TextBox>
            <br />
            <label>Tarea:</label>
            <asp:TextBox ID="txtTarea" runat="server" Enabled="false"></asp:TextBox>
            <br />
            <label>Código:</label>
            <asp:TextBox ID="txtCodigo" runat="server" Enabled="false"></asp:TextBox>
            <br />
            <label>Horas Estimadas:</label>
            <asp:TextBox ID="txtHorasEstimadas" runat="server" Enabled="false"></asp:TextBox>
            <br />
            <br />
            <label>Horas Reales:</label>
            <asp:TextBox ID="txtHorasReales" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnAgregarHoras" runat="server" Text="Agregar Horas" OnClick="btnAgregarHoras_Click" />
            <br />
            <asp:HyperLink ID="hrefAlumno" NavigateUrl="/Alumno/VerTareasEstudiante.aspx" runat="server" Text="Volver a general" OnClick="hrefAlumno_Click"></asp:HyperLink>
            <br />
            <br />
            <h3>Tareas Registradas</h3>
            <asp:GridView ID="gvTareasRegistradas" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                    <asp:BoundField DataField="Tarea" HeaderText="Tarea" />
                    <asp:BoundField DataField="HorasEstimadas" HeaderText="Horas Estimadas" />
                    <asp:BoundField DataField="HorasReales" HeaderText="Horas Reales" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>