Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
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



        Try
            thisConnection.Open()
            'errorLbl.Text = "Connection ok"
        Catch ex As Exception
            errorLbl.Text = ex.ToString()
        End Try

        For i = 0 To allCoocies.Count - 1

            If allCoocies(i).ToString.Contains("Order") Then
                Dim dishID As Integer = Integer.Parse(allCoocies(i).ToString.Substring(5, 1))
                'Dim dishID As String = allCoocies(i).ToString.Substring(5, 1)
                Dim amount As Integer = Integer.Parse(allCoocies(i + 1))

                '------------------------------------------
                cmd.Connection = thisConnection
                cmd.CommandText = "INSERT  INTO orders (u_name, u_nick,u_password, u_address, u_registrationdate, u_type) " & _
                    " VALUES (@name, @nick, @password, @address, @registrationdate, @type)"


                With cmd.Parameters
                    .AddWithValue("name", uNameTxtbox.Text)
                    .AddWithValue("nick", uNickTxtbox.Text)
                    .AddWithValue("password", uPasswordTxtbox.Text)
                    .AddWithValue("address", uAddressTxtbox.Text)
                    .AddWithValue("registrationdate", DateTime.Now)
                    .AddWithValue("type", 1)

                End With

                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    errorLbl.Text = ex.ToString
                End Try
            End If

        Next i


    End Sub

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

        Return tmpaddress
    End Function
    Function get_max_order_group_ID() As String
        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
        Dim tmpaddress As String = "1"
        Try
            thisConnection.Open()
            'errorLbl.Text = "Connection ok"
        Catch ex As Exception
            errorLbl.Text = ex.ToString()
        End Try

        Using command As New SqlCommand("SELECT MAX( FROM orders )", thisConnection)

            Dim reader As SqlDataReader = command.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                tmpaddress = reader.GetString(4)
            End If
        End Using

        Return tmpaddress

    End Function
End Class
