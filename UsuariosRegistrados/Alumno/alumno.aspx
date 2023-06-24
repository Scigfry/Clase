<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerPass.aspx.cs" Inherits="UsuariosRegistrados.Alumno.VerPass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página del Alumno</title>
    <style>
        .container {
            display: flex;
        }
    
        .left-column {
            width: 20%;
            background-color: #f2f2f2;
            padding: 10px;
        }
    
        .right-column {
            width: 80%;
            background-color: #f5e4c5;
            padding: 10px;
        }
    
        .table-sepia {
            border-collapse: collapse;
            width: 100%;
            background-color: #f9eedc;
        }
    
        .table-sepia th,
        .table-sepia td {
            padding: 8px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
    
        .label-sepia {
            color: #704214;
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="left-column">
                <h2>Columna Izquierda</h2>
                <ul>
                    <li><a href="/Alumno/VerTareasEstudiante.aspx">Ver tareas</a></li>
                    <li><asp:LinkButton ID="lnkMostrarContraseña" runat="server" Text="Ver contraseña" OnClick="btnMostrarContraseña_Click"></asp:LinkButton></li>
                </ul>
            </div>
    
            <div class="right-column">
                <h1>Página del Alumno</h1>
                <h2>Tareas Registradas</h2>
                <asp:Repeater ID="rptAsignaturas" runat="server" OnItemDataBound="rptAsignaturas_ItemDataBound">
                    <ItemTemplate>
                        <h3><%# Eval("nombre") %></h3>
                        <table class="table-sepia">
                            <tr>
                                <th>Código</th>
                                <th>Tarea</th>
                                <th>Horas Estimadas</th>
                                <th>Horas Reales</th>
                            </tr>
                            <asp:Repeater ID="rptTareas" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("Codigo") %></td>
                                        <td><%# Eval("Tarea") %></td>
                                        <td><%# Eval("HorasEstimadas") %></td>
                                        <td><%# Eval("HorasReales") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                <br />
                <asp:Label ID="lblResultado" runat="server" CssClass="label-sepia" Text=""></asp:Label>
                <br />
            </div>
        </div>
    </form>
</body>
</html>
