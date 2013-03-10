Imports System.Data.OleDb
Imports System.Data.SqlClient
Partial Class Login
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        errorLbl.visible = false
    End Sub
    Sub check_login()
        Dim loginN As String = LoginNameTxtbox.Text
        Dim pass As String = PasswordTxtbox.Text
        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)

        Try
            thisConnection.Open()
            'errorLbl.Text = "Connection ok"
        Catch ex As Exception
            errorLbl.Text = ex.ToString()
        End Try
        'If allCoocies(i).ToString.Contains("LoginName") Then
        'Order1
        Using command As New SqlCommand("SELECT * FROM users WHERE  (u_nick = N'" & loginN & "') AND (u_password = N'" & pass & "')", thisConnection)

            Dim reader As SqlDataReader = command.ExecuteReader()
            If reader.HasRows Then
                reader.Read()

                Dim usedID As String = "LoginName" & reader.GetInt32(0)
                Response.Cookies(usedID).Expires = "5/1/2013"
                Response.Cookies(usedID).Value = pass
                Server.Transfer("Home.aspx")

            Else
                pleaseRegisterLinkLbl.Text = "HERE"
                pleaseRegisterLinkLbl.PostBackUrl = "register.aspx"
                errorLbl.Text = "Please register"
                errorLbl.Visible = True
                LoginNameTxtbox.Visible = False
                PasswordTxtbox.Visible = False
                LoginBtn.Visible = False


            End If



        End Using


    End Sub
End Class
