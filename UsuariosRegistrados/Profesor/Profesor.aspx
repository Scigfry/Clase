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
        <div class="container">
            <div class="left-column">
                <h2>Columna Izquierda</h2>
                <ul>
                    <li><a href="/Profesor/GestionarTareas.aspx">Ver tareas</a></li>
                    <!--<li><a href="#">Hipervínculo 2</a></li>
                    <li><a href="#">Hipervínculo 3</a></li>-->
                </ul>
            </div>
        
            <div class="right-column">
                <h1>Página del Profesor</h1>
            
                <div>
                    <label class="label-sepia">Lorem Ipsum</label>
                    <table class="table-sepia">
                        <thead>
                            <tr>
                                <th>Nombre</th>
                                <th>Apellido</th>
                                <th>Edad</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Alumno 1</td>
                                <td>Apellido 1</td>
                                <td>20</td>
                            </tr>
                            <tr>
                                <td>Alumno 2</td>
                                <td>Apellido 2</td>
                                <td>22</td>
                            </tr>
                            <tr>
                                <td>Alumno 3</td>
                                <td>Apellido 3</td>
                                <td>19</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            
                <div>
                    <label class="label-sepia">Lorem Ipsum</label>
                    <p>Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum.</p>
                </div>
            </div>
        </div>
    </body>
</html>