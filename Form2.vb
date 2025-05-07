Imports MySql.Data.MySqlClient

Public Class Form2
    Public conn As New MySqlConnection
    Public da As MySqlDataAdapter
    Public ds As DataSet

    Public Sub koneksi()
        Dim strconn As String = "server=localhost;user=root;password=;database=coffeeshop"
        conn.ConnectionString = strconn
        Try
            conn.Open()
            conn.Close()
        Catch ex As Exception
            MsgBox("Koneksi Gagal: " & ex.Message)
        End Try
    End Sub

    Public Sub LoadData(Optional filter As String = "")
        Call koneksi()

        Try
            conn.Open()

            Dim query As String = "SELECT 
            dm.id_data AS 'ID Transaksi',
            d.nama AS 'Nama Pelanggan',
            d.tanggal AS 'Tanggal Transaksi',
            m.menu AS 'Nama Menu',
            m.harga AS 'Harga Menu',
            d.jumlah_beli AS 'Jumlah Beli',
            d.total_biaya AS 'Total Harga',
            d.bayar AS 'Uang Bayar',
            d.kembali AS 'Kembalian'
        FROM 
            data_menu dm
        JOIN 
            data d ON dm.id_data = d.id
        JOIN 
            menu m ON dm.id_menu = m.id"

            If Not String.IsNullOrEmpty(filter) Then
                query &= " WHERE " & filter
            End If

            query &= " ORDER BY d.tanggal DESC"

            da = New MySqlDataAdapter(query, conn)
            ds = New DataSet()

            da.Fill(ds, "data")

            DataGridView1.DataSource = ds.Tables("data")
            conn.Close()
        Catch ex As Exception
            MsgBox("Gagal memuat data: " & ex.Message)
        End Try
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ds As New DataSet1
        Dim dt As New DataTable
        dt = ds.Tables("data")
        For i = 0 To DataGridView1.Rows.Count - 1
            dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value,
                        DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value,
                        DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value,
                        DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value)

        Next
        With Form3.ReportViewer1.LocalReport
            .ReportPath = "D:\Kuliah\SMT 3\Pemrograman Visual\project\Report1.rdlc"
            .DataSources.Clear()
            .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt))
        End With
        Form3.Show()
        Form3.ReportViewer1.RefreshReport()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim filter As String = ""

        If CheckBox1.Checked Then
            filter &= $"d.tanggal = '{DateTimePicker1.Value.ToString("yyyy-MM-dd")}'"
        End If

        If CheckBox2.Checked Then
            Dim bulan As Integer = ComboBox1.SelectedIndex + 1
            If filter <> "" Then filter &= " AND "
            filter &= $"MONTH(d.tanggal) = {bulan}"
        End If

        If CheckBox3.Checked Then
            If filter <> "" Then filter &= " AND "
            filter &= $"YEAR(d.tanggal) = {TextBox1.Text}"
        End If

        LoadData(filter)
    End Sub

End Class