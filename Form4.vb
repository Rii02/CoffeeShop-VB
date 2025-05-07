Imports MySql.Data.MySqlClient

Public Class Form4
    Private mainForm As Form1

    Public Sub New(form As Form1)
        InitializeComponent()
        mainForm = form
    End Sub

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

    Public Sub LoadData()
        Call koneksi()

        Try
            conn.Open()

            Dim query As String = "SELECT * FROM menu"
            da = New MySqlDataAdapter(query, conn)
            ds = New DataSet()

            da.Fill(ds, "menu")

            DataGridView1.DataSource = ds.Tables("menu")
            conn.Close()
        Catch ex As Exception
            MsgBox("Gagal memuat data: " & ex.Message)
        End Try
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim nama As String = row.Cells("menu").Value.ToString()
            Dim harga As String = row.Cells("harga").Value.ToString()
            mainForm.SetMenu(nama, harga)
            Me.Close()
        End If
    End Sub
End Class
