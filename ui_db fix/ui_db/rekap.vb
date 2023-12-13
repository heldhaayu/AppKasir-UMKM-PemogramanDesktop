Imports System.Data.OracleClient
Public Class rekap
    Dim connection As New OracleConnection
    Dim command As New OracleCommand
    Dim datareader As OracleDataReader
    Dim dataadapter As OracleDataAdapter
    Dim Table As New DataTable

    Sub Viewdata()
        command = New OracleCommand("select * from tbpembayaran", connection)
        dataadapter = New OracleDataAdapter(command)
        dataadapter.Fill(Table)
        connection.Close()
        connection.Dispose()
    End Sub
    Private Sub CrystalReportViewer1_Load(sender As Object, e As EventArgs) Handles CrystalReportViewer2.Load
        Table.Clear()
        connection = New OracleConnection("data source = (DESCRIPTION =
                                (ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-939PCOS)(PORT = 1521))
                                (CONNECT_DATA =
                                (SERVER = DEDICATED)
                                (SERVICE_NAME = XE)
                                )
                                ); user id = cipung; password = cipung")
        Dim laporan As New rekap_keuangan
        Viewdata()
        laporan.Database.Tables("tbpembayaran").SetDataSource(Table)
        CrystalReportViewer2.ReportSource = Nothing
        CrystalReportViewer2.ReportSource = laporan
    End Sub

    Private Sub rekap_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CrystalReportViewer2_Load(sender As Object, e As EventArgs) Handles CrystalReportViewer2.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        transaksi.Show()
        Me.Hide()
    End Sub
End Class