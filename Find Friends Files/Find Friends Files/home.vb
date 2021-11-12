Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks
Imports System.Net.Http

Public Class home
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function
    Dim sux As String = 0
    Dim server As String = "https://apps.sunstudiodev.com/fff/"
    Dim u_ketnoi, ip_ketnoi, cor As String
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim tukhoa As String
    Dim dulieuk As String = ""
    Dim takaa As String = ""

    Dim result_get As String = ""
    Dim still_online As Boolean = True
    Dim still_startsv As Boolean = True
    Dim still_check As Boolean = True
    Dim still_getdata As Boolean = True
    Dim still_xulydata As Boolean = True
    Dim still_ngatkenoi As Boolean = True

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles end_S.Click
        End
    End Sub
    Private Function kiemtrafd(txtbox As String) As String
        If txtbox = "" Then
            MsgBox("Không có thư mục nào được chọn!", vbInformation, "Lời nhắn nhỏ")
        ElseIf txtbox.Length <= 3 Then
            MsgBox("Thư mục không hợp lệ, không được chọn ổ đĩa!", vbInformation, "Lời nhắn nhỏ")
        Else
            Return "123"
        End If
    End Function
    Private Sub bt_go_Click(sender As Object, e As EventArgs) Handles bt_go.Click
        Dim kqtr As String = kiemtrafd(txt_folder.Text)
        If kqtr = "123" Then
            tc1.SelectedTab = tp2
            bt_stop.Enabled = True
            bt_go.Enabled = False
            bt_fd.Enabled = False
            st_ht.BackColor = Color.Lime
            lb_Ss.Text = "Sẵn sàng kết nối!"
            txt_status.Text += "Client của bạn đã khởi chạy!" & vbNewLine
            txt_status.Text += "Đã sẵn sàng kết nối từ máy bạn!" & vbNewLine
            'online.Start()
            checkOnline()
            ' tm1.Start()
            startServer()

        End If

    End Sub

    Private Sub end_S_MouseMove(sender As Object, e As MouseEventArgs) Handles end_S.MouseMove
        end_S.BackColor = Color.Gainsboro
    End Sub




    Private Sub bt_stop_Click(sender As Object, e As EventArgs) Handles bt_stop.Click
        dunglai()
        cor = "23"
    End Sub

    Private Sub ll1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ll1.LinkClicked
        System.Diagnostics.Process.Start("https://apps.sunstudiodev.com/fff/")
    End Sub

    Private Sub mini_S_Click(sender As Object, e As EventArgs) Handles mini_S.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub mini_S_MouseMove(sender As Object, e As MouseEventArgs) Handles mini_S.MouseMove
        mini_S.BackColor = Color.Gainsboro

    End Sub



    Private Sub hm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'antiSandboxie()
        txt_id.Text = Environment.MachineName & "_" & IntText(CpuId())
        txt_pass.Text = IntPass(GenRandomInt(1, 55555555))
        '  SendPostData()
        Dim sadsaf As String = CentralnGetSource(server & "a+_clientnew.add.php?yunika123567_a1a2xx33c55d5d5d=" & base64en(txt_id.Text) & "&kanichiwa_vinaoffVNSSSXzzz=" & base64en(txt_pass.Text))
        '  MsgBox(sadsaf)
        '  txt_folder.Text = server & "a+_clientnew.add.php?yunika123567_a1a2xx33c55d5d5d=" & base64en(txt_id.Text) & "&kanichiwa_vinaoffVNSSSXzzz=" & base64en(txt_pass.Text)
        'xoa cho phep
        Dim delsadtdsf As String = CentralnGetSource(server & "bankt_wait.php?denghi=" & base64en(txt_id.Text))
        '  MsgBox(sadsaf)

        Button3.Visible = False

    End Sub
    Private Function GenRandomInt(min As Int32, max As Int32) As Int32
        Static staticRandomGenerator As New System.Random
        Return staticRandomGenerator.Next(min, max + 1)
    End Function
    Private Function IntPass(textk As String) As String
        textk = textk.Replace("1", "M")
        textk = textk.Replace("0", "X")
        textk = textk.Replace("3", "Z")
        textk = textk.Replace("4", "A")
        textk = textk.Replace("6", "H")
        textk = textk.Replace("8", "U")
        textk = textk.Replace("11", "K")
        textk = textk.Replace("22", "L")
        Return textk
    End Function
    Private Function IntText(textk As String) As String
        textk = textk.Replace("1", "P")
        textk = textk.Replace("0", "Z")
        textk = textk.Replace("2", "Y")
        textk = textk.Replace("3", "L")
        textk = textk.Replace("4", "A")
        textk = textk.Replace("5", "D")
        textk = textk.Replace("6", "F")
        textk = textk.Replace("7", "G")
        textk = textk.Replace("8", "H")
        textk = textk.Replace("9", "Q")
        Return textk
    End Function
    Private Function base64en(datamahoa As String) As String
        Dim bytesToEncode As Byte()
        bytesToEncode = Encoding.UTF8.GetBytes(datamahoa)
        Dim encodedText As String
        encodedText = Convert.ToBase64String(bytesToEncode)
        Return encodedText
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles bt_fd.Click
        Dim folderDlg As New FolderBrowserDialog
        folderDlg.ShowNewFolderButton = True
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            txt_folder.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        kiemtrafd(txt_folder.Text)
    End Sub
    Private Function CpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" &
            "{impersonationLevel=impersonate}!\\" &
            computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from " &
            "Win32_Processor")

        Dim cpu_ids As String = ""
        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If cpu_ids.Length > 0 Then cpu_ids =
            cpu_ids.Substring(2)

        Return cpu_ids
    End Function

    Private Sub txt_id_TextChanged(sender As Object, e As EventArgs) Handles txt_id.TextChanged
        txt_id.Text = txt_id.Text
    End Sub

    Private Sub bt1_Click(sender As Object, e As EventArgs) Handles bt1.Click
        txt_pass.Text = IntPass(GenRandomInt(1, 55555555))
        Dim sadsaf As String = CentralnGetSource(server & "a+_clientnew.add.php?yunika123567_a1a2xx33c55d5d5d=" & base64en(txt_id.Text) & "&kanichiwa_vinaoffVNSSSXzzz=" & base64en(txt_pass.Text))

    End Sub

    Public Sub SendPostData(ByVal site As String, ByVal message As String)

        Dim request As WebRequest
        request = WebRequest.Create(site)
        Dim response As WebResponse
        Dim postData As String = "yunika123567_X@#xcSD#$!@!#ASDF%$=" & message
        Dim data As Byte() = Encoding.UTF8.GetBytes(postData)


        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = data.Length

        Dim stream As Stream = request.GetRequestStream()
        stream.Write(data, 0, data.Length)
        stream.Close()

        response = request.GetResponse()
        Dim sr As New StreamReader(response.GetResponseStream())



    End Sub
    Public Function getUrlSource2(url As String) As String
        Try
            Dim Request As HttpWebRequest = HttpWebRequest.Create(url)
            Dim Response As HttpWebResponse = Request.GetResponse()
            Dim reader As StreamReader = New StreamReader(Response.GetResponseStream)
            Dim httpContent As String
            httpContent = reader.ReadToEnd
            Return httpContent
        Catch
            Return "error"
        End Try
    End Function
    Async Function getUrlSource(pathurl As String) As Task
        Using client As New HttpClient()
            Dim address As Uri = New Uri(pathurl)
            Dim getStringTask As Task(Of String) =
            client.GetStringAsync(pathurl)
            ' Dim rawresp As String = ( getStringTask)
            result_get = Await getStringTask
        End Using
    End Function
    Public Function CentralnGetSource(url As String) As String
        getUrlSource(url)
        Return result_get
    End Function

    Async Function checkOnline() As Task
        Using client As New HttpClient()
            '  Dim address As Uri = New Uri(server & "timeonof.php?reset=" & base64en(txt_id.Text))
            Dim getStringTask As Task(Of String) =
            client.GetStringAsync(server & "timeonof.php?reset=" & base64en(txt_id.Text))
            ' Dim rawresp As String = ( getStringTask)
            Dim result_get = Await getStringTask
            If (still_online) And (result_get.Length > 0) Then
                checkOnline()
            End If
        End Using
    End Function
    Public Function antiSandboxie() As Boolean
        Dim Sandboxie As Integer
        If Process.GetProcessesByName("SbieSvc").Length >= 1 Then
            Sandboxie = FindWindow("SandboxieControlWndClass", vbNullString)
            If Sandboxie = 0 Then
                MsgBox("Detected Sandboxie !", MsgBoxStyle.Information, "LHT")
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Async Function getdata() As Task
        Using client As New HttpClient()
            If (still_getdata) Then
                Dim getStringTask As Task(Of String) =
            client.GetStringAsync(server & "getdata.php?namesv=" & base64en(txt_id.Text) & "&data=" & base64en("conghiala"))

                Dim nhandatas = Await getStringTask
                If nhandatas.Length > 0 Then
                    If takaa = "0" Then

                    Else


                        tukhoa = nhandatas
                        txt_status.Text = txt_status.Text & "Đã nhận được yêu tìm kiếm từ khoá '" & nhandatas & "'. Tiến hành tìm kiếm..." & vbNewLine
                        xuongdong()
                        takaa = "0"
                        '    lb_num.Text = 2
                        '  ngatkn2.Start()
                        xulydata()
                        still_getdata = False
                    End If
                    'Else
                    '   getUrlSource(server & "getdata.php?namesv1=" & base64en(txt_id.Text) & "&k12=" & base64en("Từ khoá yêu cầu quá ngắn!"))
                End If
                getdata()
            End If
        End Using
    End Function

    Async Function xulydata() As Task
        Using client As New HttpClient()
            If (still_xulydata) Then
                '  If lb_num.Text = 5 Then
                Dim Folder As New IO.DirectoryInfo(txt_folder.Text)
                For Each File As IO.FileInfo In Folder.GetFiles("*" & tukhoa & "*.*", IO.SearchOption.AllDirectories)
                    ' lsb1.Items.Add(File.FullName)

                    ' Me.Text = getUrlSource("file:///" & test2.Replace(" ", "%20"))
                    '
                    'numk = numk + 1
                    '  Dim Sat As String = IO.File.ReadAllText(File.FullName) 'Đọc hết các dòng của file
                    ' txt1.Text = txt1.Text & "Result " & numk & ":[u0k1]" & Sat & "[u0k0]" & vbNewLine
                    Dim FileInfo As New FileInfo(File.FullName)
                    txt_data.Text = txt_data.Text & FileInfo.Name & "<p>"
                    '
                    Application.DoEvents()
                    FileInfo = Nothing
                Next


                ' CentralnGetSource(server & "getdata.php?namesv1=" & base64en(txt_id.Text) & "&k12=" & base64en(txt_data.Text))
                Dim getStringTask As Task(Of String) =
            client.GetStringAsync(server & "getdata.php?namesv1=" & base64en(txt_id.Text) & "&k12=" & base64en(txt_data.Text))

                Dim result_get = Await getStringTask
                txt_status.Text = txt_status.Text & "Xử lí hoàn tất tìm kiếm '" & tukhoa & "'" & vbNewLine
                '  numks = numks + 1
                '  txt_xl.Text = "Đã xử lí: " & numks & " yêu cầu"
                '  txt_kq.Text = "Đã xử lí: " & numk & " kết quả"
                '   txtbyt()
                xuongdong()
                txt_data.Text = ""
                tukhoa = ""
                '  lb_num.Text = 2
                getdata()
                takaa = "1"
                '   online.Start()
                still_xulydata = False

                ' End If
                xulydata()
            End If
        End Using
    End Function
    Async Function startCheck() As Task 'check xem co yeu cau ket noi nao khong
        Using client As New HttpClient()
            If (still_check) Then
                Dim getStringTask As Task(Of String) =
            client.GetStringAsync(server & "waitforserverntn.php?address=" & base64en(txt_id.Text) & "&pass=" & base64en(txt_pass.Text))

                'If lb_num.Text = 0 Then
                'lay ket qua xem co ban nao ket noi ko????
                Dim kqp As String = Await getStringTask
                '  txt_folder.Text = kqp
                If kqp.Length > 5 Then
                    Dim results = kqp.Split("|")
                    ip_ketnoi = results(0)
                    u_ketnoi = results(1)
                    '   lb_Ss.Text = "Đang kết nối từ " & ip_ketnoi & " với tên " & u_ketnoi
                    ' txt_status.Text = txt_status.Text & lb_Ss.Text & vbNewLine
                    Dim ketnoidcr As String = CentralnGetSource(server & "bankt_wait.php?address=" & base64en(txt_id.Text) & "&pass=" & base64en(txt_pass.Text) & "&pc_name=" & base64en(u_ketnoi))
                    ' MsgBox ()
                    If ketnoidcr = "chophep" + u_ketnoi Then
                        lb_Ss.Text = "Đang kết nối từ " & ip_ketnoi & " với tên " & u_ketnoi
                        txt_status.Text = txt_status.Text & lb_Ss.Text & vbNewLine
                        xuongdong()
                        '   lb_num.Text = 1
                        getdata()
                        ngatketnoi()
                        ' tmchay.Stop()
                        still_check = False
                    Else
                        '  MsgBox("chophep" + u_ketnoi)
                        ' dunglai()
                        ' tmchay.Stop()
                    End If
                End If
                startCheck()
            End If
        End Using
    End Function
    Async Function startServer() As Task
        Using client As New HttpClient()
            If (still_startsv) Then
                Dim getStringTask As Task(Of String) =
            client.GetStringAsync(server & "sts_oft_of_client.php?stst123nguoiodauroihum=on&id=" & base64en(txt_id.Text))
                Dim sts123 As String = Await getStringTask
                If sts123 = "on" Then
                    If cor = "roi" Then

                    Else
                        st_ht.BackColor = Color.Lime
                        lb_Ss.Text = "Sẵn sàng kết nối!"
                        ' tmchay.Start()
                        startCheck()
                        cor = "roi"
                    End If
                    ' tm1.Stop()
                ElseIf sts123 = "off" Then
                    st_ht.BackColor = Color.LightCoral
                    lb_Ss.Text = "Chưa sẵn sàng kết nối..."
                Else
                    st_ht.BackColor = Color.LightCoral
                    lb_Ss.Text = "Máy chủ xảy ra lỗi...."
                End If
            End If
        End Using
    End Function

    Async Function ngatketnoi() As Task
        Using client As New HttpClient()
            If (still_ngatkenoi) Then
                'If lb_num.Text = 2 Then
                ' online.Start()
                checkOnline()
                Dim ngatkn As String = CentralnGetSource(server & "bankt_wait.php?ngatdi2=" & base64en(txt_id.Text))
                Dim cl_s1 As String = CentralnGetSource(server & "timeonof.php?s1=" & base64en(txt_id.Text))
                If ngatkn = "offr" Or cl_s1 = "12345" Then
                    st_ht.BackColor = Color.Lime
                    lb_Ss.Text = "Sẵn sàng kết nối!"
                    txt_status.Text = txt_status.Text & u_ketnoi & " đã ngắt kết nối..." & vbNewLine
                    Dim ngatkn1 As String = CentralnGetSource(server & "bankt_wait.php?ngatdi=" & base64en(txt_id.Text))
                    Dim xoaclient As String = CentralnGetSource(server & "del_kochay.php?ipk=" & base64en(ip_ketnoi))
                    xuongdong()
                    ' tm1.Start()
                    startServer()
                    ' tmchay.Start()
                    startCheck()
                    '    lb_num.Text = 0

                    getdata()
                    xulydata()
                    still_ngatkenoi = False
                    '  Else
                    ' lb_num.Text = 4
                    '      online.Start()
                    'lb_num.Text = 1
                End If
                ' End If
            End If
        End Using
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CentralnGetSource(server & "sts_oft_of_client.php?stst123nguoiodauroihum=off&id=" & base64en(txt_id.Text))
        MsgBox("Đã huỷ các phiên!", vbInformation, "Lời nhắn nhỏ")
    End Sub

    Private Sub bt_del_Click(sender As Object, e As EventArgs) Handles bt_del.Click
        txt_status.Clear()
        MsgBox("Đã làm mới!", vbInformation, "Lời nhắn nhỏ")
    End Sub

    Private Sub autodon_Tick(sender As Object, e As EventArgs) Handles autodon.Tick
        txt_status.Clear()
    End Sub

    Private Sub ck_clean_CheckedChanged(sender As Object, e As EventArgs) Handles ck_clean.CheckedChanged
        If ck_clean.Checked = True Then
            autodon.Start()
        Else
            autodon.Stop()
        End If
    End Sub

    'Private Sub ngatkn_Tick(sender As Object, e As EventArgs) Handles ngatkn2.Tick
    '    'If lb_num.Text = 2 Then
    '    ' online.Start()
    '    checkOnline()
    '    Dim ngatkn As String = CentralnGetSource(server & "bankt_wait.php?ngatdi2=" & base64en(txt_id.Text))
    'Dim cl_s1 As String = CentralnGetSource(server & "timeonof.php?s1=" & base64en(txt_id.Text))
    'If ngatkn = "offr" Or cl_s1 = "12345" Then
    '        st_ht.BackColor = Color.Lime
    '        lb_Ss.Text = "Sẵn sàng kết nối!"
    '        txt_status.Text = txt_status.Text & u_ketnoi & " đã ngắt kết nối..." & vbNewLine
    '        Dim ngatkn1 As String = CentralnGetSource(server & "bankt_wait.php?ngatdi=" & base64en(txt_id.Text))
    'Dim xoaclient As String = CentralnGetSource(server & "del_kochay.php?ipk=" & base64en(ip_ketnoi))
    '        xuongdong()
    '        ' tm1.Start()
    '        startServer()
    '        ' tmchay.Start()
    '        startCheck()
    '        '    lb_num.Text = 0

    '        getdata()
    '        xulydata()
    '        ngatkn2.Stop()
    '        '  Else
    '        ' lb_num.Text = 4
    '        '      online.Start()
    '        'lb_num.Text = 1
    '    End If
    '' End If
    'End Sub
    Sub xuongdong()
        txt_status.SelectionStart = txt_status.Text.Length
        txt_status.ScrollToCaret()
    End Sub

    Sub dunglai()
        'tm1.Stop()
        still_startsv = False
        ' tmchay.Stop()
        still_check = False
        '  online.Stop()
        still_online = False
        'ngatkn2.Stop()
        still_ngatkenoi = False
        tc1.SelectedTab = tapct1
        st_ht.BackColor = Color.LightCoral
        lb_Ss.Text = "Chưa sẵn sàng kết nối..."
        bt_stop.Enabled = False
        bt_go.Enabled = True
        bt_fd.Enabled = True
        CentralnGetSource(server & "sts_oft_of_client.php?stst123nguoiodauroihum=off&id=" & base64en(txt_id.Text))
        txt_status.Clear()

    End Sub

    Private Sub hm_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        drag = False

    End Sub
    Private Sub pn1_MouseUp(sender As Object, e As MouseEventArgs) Handles pn1.MouseUp
        drag = False

    End Sub
    Private Sub hm_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If

    End Sub
    Private Sub pn1_MouseMove(sender As Object, e As MouseEventArgs) Handles pn1.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
        end_S.BackColor = Color.White
        mini_S.BackColor = Color.White
    End Sub




    'Private Sub nhandata_Tick(sender As Object, e As EventArgs) Handles nhandata.Tick
    '    '  If lb_num.Text = 1 Then
    '    ' online.Start()

    '    Dim nhandatas As String = CentralnGetSource(server & "getdata.php?namesv=" & base64en(txt_id.Text) & "&data=" & base64en("conghiala"))
    '    If nhandatas.Length > 0 Then
    '        If takaa = "0" Then

    '        Else


    '            tukhoa = nhandatas
    '            txt_status.Text = txt_status.Text & "Đã nhận được yêu tìm kiếm từ khoá '" & nhandatas & "'. Tiến hành tìm kiếm..." & vbNewLine
    '            xuongdong()
    '            takaa = "0"
    '            '    lb_num.Text = 2
    '            '  ngatkn2.Start()
    '            xulidata.Start()
    '            nhandata.Stop()
    '        End If
    '        'Else
    '        '   getUrlSource(server & "getdata.php?namesv1=" & base64en(txt_id.Text) & "&k12=" & base64en("Từ khoá yêu cầu quá ngắn!"))
    '    End If
    '    ' End If
    'End Sub

    'Private Sub xulidata_Tick(sender As Object, e As EventArgs) Handles xulidata.Tick
    '    '  If lb_num.Text = 5 Then
    '    Dim Folder As New IO.DirectoryInfo(txt_folder.Text)
    '    For Each File As IO.FileInfo In Folder.GetFiles("*" & tukhoa & "*.*", IO.SearchOption.AllDirectories)
    '        ' lsb1.Items.Add(File.FullName)

    '        ' Me.Text = getUrlSource("file:///" & test2.Replace(" ", "%20"))
    '        '
    '        'numk = numk + 1
    '        '  Dim Sat As String = IO.File.ReadAllText(File.FullName) 'Đọc hết các dòng của file
    '        ' txt1.Text = txt1.Text & "Result " & numk & ":[u0k1]" & Sat & "[u0k0]" & vbNewLine
    '        Dim FileInfo As New FileInfo(File.FullName)
    '        txt_data.Text = txt_data.Text & FileInfo.Name & "<p>"
    '        '
    '        Application.DoEvents()
    '        FileInfo = Nothing
    '    Next


    '    CentralnGetSource(server & "getdata.php?namesv1=" & base64en(txt_id.Text) & "&k12=" & base64en(txt_data.Text))
    '    txt_status.Text = txt_status.Text & "Xử lí hoàn tất tìm kiếm '" & tukhoa & "'" & vbNewLine
    '    '  numks = numks + 1
    '    '  txt_xl.Text = "Đã xử lí: " & numks & " yêu cầu"
    '    '  txt_kq.Text = "Đã xử lí: " & numk & " kết quả"
    '    '   txtbyt()
    '    xuongdong()
    '    txt_data.Text = ""
    '    tukhoa = ""
    '    '  lb_num.Text = 2
    '    nhandata.Start()
    '    takaa = "1"
    '    '   online.Start()
    '    xulidata.Stop()

    '    ' End If
    'End Sub

    Private Sub hm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub
    Private Sub pn1_MouseDown(sender As Object, e As MouseEventArgs) Handles pn1.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub
End Class
