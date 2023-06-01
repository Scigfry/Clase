<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertarTarea.aspx.cs" Inherits="UsuariosRegistrados.Profesor.InsertarTarea" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Insertar Tarea</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Insertar Tarea</h2>
            
            <div>
                <label for="ddlAsignaturas">Asignatura:</label>
                <asp:DropDownList ID="ddlAsignaturas" runat="server" DataSourceID="SqlDataSourceAsignaturas"
                    DataTextField="nombre" DataValueField="codigo">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceAsignaturas" runat="server" ConnectionString="Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    SelectCommand="SELECT codigo, nombre FROM Asignatura WHERE codigo IN (SELECT codigoAsig FROM GrupoClase WHERE codigo IN (SELECT codigoGrupo FROM ProfesorGrupo WHERE email = @Email))">
                    <SelectParameters>
                        <asp:SessionParameter Name="Email" SessionField="email" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            
            <div>
                <label for="txtCodigo">Código:</label>
                <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
            </div>

            <div>
                <label for="txtDescripcion">Descripción:</label>
                <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
            </div>
            
            <div>
                <label for="txtHorasEstimadas">Horas Estimadas:</label>
                <asp:TextBox ID="txtHorasEstimadas" runat="server"></asp:TextBox>
            </div>
            
            <div>
                <label for="txtTipoTarea">Tipo de Tarea:</label>
                <asp:TextBox ID="txtTipoTarea" runat="server"></asp:TextBox>
            </div>
            
            <div>
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" OnClick="btnInsertar_Click" />
                <asp:HyperLink ID="hrefTareasProfesor" runat="server" NavigateUrl="GestionarTareas.aspx">Ir a Tareas del Profesor</asp:HyperLink>
            </div>

            <div>
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </div>

            <div>
                <asp:Label ID="lblFeedback" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>