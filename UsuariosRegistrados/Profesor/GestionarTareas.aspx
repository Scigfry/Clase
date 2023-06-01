<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionarTareas.aspx.cs" Inherits="UsuariosRegistrados.Profesor.GestionarTareas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <h2>Gestionar Tareas</h2>
                <div>
                    <asp:DropDownList ID="ddlAsignaturas" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceAsignaturas"
                        DataTextField="nombre" DataValueField="codigo">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceAsignaturas" runat="server" ConnectionString="Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                        SelectCommand="SELECT codigo, nombre FROM Asignatura WHERE codigo IN (SELECT codigoAsig FROM GrupoClase WHERE codigo IN (SELECT codigoGrupo FROM ProfesorGrupo WHERE email = @Email))">
                        <SelectParameters>
                            <asp:SessionParameter Name="Email" SessionField="email" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
                <br />
                <div>
                    <asp:GridView ID="GridViewTareas" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceTareas"
                        AllowSorting="True" OnRowUpdating="GridViewTareas_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                            <asp:TemplateField HeaderText="Código" SortExpression="codigo">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("codigo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblEditCodigo" runat="server" Text='<%# Eval("codigo") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="hEstimadas" HeaderText="Horas Estimadas" SortExpression="hEstimadas" />
                            <asp:CheckBoxField DataField="explotacion" HeaderText="Activa" SortExpression="explotacion" />
                            <asp:BoundField DataField="tipoTarea" HeaderText="Tipo de Tarea" SortExpression="tipoTarea" />
                            <asp:CommandField ShowEditButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSourceTareas" runat="server" ConnectionString="Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                        SelectCommand="SELECT descripcion, codigo, hEstimadas, explotacion, tipoTarea FROM TareaGenerica WHERE codAsig = @CodigoAsignatura"
                        UpdateCommand="UPDATE TareaGenerica SET descripcion = @Descripcion, hEstimadas = @HorasEstimadas, explotacion = @Activa, tipoTarea = @TipoTarea WHERE codigo = @CodigoTarea">
                        <SelectParameters>
                            <asp:ControlParameter Name="CodigoAsignatura" ControlID="ddlAsignaturas" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Descripcion" Type="String" />
                            <asp:Parameter Name="HorasEstimadas" Type="Int32" />
                            <asp:Parameter Name="Activa" Type="Boolean" />
                            <asp:Parameter Name="TipoTarea" Type="String" />
                            <asp:Parameter Name="CodigoTarea" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </div>
                <br />
                <div>
                    <asp:Button ID="btnNuevaTarea" runat="server" Text="Nueva Tarea" OnClick="btnNuevaTarea_Click" />
                    <asp:HyperLink ID="hrefLogout" runat="server" NavigateUrl="/Inicio.aspx" OnClick="hrefLogout_OnClick">Cerrar sesión</asp:HyperLink>
                </div>
        </div>
    </form>
</body>
</html>
