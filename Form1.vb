Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Form1
    Public conn As New MySqlConnection
    Public da As MySqlDataAdapter
    Public ds As DataSet
    Public cmd As MySqlCommand
    Public dr As MySqlDataReader

    Public Sub koneksi()
        Dim strconn As String
        Try
            strconn = "server=localhost;user=root;password=;database=coffeeshop"
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.ConnectionString = strconn
            conn.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        da = New MySqlDataAdapter("select * from data", conn)
        ds = New DataSet
        da.Fill(ds, "data")

        If DataGridView1.Columns.Count = 0 Then
            DataGridView1.Columns.Add("Menu", "Menu")
            DataGridView1.Columns.Add("Harga", "Harga")
            DataGridView1.Columns.Add("Jumlah", "Jumlah")
            DataGridView1.Columns.Add("Total_Harga", "Total Harga")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form4 As New Form4(Me)
        form4.Show()
    End Sub

    Public Sub SetMenu(nama As String, harga As String)
        Dim jumlahBeli As Integer
        If Integer.TryParse(TextBox4.Text, jumlahBeli) Then
            Dim hargaTotal As Decimal = jumlahBeli * Convert.ToDecimal(harga)

            DataGridView1.Rows.Add(nama, harga, jumlahBeli, hargaTotal.ToString("N0"))

            RichTextBox1.AppendText(nama & vbCrLf)
            RichTextBox2.AppendText("Rp. " & hargaTotal.ToString("N0") & vbCrLf)
            TextBox6.AppendText(jumlahBeli.ToString() & ",")
            HitungTotalBiaya()
        Else
            MsgBox("Masukkan jumlah item yang valid.")
        End If
    End Sub



    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        HitungTotalBiaya()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If Not TextBox3.Text.StartsWith("Rp. ") Then
            TextBox3.Text = "Rp. " & TextBox3.Text
            TextBox3.SelectionStart = TextBox3.Text.Length
        End If
        HitungTotalKembalian()
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        HitungTotalKembalian()
    End Sub

    Private Sub HitungTotalBiaya()
        Dim totalBiaya As Decimal = 0

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                Dim hargaTotalText As String = row.Cells("Total_Harga").Value?.ToString().Replace("Rp. ", "")
                Dim hargaTotal As Decimal
                If Decimal.TryParse(hargaTotalText, hargaTotal) Then
                    totalBiaya += hargaTotal
                End If
            End If
        Next

        TextBox5.Text = "Rp. " & totalBiaya.ToString("N0")

        HitungTotalKembalian()
    End Sub

    Private Sub HitungTotalKembalian()
        Dim uangPembayaran As Decimal
        Dim totalBiaya As Decimal
        Dim totalKembalian As Decimal

        Dim totalBiayaText As String = TextBox5.Text.Replace("Rp. ", "")
        Dim uangPembayaranText As String = TextBox3.Text.Replace("Rp. ", "")

        If Decimal.TryParse(uangPembayaranText, uangPembayaran) AndAlso Decimal.TryParse(totalBiayaText, totalBiaya) Then
            totalKembalian = uangPembayaran - totalBiaya
            TextBox8.Text = "Rp. " & totalKembalian.ToString("N0")
        Else
            TextBox8.Text = "Rp. 0"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()

        Try

            Dim query As String = "INSERT INTO data (nama, tanggal, total_biaya, jumlah_beli, bayar, kembali) " &
                              "VALUES (@nama, @tanggal, @total_biaya, @jumlah_beli, @bayar, @kembali)"
            cmd = New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@nama", TextBox2.Text)
            cmd.Parameters.AddWithValue("@tanggal", DateTimePicker1.Value)
            cmd.Parameters.AddWithValue("@total_biaya", TextBox5.Text.Replace("Rp. ", "").Replace(",", ""))
            cmd.Parameters.AddWithValue("@jumlah_beli", TextBox6.Text)
            cmd.Parameters.AddWithValue("@bayar", TextBox3.Text.Replace("Rp. ", "").Replace(",", ""))
            cmd.Parameters.AddWithValue("@kembali", TextBox8.Text.Replace("Rp. ", "").Replace(",", ""))

            cmd.ExecuteNonQuery()

            Dim idTransaksi As Integer
            cmd = New MySqlCommand("SELECT LAST_INSERT_ID()", conn)
            idTransaksi = Convert.ToInt32(cmd.ExecuteScalar())

            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    Dim idMenu As Integer = GetMenuId(row.Cells("Menu").Value.ToString())

                    If idMenu > 0 Then

                        Dim queryCek As String = "SELECT COUNT(*) FROM data_menu WHERE id_data = @id_data AND id_menu = @id_menu"
                        cmd = New MySqlCommand(queryCek, conn)
                        cmd.Parameters.AddWithValue("@id_data", idTransaksi)
                        cmd.Parameters.AddWithValue("@id_menu", idMenu)

                        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                        If count = 0 Then

                            Dim queryRelasi As String = "INSERT INTO data_menu (id_data, id_menu) VALUES (@id_data, @id_menu)"
                            cmd = New MySqlCommand(queryRelasi, conn)
                            cmd.Parameters.AddWithValue("@id_data", idTransaksi)
                            cmd.Parameters.AddWithValue("@id_menu", idMenu)
                            cmd.ExecuteNonQuery()
                        End If
                    Else
                        MsgBox("Menu tidak ditemukan: " & row.Cells("Menu").Value.ToString())
                    End If
                End If
            Next

            MsgBox("Data berhasil disimpan.", MsgBoxStyle.Information, "Sukses")

        Catch ex As Exception
            MsgBox("Gagal menyimpan data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GetMenuId(menuName As String) As Integer
        Dim idMenu As Integer = 0
        Dim query As String = "SELECT id FROM menu WHERE menu = @menu"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@menu", menuName)
        Dim result = cmd.ExecuteScalar()

        If result IsNot Nothing Then
            idMenu = Convert.ToInt32(result)
        End If

        Return idMenu
    End Function



End Class
