Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json.Linq
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks

Public Class home
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function
    Dim server As String = "https://apps.sunstudiodev.com/fff/"
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim strHostName As String = System.Net.Dns.GetHostName()
    Dim ip_ketnoi = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()

    Dim result_get As String = ""
    Dim still_connect As Boolean = True
    Dim still_disconnect As Boolean = True
    Dim still_online As Boolean = True
    Private Sub home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' antiSandboxie()
    End Sub

    Private Sub end_S_Click(sender As Object, e As EventArgs) Handles end_S.Click
        End
    End Sub

    Private Sub end_S_MouseMove(sender As Object, e As MouseEventArgs) Handles end_S.MouseMove
        end_S.BackColor = Color.Gainsboro

    End Sub

    Private Sub mini_S_Click(sender As Object, e As EventArgs) Handles mini_S.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub mini_S_MouseMove(sender As Object, e As MouseEventArgs) Handles mini_S.MouseMove
        mini_S.BackColor = Color.Gainsboro
    End Sub



    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        end_S.BackColor = Color.White
        mini_S.BackColor = Color.White
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub bt_go_Click(sender As Object, e As EventArgs) Handles bt_go.Click
        ' ketnoi2.Start()
        checkConnect()
        ' TextBox1.Text = server & "banketnoi.php?address=" & base64en(txt_id.Text) & "&pass=" & base64en(txt_pass.Text) & "&pc_name=" & base64en(Environment.MachineName)
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
    'Public Function getUrlSource(url As String) As String
    '    Try
    '        Dim Request As HttpWebRequest = HttpWebRequest.Create(url)
    '        Dim Response As HttpWebResponse = Request.GetResponse()
    '        Dim reader As StreamReader = New StreamReader(Response.GetResponseStream)
    '        Dim httpContent As String
    '        httpContent = reader.ReadToEnd
    '        Return httpContent
    '    Catch
    '        Return "error"
    '    End Try
    'End Function

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

    Async Function checkConnect() As Task
        Using client As New HttpClient()
            If still_connect Then
                Dim getStringTask As Task(Of String) =
                client.GetStringAsync(server & "banketnoi.php?address=" & base64en(txt_id.Text) & "&pass=" & base64en(txt_pass.Text) & "&pc_name=" & base64en(Environment.MachineName))
                'If lb_num.Text = 0 Then
                Dim pcname As String = Environment.MachineName
                If txt_id.TextLength > 10 And txt_pass.TextLength >= 5 Then
                    lb_st.Text = "Đang chuẩn bị kết nối"
                    Dim ketnoi As String = Await getStringTask
                    If ketnoi = "fail" Then
                        lb_st.Text = ("ID hoặc Pass không đúng!")
                    Else
                        txt_id.Enabled = False
                        txt_pass.Enabled = False
                        bt_go.Enabled = False
                        Dim getStringTask2 As Task(Of String) =
                client.GetStringAsync(server & "sts_oft_of_client.php?idk=" & base64en(txt_id.Text))

                        Dim sts123 As String = Await getStringTask2
                        If sts123 = "on" Then

                            lb_st.Text = "Đang chờ máy bạn..."
                            Dim getStringTask3 As Task(Of String) =
                client.GetStringAsync(server & "bankt_wait.php?idk=" & base64en(txt_id.Text))
                            Dim sk1 As String = Await getStringTask3
                            If sk1 = "chophep" & pcname Then
                                TabControl1.SelectedTab = tp2
                                lb_st.Text = "Đã kết nối đến " & txt_id.Text
                                checkDisconnect()
                                still_connect = False
                            End If

                        ElseIf sts123 = "off" Then
                            lb_st.Text = "Máy chủ này hiện không hoạt động!!"
                        Else
                            lb_st.Text = "Máy chủ này không tồn tại!!" & sts123
                            '  TextBox1.Text = sts123
                        End If
                    End If
                Else
                    MsgBox("Không đúng định dạng ID&Pass!", vbInformation, "Lời nhắn nhỏ")
                    txt_id.Enabled = True
                    txt_pass.Enabled = True
                    bt_go.Enabled = True
                End If
                '   checkConnect()
            End If
        End Using
    End Function

    Async Function checkDisconnect() As Task
        Using client As New HttpClient()
            If still_disconnect Then
                Dim getStringTask As Task(Of String) =
                client.GetStringAsync(server & "sts_oft_of_client.php?idk=" & base64en(txt_id.Text))
                Dim sts123 As String = Await getStringTask
                checkOnline()

                If sts123 = "on" Then
                    lb_num.Text = 1
                ElseIf sts123 = "off" Then
                    txt_status.Clear()
                    TabControl1.SelectedTab = tp1
                    txt_id.Enabled = True
                    txt_pass.Enabled = True
                    bt_go.Enabled = True
                    lb_st.Text = "Máy bạn đã ngắt kết nối"
                    Dim ngatkn As String = CentralnGetSource(server & "bankt_wait.php?ngatdi=" & base64en(txt_id.Text))
                    '   lb_num.Text = 0
                    checkConnect()
                    still_disconnect = False
                End If
                checkDisconnect()
            End If
        End Using
    End Function

    Async Function checkOnline() As Task
        Using client As New HttpClient()
            If still_online Then
                Dim getStringTask As Task(Of String) =
                client.GetStringAsync(server & "timeonof.php?id=" & base64en(txt_id.Text))
                'If lb_num.Text = 1 Then
                Dim sts1234 As String = Await getStringTask

                Dim getStringTask2 As Task(Of String) =
                client.GetStringAsync(server & "timeonof.php?s2=" & base64en(txt_id.Text))
                Dim cl_s1 As String = Await getStringTask2
                If sts1234.Length >= 4 Then
                    lb_st.Text = "Máy bạn đã offline!"
                    txt_status.Clear()
                    TabControl1.SelectedTab = tp1
                    txt_id.Enabled = True
                    txt_pass.Enabled = True
                    bt_go.Enabled = True

                    Dim getStringTask3 As Task(Of String) =
                client.GetStringAsync(server & "bankt_wait.php?ngatdi=" & base64en(txt_id.Text))

                    Dim getStringTask4 As Task(Of String) =
                client.GetStringAsync(server & "del_kochay.php?ipk=" & base64en(ip_ketnoi))
                    Dim ngatkn As String = Await getStringTask3
                    Dim xoaclient As String = Await getStringTask4
                    '   lb_num.Text = 2
                    still_disconnect = False
                    still_online = False
                End If
                '   End If
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



    Private Sub bt_stop_Click(sender As Object, e As EventArgs) Handles bt_stop.Click
        TabControl1.SelectedTab = tp1
        txt_id.Enabled = True
        txt_pass.Enabled = True
        bt_go.Enabled = True
        Dim ngatkn As String = CentralnGetSource(server & "bankt_wait.php?ngatdi=" & base64en(txt_id.Text))
        Dim xoaclient As String = CentralnGetSource(server & "del_kochay.php?ipk=" & base64en(ip_ketnoi))

        lb_st.Text = "Đã huỷ phiên!"

    End Sub

    Private Sub home_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub home_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        drag = False

    End Sub

    Private Sub home_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub



    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        drag = False
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub





    Private Sub bt_find_Click(sender As Object, e As EventArgs) Handles bt_find.Click
        Dim timkiem As String = CentralnGetSource(server & "metoo.php?q=" & txt_find.Text)
        txt_find.Clear()
        bt_find.Enabled = False
    End Sub


End Class
