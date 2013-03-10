Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
Partial Class register
    Inherits System.Web.UI.Page
    Sub check_form()
        errorLbl.Text = ""

        If Not check_if_nick_exist(uNickTxtbox.Text) Then


            Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim cmd As New SqlCommand


            Try
                thisConnection.Open()
                'errorLbl.Text = "Connection ok"
            Catch ex As Exception
                errorLbl.Text = ex.ToString()
            End Try

            cmd.Connection = thisConnection
            cmd.CommandText = "INSERT  INTO users (u_name, u_nick,u_password, u_address, u_registrationdate, u_type) " & _
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
        Else
            errorLbl.Text = uNickTxtbox.Text & " already exist. Please choose another nick."

            For Each contr In Controls
                If TypeOf contr Is TextBox Then
                    CType(contr, TextBox).Text = String.Empty
                End If

            Next
        End If

    End Sub
    Function check_if_nick_exist(nick As String) As Boolean
        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
        Dim foundFlag As Boolean = False

        thisConnection.Open()
        Using command As New SqlCommand("SELECT * FROM users WHERE  (u_nick= '" & nick & "')", thisConnection)

            Dim reader As SqlDataReader = command.ExecuteReader()
            If reader.HasRows Then
                foundFlag = True
            End If
        End Using

        Return foundFlag
    End Function

End Class
