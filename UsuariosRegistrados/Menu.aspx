<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="UsuariosRegistrados.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pagina del menú vacía</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HyperLink ID="lnkInicio" runat="server" Text="Pagina de inicio" NavigateUrl="Inicio.aspx"></asp:HyperLink>
        </div>

        <div>
            <asp:HyperLink ID="link" runat="server" Text="Cambiar Contraseña" NavigateUrl="CambiarContrasena.aspx"></asp:HyperLink>
        </div>
    </form>
</body>
</html>
