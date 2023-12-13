Imports System.Data.OracleClient
Public Class laporan
    Dim connection As New OracleConnection
    Dim command As New OracleCommand
    Dim datareader As OracleDataReader
    Dim dataadapter As OracleDataAdapter
    Dim Table As New DataTable

    Sub Viewdata()
        command = New OracleCommand("select * from tbtransaksi", connection)
        dataadapter = New OracleDataAdapter(command)
        dataadapter.Fill(Table)
        connection.Close()
        connection.Dispose()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CrystalReportViewer1_Load(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Load
        Table.Clear()
        connection = New OracleConnection("data source = (DESCRIPTION =
                                (ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-939PCOS)(PORT = 1521))
                                (CONNECT_DATA =
                                (SERVER = DEDICATED)
                                (SERVICE_NAME = XE)
                                )
                                ); user id = cipung; password = cipung")
        Dim laporan As New cetak_laporan
        Viewdata()
        laporan.Database.Tables("tbtransaksi").SetDataSource(Table)
        CrystalReportViewer1.ReportSource = Nothing
        CrystalReportViewer1.ReportSource = laporan
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        transaksi.Show()
        Me.Hide()

    End Sub
End Class