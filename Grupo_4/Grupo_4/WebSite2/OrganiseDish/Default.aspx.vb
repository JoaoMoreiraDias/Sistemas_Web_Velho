Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Partial Class OrganiseDish_Default
    Inherits System.Web.UI.Page
    'Public imageCount As Integer = 0


    Sub Page_Load(Src As Object, e As EventArgs)
        testlabel.Text = "The date and time is "
    End Sub
    Sub check_coocies()
        Dim i As Integer
        Dim output As New ArrayList
        Dim aCookie As HttpCookie

        For i = 0 To Request.Cookies.Count - 1
            aCookie = Request.Cookies(i)
            output.Add(Server.HtmlEncode(aCookie.Name)) 'Order ID or username
            output.Add(Server.HtmlEncode(aCookie.Value)) 'Amount
        Next

        For i = 0 To output.Count - 1

            If output(i).ToString.Contains("LoginName") Then
                add_Dish_btn.Visible = True

            Else
                add_Dish_btn.Visible = False
                dImgErrorLbl.Text = "Please login"
            End If
        Next i


    End Sub

    Sub show_form_for_add_dish(sender As Object, e As EventArgs)
        pnlform.Visible = True
        add_Dish_btn.Visible = False
        'Me.dCategoryDropDownList.Items.Add("shh")
        'Me.dCategoryDropDownList.Items.Add("blab bal")

        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)


        Try
            thisConnection.Open()
            testlabel.Text = "Connection ok"
        Catch ex As Exception
            testlabel.Text = ex.ToString()
        End Try

        Using command As New SqlCommand("SELECT d_category FROM dishes", thisConnection)
            Dim reader As SqlDataReader = command.ExecuteReader()
            While reader.Read()
                Me.dCategoryDropDownList.Items.Add(reader.GetString(0))
            End While
        End Using

    End Sub

    Sub connect_to_DB()
        testlabel.Text = ""

        'Dim dbconn = New SqlConnection("Data Source=e=" & Server.MapPath("~\App_Data\cafeDB.mdf"))
        Dim dbconn = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\val\Documents\Visual Studio 2010\WebSites\WebSite2\App_Data\cafeDB.mdf;Integrated Security=True;User Instance=True")
        ' Data Source=.\SQLEXPRESS;AttachDbFilename="C:\Users\val\Documents\Visual Studio 2010\WebSites\WebSite2\App_Data\cafeDB.mdf";Integrated Security=True;User Instance=True
        Try
            dbconn.Open()
            testlabel.Text = "Connection ok"
        Catch ex As Exception
            testlabel.Text = ex.ToString()
        End Try

    End Sub
    Sub submit_dish_data_toDB()
        Dim cmd As New SqlCommand
        Dim arImgUrl As New ArrayList()
        Dim intMaxDishID As Integer


        Dim thisConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
        'Dim constring As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        'dImgErrorLbl.Text = constring
        Try
            thisConnection.Open()

        Catch ex As Exception
            dImgErrorLbl.Text = ex.ToString
        End Try

        Using command As New SqlCommand("SELECT MAX(d_ID) FROM dishes", thisConnection)
            Dim reader As SqlDataReader = command.ExecuteReader()
            While reader.Read()
                intMaxDishID = reader.GetInt32(0) + 1
            End While
        End Using

        testlabel.Text = intMaxDishID.ToString

        thisConnection.Close()

        thisConnection.Open()


        cmd.Connection = thisConnection
        cmd.CommandText = "INSERT  INTO dishes (d_name, d_description,d_additionalinfo, d_price, d_category, d_dateadded, d_status) " & _
            " VALUES (@name, @description, @additionalinfo, @price, @category, @nowtime, @status )"


        With cmd.Parameters
            .AddWithValue("name", dNameTxtbox.Text)
            .AddWithValue("description", dDecriptionTxtbox.Text)
            .AddWithValue("additionalinfo", dAdditioninfoTxtbox.Text)
            .AddWithValue("price", dPriceTxtbox.Text)
            .AddWithValue("category", dCategoryDropDownList.SelectedValue)
            .AddWithValue("nowtime", DateTime.Now)
            .AddWithValue("status", "1")
        End With

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            dImgErrorLbl.Text = ex.ToString
        End Try


        If dImageLink1.HasFile Then
            upl_image(dImageLink1, arImgUrl)
        End If
        If dImageLink2.HasFile Then
            upl_image(dImageLink2, arImgUrl)
        End If
        If dImageLink3.HasFile Then
            upl_image(dImageLink3, arImgUrl)
        End If


        For Each item In arImgUrl
            cmd.CommandText = "INSERT  INTO dishpics (dp_d_ID, dp_picurl,dp_main) " & _
            " VALUES ('" & intMaxDishID & "', '" & item & "', '" & 0 & "' )"
            cmd.ExecuteNonQuery()

        Next

        'thisConnection.Close()







    End Sub

    Sub upl_image(ByVal imgUrl As FileUpload, ByRef arrayImgUrl As ArrayList)

        Dim intWidth, intHeight As Integer
        Dim UlFileName, NewFileName As String


        intWidth = 100 '*** Fix Width ***'  
        intHeight = 0   '*** If = 0 Auto Re-Cal Size ***'  
        'intHeight = 120

        UlFileName = "~/Uploads/" & imgUrl.FileName

        '*** Save Images ***'  
        imgUrl.SaveAs(Server.MapPath(UlFileName))

        NewFileName = "~/Uploads/thumb/" & imgUrl.FileName

        Dim objGraphic As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(UlFileName))

        Dim objBitmap As Bitmap
        '*** Calculate Height ***'  
        If intHeight > 0 Then
            objBitmap = New Bitmap(objGraphic, intWidth, intHeight)
        Else
            If objGraphic.Width > intWidth Then
                Dim ratio As Double = objGraphic.Height / objGraphic.Width
                intHeight = ratio * intWidth
                objBitmap = New Bitmap(objGraphic, intWidth, intHeight)
            Else
                objBitmap = New Bitmap(objGraphic)
            End If
        End If

        '*** Save As  ***'  
        objBitmap.Save(Server.MapPath(NewFileName), objGraphic.RawFormat)

        ' Add both files to arrray
        arrayImgUrl.Add(UlFileName)
        arrayImgUrl.Add(NewFileName)




        '*** Close ***'  
        objGraphic.Dispose()





    End Sub
    Sub change_visibility_of_imagelink(sender As Object, e As CommandEventArgs)
        Dim ID As Integer = CType(e.CommandArgument, Integer)

        Select Case ID
            Case 1
                dImageLink2.Visible = True
                addMoreImgBtn2.Visible = True
                addMoreImgBtn1.Visible = False
            Case 2
                dImageLink3.Visible = True
                addMoreImgBtn2.Visible = False
            Case Else
                dImageLink3.Visible = True
        End Select
    End Sub
    Sub make_dCategoryAddMoreTxtbox_visible()

        dCategoryAddMoreTxtbox.Visible = True
        dCategoryAddMoreTxtbox.Text = ""
        dCategoriesAddMoreSubmitBtn.Visible = True
        dCategoriesAddMoreBtn.Visible = False


    End Sub
    Sub add_one_category()
        dCategoriesAddMoreSubmitBtn.Visible = False
        dCategoryAddMoreTxtbox.Visible = False
        dCategoriesAddMoreBtn.Visible = True
        Me.dCategoryDropDownList.Items.Add(dCategoryAddMoreTxtbox.Text)

        dCategoryDropDownList.SelectedValue = dCategoryAddMoreTxtbox.Text

    End Sub

End Class
