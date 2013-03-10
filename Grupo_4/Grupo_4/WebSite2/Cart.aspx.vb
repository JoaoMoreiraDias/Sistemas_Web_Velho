Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Partial Class _Default
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("cookie") IsNot Nothing Then
            ReadCookie()
        End If
    End Sub
    Sub chechlogin()
        Dim allCoocies = ReadCookie()
        Dim loginflag As Boolean = True


        For i = 0 To allCoocies.Count - 1
            If allCoocies(i).ToString.Contains("LoginName") Then
                Dim userID As String = Integer.Parse(allCoocies(i).ToString.Substring(9))
                Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
                Dim password As String = allCoocies(i + 1)

                Try
                    thisConnection.Open()
                    'errorLbl.Text = "Connection ok"
                Catch ex As Exception
                    errorLbl.Text = ex.ToString()
                End Try

                Using command As New SqlCommand("SELECT * FROM users WHERE  (u_ID= '" & userID & "')", thisConnection)

                    Dim reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        reader.Read()
                        'loginname.Text = reader.GetString(3) & "|||" & password
                        If String.Equals(reader.GetString(3).Trim, password) Then
                            Dim continueorder As New LinkButton
                            continueorder.Text = "Continue Order"
                            continueorder.PostBackUrl = "cart2.aspx"
                            LoginPnl.Controls.Add(continueorder)
                            loginflag = False
                        End If
                    End If
                End Using
            End If

        Next

        If loginflag Then
            Dim loginbtn As New LinkButton
            loginbtn.Text = "Please login"
            loginbtn.PostBackUrl = "Login.aspx"

            LoginPnl.Controls.Add(loginbtn)
        End If
    End Sub

    Function ReadCookie() As ArrayList 'Get the cookie name the user entered 
        Dim i As Integer
        Dim output As New ArrayList
        Dim aCookie As HttpCookie

        For i = 0 To Request.Cookies.Count - 1
            aCookie = Request.Cookies(i)
            output.Add(Server.HtmlEncode(aCookie.Name)) 'Order ID
            output.Add(Server.HtmlEncode(aCookie.Value)) 'Amount
        Next

        Return output
    End Function
    Sub show_cart_items()
        Dim allCoocies = ReadCookie()
        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
        Dim orderflag As Boolean = True



        Try
            thisConnection.Open()
            'errorLbl.Text = "Connection ok"
        Catch ex As Exception
            errorLbl.Text = ex.ToString()
        End Try


        Dim tbl As Table = New Table()
        cart.Controls.Add(tbl)
        'tbl.BorderStyle = BorderStyle.Dotted
        'tbl.BorderWidth = Unit.Parse(3)
        'tbl.BorderColor = Drawing.Color.Brown
        tbl.HorizontalAlign = HorizontalAlign.Left


        'tbl.CellSpacing = "20"
        'tbl.CellPadding = "20"

        Dim tableStyle As New TableItemStyle()
        tableStyle.VerticalAlign = VerticalAlign.Bottom
        tableStyle.BorderStyle = BorderStyle.Solid
        tableStyle.BorderWidth = "2"
        tableStyle.BorderColor = Drawing.Color.Chocolate
        tableStyle.Width = "90"
        'tableStyle.Width = Unit.Parse(30)
        'tableStyle.Height = "250"

        Dim tc As TableCell = New TableCell()
        Dim trow As TableRow = New TableRow()
        tc.ApplyStyle(tableStyle)
        tc.Controls.Add(get_string_as_label("Dish Name"))
        trow.Cells.Add(tc)

        tc = New TableCell()
        tc.ApplyStyle(tableStyle)
        tc.Controls.Add(get_string_as_label("Total Price"))
        trow.Cells.Add(tc)
        tbl.Rows.Add(trow)
        trow = New TableRow()



        For i = 0 To allCoocies.Count - 1

            If allCoocies(i).ToString.Contains("Order") Then
                Dim dishID As Integer = Integer.Parse(allCoocies(i).ToString.Substring(5))
                'Dim dishID As String = allCoocies(i).ToString.Substring(5, 1)
                Dim amount As Integer = Integer.Parse(allCoocies(i + 1))
                orderflag = False

               
                thisConnection.Close()
                thisConnection.Open()


                Dim command As New SqlCommand("SELECT * FROM dishes WHERE  (d_ID= '" & dishID & "')", thisConnection)

                Dim reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()

                    tc = New TableCell()
                    tc.ApplyStyle(tableStyle)
                    Dim dnametxtbox As New Label
                    dnametxtbox.Text = reader.GetString(1)
                    'dnametxtbox.ReadOnly = True
                    tc.Controls.Add(dnametxtbox)
                    trow.Cells.Add(tc)


                    tc = New TableCell()
                    tc.ApplyStyle(tableStyle)
                    Dim dpricetxtbox As New Label
                    dpricetxtbox.Text = reader.GetDouble(4) * amount
                    'dpricetxtbox.ReadOnly = True
                    tc.Controls.Add(dpricetxtbox)


                    trow.Cells.Add(tc)

                    tbl.Rows.Add(trow)
                    trow = New TableRow()

                End While



            ElseIf orderflag Then

                errorLbl.Text = "You didnt order anything"
                tbl.Visible = False



            End If

        Next
        tbl.Rows.Add(trow)
    End Sub

    Function get_string_as_label(value As String) As Label

        Dim tmplabel As New Label
        tmplabel.Text = value.ToString

        Return tmplabel

    End Function

End Class
