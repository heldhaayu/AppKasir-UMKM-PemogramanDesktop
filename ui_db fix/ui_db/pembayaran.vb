Imports System.Data.OracleClient
Public Class pembayaran
    Dim connection As New OracleConnection
    Dim command As New OracleCommand
    Dim datareader As OracleDataReader

    Dim bayar As String
    Dim total As Integer

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

    Private Sub tb_payment_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub pembayaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
    End Sub

    Private Sub tb_back_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub rd_dana_CheckedChanged(sender As Object, e As EventArgs) Handles rd_dana.CheckedChanged
        bayar = "dana"
        tb_tunai.Text = tb_harga.Text
        pnltunai.Visible = False

    End Sub

    Private Sub rd_tunai_CheckedChanged(sender As Object, e As EventArgs) Handles rd_tunai.CheckedChanged
        bayar = "tunai"
        pnltunai.Visible = True
    End Sub

    Private Sub rd_ovo_CheckedChanged(sender As Object, e As EventArgs) Handles rd_ovo.CheckedChanged
        bayar = "OVO"
        tb_tunai.Text = tb_harga.Text
        pnltunai.Visible = False

    End Sub

    Private Sub rd_shopee_CheckedChanged(sender As Object, e As EventArgs) Handles rd_shopee.CheckedChanged
        bayar = "Shopee Pay"
        tb_tunai.Text = tb_harga.Text
        pnltunai.Visible = False

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

    Private Sub bt_back_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub bt_payment1_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        lv_pembayaran.Items.Clear()
        tb_harga.Text = ""
        tb_nm.Text = ""
        tb_tunai.Text = ""
        tb_nots.Text = ""
        tbkembali.Text = ""
        transaksi.Show()
        Me.Hide()
        transaksi.lv_transaksi.Items.Clear()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        tbkembali.Text = CDbl(tb_tunai.Text) - CDbl(tb_harga.Text)
        command = New OracleCommand("insert into tbpembayaran (nama_customer,kode_transkasi,metode_pembayaran,total_pembayaran) values('" & tb_nm.Text & "','" & tb_nots.Text & "','" & bayar & "','" & tb_harga.Text & "')", connection)
        command.ExecuteNonQuery()
        MsgBox("Pembayaran Sukses " & vbCrLf & "total : " & tb_harga.Text & vbCrLf & " Via : " & bayar & vbCrLf & " kembali : " & tbkembali.Text)
    End Sub

    Private Sub tb_harga_TextChanged(sender As Object, e As EventArgs) Handles tb_harga.TextChanged

    End Sub



    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click
        lv_pembayaran.Items.Clear()
        tb_harga.Text = ""
        tb_nm.Text = ""
        tb_tunai.Text = ""
        tb_nots.Text = ""
        tbkembali.Text = ""
        transaksi.Show()
        Me.Hide()
        transaksi.lv_transaksi.Items.Clear()
    End Sub
End Class