<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="UsuariosRegistrados.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página de Inicio</title>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="SqlDataSourceLogin" runat="server" 
            ConnectionString="Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" 
            SelectCommand="SELECT Tipo FROM Usuario WHERE email = @Email AND pass = @Password">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtEmail" Name="Email" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtContraseña" Name="Password" 
                    PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        
        <div>
            <h1>Bienvenido a la Página de Inicio</h1>

            <div>
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </div>

            <div>
                <label for="txtContraseña">Contraseña:</label>
                <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Label runat="server" ID="lblMensaje"></asp:Label>
            </div>
            <div>
                <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar Sesión" OnClick="btnIniciarSesion_Click" />
            </div>
            <!--
            <div>
                <asp:HyperLink ID="lnkRegistro" runat="server" Text="Registrarse" NavigateUrl="Registro.aspx"></asp:HyperLink>
                <asp:HyperLink ID="lnkModificarContraseña" runat="server" Text="Contraseña olvidada" NavigateUrl="ContrasenaOlvidada.aspx"></asp:HyperLink>
            </div>
            -->
        </div>
        
    </form>
</body>
</html>