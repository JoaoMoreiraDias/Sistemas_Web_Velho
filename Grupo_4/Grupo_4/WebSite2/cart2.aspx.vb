Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.HttpRequest
Partial Class cart2
    Inherits System.Web.UI.Page

    Sub showadditional_address()
        otheraddressTxtBox.Visible = True
        deliveruseraddressBtn.Visible = False
        delivertoorheraddressBtn.Visible = False
        otheraddressLbl.Visible = True
        finishorderBtn.Visible = True
    End Sub

    Sub finishOrder()
        Dim allCoocies = ReadCookie()
        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
        Dim orderflag As Boolean = True
        Dim userID As String = get_active_user()
        Dim cmd As New SqlCommand
        Dim orderGroupID As Integer = get_max_order_group_ID()
        Dim client_ip As String = Request.UserHostAddress
        Dim userAddress As String
        Dim okflag As Boolean = False
        Dim dishID As Integer

        If otheraddressTxtBox.Visible Then
            userAddress = otheraddressTxtBox.Text

        Else
            userAddress = get_user_address(userID)
        End If



        Try
            thisConnection.Open()
            'errorLbl.Text = "Connection ok"
        Catch ex As Exception
            errorLbl.Text = ex.ToString()
        End Try

        For i = 0 To allCoocies.Count - 1

            If allCoocies(i).ToString.Contains("Order") Then
                dishID = Integer.Parse(allCoocies(i).ToString.Substring(5))
                'Dim dishID As String = allCoocies(i).ToString.Substring(5, 1)
                Dim amount As Integer = Integer.Parse(allCoocies(i + 1))

                '------------------------------------------
                '(dishID, userID, amount, orderGroupID, userAddress, client_ip)
                okflag = insert_to_orders_tbl(dishID, userID, amount, orderGroupID, userAddress, client_ip)
                If okflag = True Then
                    Exit Sub
                End If

            End If

        Next i

        If Not okflag Then
            Dim aCookie As HttpCookie
            Dim i As Integer
            Dim cookieName As String
            Dim limit As Integer = Request.Cookies.Count - 1
            For i = 0 To limit
                cookieName = Request.Cookies(i).Name
                aCookie = New HttpCookie(cookieName)
                aCookie.Expires = DateTime.Now.AddDays(-1)
                Response.Cookies.Add(aCookie)
            Next
            Server.Transfer("Home.aspx")
        End If

        thisConnection.Close()
    End Sub
    Function insert_to_orders_tbl(dishID As String, userID As String, amount As Integer, orderGroupID As Integer, userAddress As String, client_ip As String) As Boolean
        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
        Dim cmd As New SqlCommand
        Dim okflag As Boolean = False

        thisConnection.Open()
        cmd.Connection = thisConnection
        cmd.CommandText = "INSERT  INTO orders (o_d_ID, o_u_ID,o_amount, o_orderdate, o_groupID, o_orderaddress, o_discountcode, o_ipaddress, o_status) " & _
            " VALUES (@o_d_ID, @u_ID, @amount, @orderdate, @groupID, @orderaddress, @discountcode  ,@ipaddress, @status )"


        With cmd.Parameters
            .AddWithValue("o_d_ID", dishID)
            .AddWithValue("u_ID", userID)
            .AddWithValue("amount", amount)
            .AddWithValue("orderdate", DateTime.Now)
            .AddWithValue("groupID", orderGroupID)
            .AddWithValue("orderaddress", userAddress)
            .AddWithValue("discountcode", discountcodeTxtbox.Text)
            .AddWithValue("ipaddress", client_ip)
            .AddWithValue("status", 1)
        End With

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            errorLbl.Text = ex.ToString
            okflag = True
        End Try

        Return okflag
    End Function
    Function ReadCookie() As ArrayList 'Get the cookie name the user entered 
        Dim i As Integer
        Dim output As New ArrayList
        Dim aCookie As HttpCookie

        For i = 0 To Request.Cookies.Count - 1
            aCookie = Request.Cookies(i)
            output.Add(Server.HtmlEncode(aCookie.Name)) 'Order ID or username
            output.Add(Server.HtmlEncode(aCookie.Value)) 'Amount
        Next

        Return output
    End Function

    Function get_active_user() As String
        Dim allCoocies = ReadCookie()

        Dim orderflag As Boolean = True
        Dim userID As String = ""


        For i = 0 To allCoocies.Count - 1

            If allCoocies(i).ToString.Contains("LoginName") Then
                userID = Integer.Parse(allCoocies(i).ToString.Substring(9))

            End If


        Next i

        Return userID
    End Function
    Function get_user_address(userID As String) As String
        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
        Dim tmpaddress As String = ""
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
                tmpaddress = reader.GetString(4)
            End If
        End Using
        thisConnection.Close()
        Return tmpaddress
    End Function
    Function get_max_order_group_ID() As Integer
        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
        Dim tmpaddress As Integer = 1
        Try
            thisConnection.Open()
            'errorLbl.Text = "Connection ok"
        Catch ex As Exception
            errorLbl.Text = ex.ToString()
        End Try

        Dim command As New SqlCommand("SELECT MAX(o_groupID) AS maxV FROM orders", thisConnection)

        Dim reader As SqlDataReader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            If Not reader.IsDBNull(0) Then
                'reader.GetInt32(0)
                tmpaddress = reader.GetInt32(0) + 1
            End If



        End If


        Return tmpaddress

    End Function
    Sub show_submitbnt()
        deliveruseraddressBtn.Visible = False
        delivertoorheraddressBtn.Visible = False
        finishorderBtn.Visible = True

    End Sub


End Class
