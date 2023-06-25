<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profesor.aspx.cs" Inherits="UsuariosRegistrados.Profesor.Profesor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página del Profesor</title>
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
                    <li><a href="/Profesor/GestionarTareas.aspx">Ver tareas</a></li>
                    <li><asp:LinkButton ID="lnkMostrarContraseña" runat="server" Text="Ver contraseña" OnClick="btnMostrarContraseña_Click"></asp:LinkButton></li>
                </ul>
            </div>
        
            <div class="right-column">
                <h1>Página del Profesor</h1>
                <br />
                <div>
                    <asp:Label ID="lblResultado" runat="server" CssClass="label-sepia" Text=""></asp:Label>
                </div>
                <h2>Seleccionar alumno:</h2>
                <asp:DropDownList ID="ddlAlumnos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAlumnos_SelectedIndexChanged"/>
                <h2>Dedicación de un alumno en cada tarea instanciada</h2>
                <table class="table-sepia">
                    <tr>
                        <th>Código</th>
                        <th>Tarea</th>
                        <th>Horas Reales</th>
                    </tr>
                    <asp:Repeater ID="rptDedicacionTareas" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Codigo") %></td>
                                <td><%# Eval("Tarea") %></td>
                                <td><%# Eval("HorasReales") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Label ID="lblMediaHorasReales" runat="server" Text="Media de horas reales:"></asp:Label>
                <br />
                <h2>Dedicación media empleada en cada una de las asignaturas</h2>
                <table class="table-sepia">
                    <tr>
                        <th>Asignatura</th>
                        <th>Dedicación Media</th>
                    </tr>
                    <asp:Repeater ID="rptDedicacionAsignaturas" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Asignatura") %></td>
                                <td><%# Eval("DedicacionMedia") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
