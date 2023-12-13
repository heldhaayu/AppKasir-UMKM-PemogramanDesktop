Imports System.Data.OracleClient
Public Class login
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

    Private Sub btsignin_Click(sender As Object, e As EventArgs) Handles btsignin.Click
        command = New OracleCommand("SELECT * from tbadmin where id_admin='" & tbusername.Text & "' and password='" & tbpassword.Text & "'", connection)
        datareader = command.ExecuteReader()
        datareader.Read()
        If datareader.HasRows Then

            transaksi.Show()
            Me.Hide()
        Else
            MsgBox("Username OR Password ERROR")
        End If
        datareader.Close()
        command.Dispose()
    End Sub

    Private Sub linksignup_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linksignup.LinkClicked
        tbpassword.Text = ""
        tbusername.Text = ""
        registrasi.Show()
        Me.Hide()
    End Sub

    Private Sub showpassword_CheckedChanged(sender As Object, e As EventArgs) Handles showpassword.CheckedChanged
        If showpassword.Checked = True Then
            tbpassword.PasswordChar = ""
        Else
            tbpassword.PasswordChar = "x"
        End If
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        tbpassword.PasswordChar = "x"
    End Sub

    Private Sub btclose_Click(sender As Object, e As EventArgs) Handles btclose.Click
        Me.Close()

    End Sub

    Private Sub btmaximize_Click(sender As Object, e As EventArgs) Handles btmaximize.Click
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
