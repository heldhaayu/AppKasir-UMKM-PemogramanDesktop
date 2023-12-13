Imports System.Data.OracleClient
Public Class transaksi
    Dim connection As New OracleConnection
    Dim command As New OracleCommand
    Dim datareader As OracleDataReader

    Dim total As Integer
    Dim i As Integer
    Dim tb As Integer

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
    Sub autonumber()
        Dim nom As String
        nom = ""
        command = New OracleCommand("select case when max(to_number(substr(no_transaksi,3,6))) IS NULL then 1 " & "else max(to_number(substr(no_transaksi,3,6)))+1 end as NO " & "from tbtransaksi", connection)
        datareader = command.ExecuteReader()
        datareader.Read()
        tb_no.Text = datareader.Item(0)

        For i As Integer = 1 To 4 - tb_no.TextLength
            nom = nom & "0"
        Next
        tb_no.Text = "TS" & nom & tb_no.Text
        datareader.Close()
    End Sub
    Sub idbarang()
        Dim nom As String
        nom = ""
        command = New OracleCommand("select case when max(to_number(substr(id_barang,3,6))) IS NULL then 1 " & "else max(to_number(substr(id_barang,3,6)))+1 end as NO " & "from barang", connection)
        datareader = command.ExecuteReader()
        datareader.Read()
        tb_idmenu.Text = datareader.Item(0)

        For i As Integer = 1 To 3 - tb_idmenu.TextLength
            nom = nom & "0"
        Next
        tb_idmenu.Text = "MN" & nom & tb_idmenu.Text
        datareader.Close()
    End Sub
    Private Sub btmenu_Click(sender As Object, e As EventArgs) Handles btmenu.Click
        If sliddingpanel.Width > 40 Then
            Timer1.Enabled = True
        Else
            Timer2.Enabled = True
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If sliddingpanel.Width > 40 Then
            sliddingpanel.Width -= 40
            grupbox1.Left -= 20

        Else
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If sliddingpanel.Width < 200 Then
            sliddingpanel.Width += 40
            grupbox1.Left += 20

        Else
            Timer2.Enabled = False
        End If
    End Sub

    Private Sub sliddingpanel_Paint(sender As Object, e As PaintEventArgs) Handles sliddingpanel.Paint
        If sliddingpanel.Width > 100 Then
            bttransaksi.Text = "TRANSAKSI"
            data_transaksi.Text = "DATA TRANSAKSI"
            laporan_keuangan.Text = "LAPORAN TRANSAKSI"
            datamaster.Text = "DATA MASTER"
            log_out.Text = "LOG OUT"
            picturepanel.Visible = True


        Else
            bttransaksi.Text = ""
            data_transaksi.Text = ""
            laporan_keuangan.Text = ""
            datamaster.Text = ""
            log_out.Text = ""
            picturepanel.Visible = False


        End If
    End Sub

    Private Sub log_out_Click(sender As Object, e As EventArgs) Handles log_out.Click
        Me.Hide()
        login.Show()

    End Sub

    Private Sub bttransaksi_Click(sender As Object, e As EventArgs) Handles bttransaksi.Click

        pntransaksi.Visible = True
        pntentangaplikasi.Visible = False
        pnlaporantransaksi.Visible = False
        'pndatatransaksi.Visible = False
        cbmakanan.Items.Clear()
        cbminuman.Items.Clear()
        Dim mk As String = "MAKANAN"
        command = New OracleCommand("SELECT  * from barang where jenis_barang= '" & mk & "'", connection)
        datareader = command.ExecuteReader()
        While datareader.Read
            cbmakanan.Items.Add(datareader.Item("nama_barang"))
        End While
        datareader.Close()
        command.Dispose()
        Dim MN As String = "MINUMAN"
        command = New OracleCommand("SELECT  * from barang where jenis_barang= '" & MN & "'", connection)
        datareader = command.ExecuteReader()
        While datareader.Read
            cbminuman.Items.Add(datareader.Item("nama_barang"))
        End While
        datareader.Close()
        command.Dispose()
    End Sub

    Private Sub data_transaksi_Click(sender As Object, e As EventArgs) Handles data_transaksi.Click
        pntransaksi.Visible = False
        pntentangaplikasi.Visible = False
        pnlaporantransaksi.Visible = False
        pndatatransaksi.Visible = True

        lv_datatransaksi.Items.Clear()
        command = New OracleCommand("SELECT nama,no_transaksi,barang,jumlah,harga,total from tbtransaksi", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            tb = 1
            While datareader.Read
                lv_datatransaksi.Items.Add(datareader.Item(0))
                lv_datatransaksi.Items(lv_datatransaksi.Items.Count - 1).SubItems.Add(If(datareader.IsDBNull(1), "", datareader.GetString(1)))
                lv_datatransaksi.Items(lv_datatransaksi.Items.Count - 1).SubItems.Add(If(datareader.IsDBNull(2), "", datareader.GetString(2)))
                lv_datatransaksi.Items(lv_datatransaksi.Items.Count - 1).SubItems.Add(datareader.Item(3))
                lv_datatransaksi.Items(lv_datatransaksi.Items.Count - 1).SubItems.Add(datareader.Item(4))
                lv_datatransaksi.Items(lv_datatransaksi.Items.Count - 1).SubItems.Add(datareader.Item(5))
                tb += 1

            End While
            datareader.Close()
        End If
        datareader.Close()
        command.Dispose()

        command = New OracleCommand("SELECT  kode_transkasi,total_pembayaran  from tbpembayaran", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            While datareader.Read()
                lv_header.Items.Add(datareader.Item(0))
                lv_header.Items(lv_header.Items.Count - 1).SubItems.Add(datareader.Item(1))

            End While
        End If
    End Sub

    Private Sub laporan_keuangan_Click(sender As Object, e As EventArgs) Handles laporan_keuangan.Click
        pntransaksi.Visible = False
        pntentangaplikasi.Visible = False
        pnlaporantransaksi.Visible = True
        pndatatransaksi.Visible = False
        command = New OracleCommand("SELECT  kode_transkasi,metode_pembayaran,total_pembayaran  from tbpembayaran", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            While datareader.Read()
                lv_laporan.Items.Add(datareader.Item(0))
                lv_laporan.Items(lv_laporan.Items.Count - 1).SubItems.Add(datareader.Item(1))
                lv_laporan.Items(lv_laporan.Items.Count - 1).SubItems.Add(datareader.Item(2))
            End While
        End If
    End Sub


    Private Sub transaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        koneksi()
        nu_jumlah.Value = 1
        command = New OracleCommand("SELECT * from tbadmin where id_admin='" & login.tbusername.Text & "' and password='" & login.tbpassword.Text & "'", connection)
        datareader = command.ExecuteReader()
        datareader.Read()
        If datareader.HasRows Then
            namaadmin.Text = datareader.Item("nama_admin")

        End If
        login.tbpassword.Text = ""
        login.tbusername.Text = ""
        autonumber()
        idbarang()
        Dim mk As String = "MAKANAN"

        command = New OracleCommand("SELECT  * from barang where jenis_barang= '" & mk & "'", connection)
        datareader = command.ExecuteReader()
        While datareader.Read
            cbmakanan.Items.Add(datareader.Item("nama_barang"))
        End While
        datareader.Close()
        command.Dispose()
        Dim MN As String = "MINUMAN"
        command = New OracleCommand("SELECT  * from barang where jenis_barang= '" & MN & "'", connection)
        datareader = command.ExecuteReader()
        While datareader.Read
            cbminuman.Items.Add(datareader.Item("nama_barang"))
        End While
        datareader.Close()
        command.Dispose()

        lv_datamaster.Items.Clear()
        command = New OracleCommand("SELECT id_barang,nama_barang,harga_barang,jenis_barang from barang", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            tb = 1
            While datareader.Read
                lv_datamaster.Items.Add(tb)
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(0))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(1))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(2))

                tb += 1
            End While
            datareader.Close()
        End If
        datareader.Close()
        command.Dispose()
    End Sub

    Private Sub Guna2CircleButton9_Click(sender As Object, e As EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btminimize_Click(sender As Object, e As EventArgs) Handles btminimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btmaximize_Click(sender As Object, e As EventArgs) Handles btmaximize.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub cbmakanan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbmakanan.SelectedIndexChanged
        command = New OracleCommand("Select * From barang where nama_barang = '" & cbmakanan.Text & "'", connection)
        datareader = command.ExecuteReader
        datareader.Read()
        If datareader.HasRows = True Then
            tb_harga.Text = datareader.Item("harga_barang")
            tbjenis.Text = datareader.Item("jenis_barang")
        Else
            tb_harga.Text = ""
            tbjenis.Text = ""
        End If
    End Sub

    Private Sub cbminuman_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbminuman.SelectedIndexChanged
        command = New OracleCommand("Select * From barang where nama_barang = '" & cbminuman.Text & "'", connection)
        datareader = command.ExecuteReader
        datareader.Read()
        If datareader.HasRows = True Then
            tb_harga.Text = datareader.Item("harga_barang")
            tbjenis.Text = datareader.Item("jenis_barang")
        Else
            tb_harga.Text = ""
            tbjenis.Text = ""
        End If
    End Sub

    Private Sub datamaster_Click(sender As Object, e As EventArgs) Handles datamaster.Click
        pntransaksi.Visible = False
        pntentangaplikasi.Visible = True
        pnlaporantransaksi.Visible = False
        pndatatransaksi.Visible = False
        lv_datamaster.Items.Clear()
        command = New OracleCommand("SELECT id_barang,nama_barang,harga_barang,jenis_barang from barang", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            tb = 1

            While datareader.Read
                lv_datamaster.Items.Add(tb)
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(0))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(1))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(2))


                tb += 1
            End While
            datareader.Close()
        End If
        datareader.Close()
        command.Dispose()
    End Sub

    Private Sub btsignin_Click(sender As Object, e As EventArgs) Handles cetak.Click
        rekap.Show()
        Me.Hide()
    End Sub

    Private Sub bt_add_Click_1(sender As Object, e As EventArgs) Handles bt_add.Click
        Dim makanan As String
        Dim minuman As String
        makanan = cbmakanan.Text
        minuman = cbminuman.Text
        If tbjenis.Text = "MAKANAN" Then
            total = tb_harga.Text * nu_jumlah.Value
            command = New OracleCommand("insert into tbtransaksi (no_transaksi,tanggal,jumlah,barang,admin,harga,nama,total) values('" & tb_no.Text & "','" & dt_tanggal.Text & "','" & nu_jumlah.Value & "','" & makanan & "','" & namaadmin.Text & "','" & tb_harga.Text & "','" & tb_customer.Text & "','" & total & "')", connection)
            command.ExecuteNonQuery()
            MsgBox("Menu Sukses di tambah")
            tbjenis.Text = ""
            tb_harga.Text = ""
            nu_jumlah.Value = 1
            total = 0
        ElseIf tbjenis.Text = "MINUMAN" Then
            total = tb_harga.Text * nu_jumlah.Value
            command = New OracleCommand("insert into tbtransaksi (no_transaksi,tanggal,jumlah,barang,admin,harga,nama,total) values('" & tb_no.Text & "','" & dt_tanggal.Text & "','" & nu_jumlah.Value & "','" & minuman & "','" & namaadmin.Text & "','" & tb_harga.Text & "','" & tb_customer.Text & "','" & total & "')", connection)
            command.ExecuteNonQuery()
            MsgBox("Menu Sukses di tambah")
            tbjenis.Text = ""
            tb_harga.Text = ""
            nu_jumlah.Value = 1
            total = 0
        End If


        lv_transaksi.Items.Clear()
        command = New OracleCommand("SELECT  barang,no_transaksi, admin, tanggal, jumlah,harga,nama  ,total from tbtransaksi where no_transaksi='" & tb_no.Text & "'", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            tb = 1
            While datareader.Read

                lv_transaksi.Items.Add(tb)
                lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(If(datareader.IsDBNull(0), "", datareader.GetString(0)))
                lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(4))
                lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(5))
                lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(7))
                tb += 1
            End While
            datareader.Close()
        End If
        datareader.Close()
        command.Dispose()

        For i As Integer = 0 To lv_transaksi.Items.Count - 1
            total += CInt(lv_transaksi.Items(i).SubItems(3).Text)
        Next
        pembayaran.tb_harga.Text = total
        cbmakanan.Text = ""
        cbminuman.Text = ""
    End Sub

    Private Sub Guna2Button1_Click_1(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim makanan As String
        Dim minuman As String
        makanan = cbmakanan.Text
        minuman = cbminuman.Text
        If tbjenis.Text = "MAKANAN" Then
            command = New OracleCommand("update tbtransaksi set barang='" & makanan & "',harga='" & tb_harga.Text & "',jumlah=" & nu_jumlah.Value & " where no_transaksi='" & tb_no.Text & "' and barang='" & makanan & "'", connection)
            command.ExecuteNonQuery()
            MsgBox("Menu Sukses di Update")
            lv_transaksi.Items.Clear()
            command = New OracleCommand("SELECT  barang,no_transaksi, admin, tanggal, jumlah,harga,nama  ,total from tbtransaksi where no_transaksi='" & tb_no.Text & "'", connection)
            datareader = command.ExecuteReader()
            If datareader.HasRows Then
                tb = 1
                While datareader.Read

                    lv_transaksi.Items.Add(tb)
                    lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(If(datareader.IsDBNull(0), "", datareader.GetString(0)))
                    lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(4))
                    lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(5))
                    lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(7))
                    tb += 1
                End While
                datareader.Close()
            End If
            datareader.Close()
            command.Dispose()

            For i As Integer = 0 To lv_transaksi.Items.Count - 1
                total += CInt(lv_transaksi.Items(i).SubItems(3).Text)
            Next
            pembayaran.tb_harga.Text = total
            cbmakanan.Text = ""
            cbminuman.Text = ""

        ElseIf tbjenis.Text = "MINUMAN" Then
            command = New OracleCommand("update tbtransaksi set barang='" & minuman & "',harga='" & tb_harga.Text & "',jumlah=" & nu_jumlah.Value & " where no_transaksi='" & tb_no.Text & "' and barang='" & minuman & "'", connection)
            command.ExecuteNonQuery()
            MsgBox("Menu Sukses di Update")
            If datareader.HasRows Then
                tb = 1
                While datareader.Read

                    lv_transaksi.Items.Add(tb)
                    lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(If(datareader.IsDBNull(0), "", datareader.GetString(0)))
                    lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(4))
                    lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(5))
                    lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(7))
                    tb += 1
                End While
                datareader.Close()
            End If
            datareader.Close()
            command.Dispose()

            For i As Integer = 0 To lv_transaksi.Items.Count - 1
                total += CInt(lv_transaksi.Items(i).SubItems(3).Text)
            Next
            pembayaran.tb_harga.Text = total
            cbmakanan.Text = ""
            cbminuman.Text = ""
        End If

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles btdelete.Click
        If tbjenis.Text = "MAKANAN" Then
            command = New OracleCommand("delete tbtransaksi where no_transaksi='" & tb_no.Text & "' and barang='" & cbmakanan.Text & "'", connection)
            command.ExecuteNonQuery()
            MsgBox("Pesanan Terhapus")
        ElseIf tbjenis.Text = "MINUMAN" Then
            command = New OracleCommand("delete tbtransaksi where no_transaksi='" & tb_no.Text & "' and barang='" & cbminuman.Text & "'", connection)
            command.ExecuteNonQuery()
            MsgBox("Pesanan Terhapus")
        End If

        lv_transaksi.Items.Clear()
        command = New OracleCommand("SELECT  barang,no_transaksi, admin, tanggal, jumlah,harga,nama  ,total from tbtransaksi where no_transaksi='" & tb_no.Text & "'", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            tb = 1
            While datareader.Read

                lv_transaksi.Items.Add(tb)
                lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(If(datareader.IsDBNull(0), "", datareader.GetString(0)))
                lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(4))
                lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(5))
                lv_transaksi.Items(lv_transaksi.Items.Count - 1).SubItems.Add(datareader.Item(7))
                tb += 1
            End While
            datareader.Close()
        End If
        datareader.Close()
        command.Dispose()

        For i As Integer = 0 To lv_transaksi.Items.Count - 1
            total -= CInt(lv_transaksi.Items(i).SubItems(3).Text)
        Next
        pembayaran.tb_harga.Text = total
        cbmakanan.Text = ""
        cbminuman.Text = ""
        tbjenis.Text = ""
        tb_harga.Text = ""
        nu_jumlah.Value = 0
    End Sub

    Private Sub btpay_Click(sender As Object, e As EventArgs) Handles btpay.Click
        pembayaran.lv_pembayaran.Items.Clear()
        command = New OracleCommand("SELECT  barang,no_transaksi, admin, tanggal, jumlah,harga,nama  ,total from tbtransaksi where no_transaksi='" & tb_no.Text & "'", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then

            While datareader.Read()
                pembayaran.tb_nm.Text = datareader.Item("nama")
                pembayaran.tb_nots.Text = datareader.Item("no_transaksi")
                pembayaran.lv_pembayaran.Items.Add(datareader.Item(0))
                pembayaran.lv_pembayaran.Items(pembayaran.lv_pembayaran.Items.Count - 1).SubItems.Add(datareader.Item(5))
                pembayaran.lv_pembayaran.Items(pembayaran.lv_pembayaran.Items.Count - 1).SubItems.Add(datareader.Item(4))
                pembayaran.lv_pembayaran.Items(pembayaran.lv_pembayaran.Items.Count - 1).SubItems.Add(datareader.Item(7))
            End While
            datareader.Close()
        End If
        datareader.Close()
        command.Dispose()

        For i As Integer = 0 To pembayaran.lv_pembayaran.Items.Count - 1
            total += CInt(pembayaran.lv_pembayaran.Items(i).SubItems(3).Text)
        Next
        pembayaran.tb_harga.Text = total
        pembayaran.Show()
        Me.Hide()
    End Sub

    Private Sub Addmenu_Click(sender As Object, e As EventArgs) Handles Addmenu.Click
        command = New OracleCommand("insert into barang (id_barang,nama_barang,harga_barang,jenis_barang) values('" & tb_idmenu.Text & "','" & tb_menu.Text & "','" & tbharga.Text & "','" & tbjenismenu.Text & "')", connection)
        command.ExecuteNonQuery()
        MsgBox("Menu Sukses di tambah")

        lv_datamaster.Items.Clear()
        command = New OracleCommand("SELECT id_barang,nama_barang,harga_barang,jenis_barang from barang", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            tb = 1
            While datareader.Read
                lv_datamaster.Items.Add(tb)
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(0))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(1))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(2))

                tb += 1
            End While
            datareader.Close()
        End If
        datareader.Close()
        command.Dispose()
        tb_idmenu.Text = ""
        tbjenismenu.Text = ""
        tbharga.Text = ""
        tbjenismenu.Text = ""
    End Sub

    Private Sub Updatemenu_Click(sender As Object, e As EventArgs) Handles Updatemenu.Click
        command = New OracleCommand("update barang set nama_barang='" & tb_menu.Text & "',harga_barang='" & tbharga.Text & "',jenis_barang='" & tbjenismenu.Text & "' where id_barang='" & tb_idmenu.Text & "'", connection)
        command.ExecuteNonQuery()

        MsgBox("Menu Sukses di Update")
        lv_datamaster.Items.Clear()
        command = New OracleCommand("SELECT id_barang,nama_barang,harga_barang,jenis_barang from barang", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            tb = 1
            While datareader.Read
                lv_datamaster.Items.Add(tb)
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(0))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(1))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(2))

                tb += 1
            End While
            datareader.Close()
        End If
        datareader.Close()
        command.Dispose()
        tb_idmenu.Text = ""
        tbjenismenu.Text = ""
        tbharga.Text = ""
        tbjenismenu.Text = ""
        tb_menu.Text = ""
    End Sub

    Private Sub Guna2Button2_Click_1(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        command = New OracleCommand("delete barang where id_barang='" & tb_idmenu.Text & "'", connection)
        command.ExecuteNonQuery()
        MsgBox("Menu Terhapus")
        tb_idmenu.Text = ""
        tbjenismenu.Text = ""
        tbharga.Text = ""
        tbjenismenu.Text = ""
        lv_datamaster.Items.Clear()
        command = New OracleCommand("SELECT id_barang,nama_barang,harga_barang,jenis_barang from barang", connection)
        datareader = command.ExecuteReader()
        If datareader.HasRows Then
            tb = 1
            While datareader.Read
                lv_datamaster.Items.Add(tb)
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(0))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(1))
                lv_datamaster.Items(lv_datamaster.Items.Count - 1).SubItems.Add(datareader.Item(2))

                tb += 1
            End While
            datareader.Close()
        End If
        datareader.Close()
        command.Dispose()
    End Sub

    Private Sub Guna2CircleButton12_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton12.Click
        Me.Close()
    End Sub

    Private Sub Guna2CircleButton4_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton4.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Guna2CircleButton10_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton10.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Guna2CircleButton11_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton11.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Guna2CircleButton6_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton6.Click
        Me.Close()
    End Sub

    Private Sub Guna2CircleButton5_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton5.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Guna2CircleButton3_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton3.Click
        Me.Close()
    End Sub

    Private Sub Guna2CircleButton1_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton1.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Guna2CircleButton2_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton2.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        laporan.Show()
        Me.Hide()

    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles dt_tanggal.TextChanged

        dt_tanggal.Text = DateTime.Now.ToString("dd - MM - yyyy")
    End Sub
End Class