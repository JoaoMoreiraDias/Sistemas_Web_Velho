Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Services
Imports System.Web
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services


Partial Class _Default
    Inherits System.Web.UI.Page
    Sub load_all_dish()
        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
        Dim intTableCollumnCount As Integer = 0

        Try
            thisConnection.Open()
            'errorLbl.Text = "Connection ok"
        Catch ex As Exception
            errorLbl.Text = ex.ToString()
        End Try

        Dim tblRows As Integer = 5
        Dim tblCols As Integer = 6

        Dim tbl As Table = New Table()
        mainHolderPnl.Controls.Add(tbl)
        'tbl.BorderStyle = BorderStyle.Dotted
        'tbl.BorderWidth = Unit.Parse(3)
        'tbl.BorderColor = Drawing.Color.Brown
        tbl.HorizontalAlign = HorizontalAlign.Left

        tbl.CellSpacing = "20"
        ' tbl.CellPadding = "20"

        Dim tableStyle As New TableItemStyle()
        tableStyle.VerticalAlign = VerticalAlign.Bottom
        tableStyle.BorderStyle = BorderStyle.Solid
        tableStyle.BorderWidth = "1"
        tableStyle.BorderColor = Drawing.Color.Chocolate
        tableStyle.Width = Unit.Parse(30)
        tableStyle.Height = "250"



        Dim trow As TableRow = New TableRow()

        Dim command As New SqlCommand("SELECT  dishes.d_name, dishes.d_description, dishes.d_price, dishes.d_category, dishes.d_status, dishpics.dp_picurl, dishes.d_ID FROM  dishes INNER JOIN dishpics ON dishes.d_ID = dishpics.dp_d_ID WHERE  (dishpics.dp_main = 1)", thisConnection)

        Dim reader As SqlDataReader = command.ExecuteReader()
        While reader.Read()

            Dim dPic As New Image
            'Dim trow As TableRow = New TableRow()
            Dim tc As TableCell = New TableCell()
            tc.ApplyStyle(tableStyle)
            Dim description As New TextBox
            description.Text = ""
            description.TextMode = TextBoxMode.MultiLine
            description.Width = "150"
            description.ReadOnly = True
            description.Text = "Desciption: " & reader.GetString(1).Trim


            dPic.ImageUrl = reader.GetString(5)
            tc.Controls.Add(dPic)


            tc.Controls.Add(get_text_box_from_reader(reader.GetString(0).Trim.ToString, "Name: "))
            tc.Controls.Add(description)


            ' tc.Controls.Add(get_text_box_from_reader(reader.GetString(1).Trim.ToString, "Desciption: "))
            tc.Controls.Add(get_text_box_from_reader(reader.GetDouble(2), "Price: "))

            Dim quantityLbl As New Label
            quantityLbl.Text = "Quantity: "
            tc.Controls.Add(quantityLbl)


            Dim quantityDropDownList As New DropDownList
            For i = 1 To 10
                quantityDropDownList.Items.Add(i)
            Next
            quantityDropDownList.ID = reader.GetInt32(6).ToString

            tc.Controls.Add(quantityDropDownList)


            Dim addToCartBtn As New Button
            addToCartBtn.Text = "Add To Cart"
            ' addToCartBtn.OnClientClick = "tes();"
            addToCartBtn.OnClientClick = "setCookie(" & reader.GetInt32(6) & "," & quantityDropDownList.SelectedValue & ",5);"
            'addToCartBtn.OnClientClick = "addToCartDish(" & reader.GetInt32(6) & "," & quantityDropDownList.SelectedValue & ")"

            'addToCartBtn.OnClientClick = "return confirm('Are you sure you want to reset the selected users time for " & DateTime.Now.Year & " ');"
            tc.Controls.Add(addToCartBtn)



            trow.Cells.Add(tc)
            If intTableCollumnCount > 3 Then
                intTableCollumnCount = 0
                tbl.Rows.Add(trow)
                trow = New TableRow()
            End If


            intTableCollumnCount += 1
            'mainHolderPnl.Controls.Add(dPic)
        End While

        tbl.Rows.Add(trow)
        ViewState("dynamictable") = True

    End Sub
    Function get_text_box_from_reader(ByVal value As String, ByVal typename As String) As TextBox
        Dim tmpTextBox As New TextBox

        tmpTextBox.Text = typename & value
        tmpTextBox.ReadOnly = True
        tmpTextBox.BorderStyle = BorderStyle.None
        'tmpTextBox.TextMode = TextBoxMode.MultiLine



        Return tmpTextBox
    End Function
    Function get_label_from_reader(ByVal value As String) As Label
        Dim tmpLabel As New Label

        tmpLabel.Text = value

        Return tmpLabel
    End Function
    '    TCell.Controls.Add(New LiteralControl("<div class=""Margin"">"))
    'newPanel.Controls.Add(newHLink1)
    'PH.Controls.Add(New LiteralControl("</div>"))


End Class
