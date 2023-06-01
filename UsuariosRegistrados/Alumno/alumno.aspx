<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="alumno.aspx.cs" Inherits="UsuariosRegistrados.Alumno.alumno" %>


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
        <div class="container">
            <div class="left-column">
                <h2>Columna Izquierda</h2>
                <ul>
                    <li><a href="/Alumno/VerTareasEstudiante.aspx">Ver tareas</a></li>
                    <!--<li><a href="#">Hipervínculo 2</a></li>
                    <li><a href="#">Hipervínculo 3</a></li>-->
                </ul>
            </div>
        
            <div class="right-column">
                <h1>Página del Alumno</h1>
            </div>
        </div>
    </body>
</html>