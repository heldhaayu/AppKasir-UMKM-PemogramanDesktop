Imports System.Data.OracleClient
Public Class registrasi
    Dim connection As New OracleConnection
    Dim command As New OracleCommand
    Dim datareader As OracleDataReader

    Sub koneksi()
        connection.Close()
        connection = New OracleConnection("data source = (DESCRIPTION =
                                (ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-939PCOS)(PORT = 1521))
                                (CONNECT_DATA =
                                (SERVER = DEDICATED)
                                (SERVICE_NAME = XE)
                                )
                                ); user id = cipung; password = cipung")

        connection.Open()
    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles btsignup.Click
        If tbpassword.Text = tbconfirm.Text Then

            command = New OracleCommand("insert into tbadmin (id_admin,nama_admin,password) values('" & tbusername.Text & "','" & tbname.Text & "','" & tbpassword.Text & "')", connection)
            command.ExecuteNonQuery()
            MsgBox("REGISTRASI SUKSES")
            login.Show()
            Me.Hide()
        Else
            MsgBox(" PASSWORD DAN KONFIRMASI PASSWORD SALAH")

        End If
        tbpassword.Text = ""
        tbusername.Text = ""
        tbname.Text = ""
        tbconfirm.Text = ""
    End Sub

    Private Sub registrasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
    End Sub

    Private Sub close_Click(sender As Object, e As EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btmaximixe_Click(sender As Object, e As EventArgs) Handles btmaximixe.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub btminimize_Click(sender As Object, e As EventArgs) Handles btminimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class