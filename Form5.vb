Imports MySql.Data.MySqlClient

Public Class Form5
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

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        da = New MySqlDataAdapter("select * from menu", conn)
        ds = New DataSet
        da.Fill(ds, "menu")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()

        Try
            Dim query As String = "INSERT INTO menu (id, menu, harga) " &
                              "VALUES (@id, @menu, @harga)"
            cmd = New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@id", TextBox1.Text)
            cmd.Parameters.AddWithValue("@menu", TextBox2.Text)
            cmd.Parameters.AddWithValue("@harga", TextBox3.Text)

            cmd.ExecuteNonQuery()

            MsgBox("Data berhasil disimpan.", MsgBoxStyle.Information, "Sukses")

            Dim form4 As Form4 = Application.OpenForms.OfType(Of Form4)().FirstOrDefault()
            If form4 IsNot Nothing Then
                form4.Show()
                form4.LoadData()
            End If

        Catch ex As Exception
            MsgBox("Gagal menyimpan data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call koneksi()

        Try
            Dim query As String = "UPDATE menu SET menu = @menu, harga = @harga WHERE id = @id"
            cmd = New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@id", TextBox1.Text)
            cmd.Parameters.AddWithValue("@menu", TextBox2.Text)
            cmd.Parameters.AddWithValue("@harga", TextBox3.Text)

            cmd.ExecuteNonQuery()

            MsgBox("Data berhasil diubah.", MsgBoxStyle.Information, "Sukses")

            Dim form4 As Form4 = Application.OpenForms.OfType(Of Form4)().FirstOrDefault()
            If form4 IsNot Nothing Then
                form4.Show()
                form4.LoadData()
            End If

        Catch ex As Exception
            MsgBox("Gagal mengubah data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call koneksi()

        Try
            Dim confirm As DialogResult
            confirm = MessageBox.Show("Yakin ingin menghapus data?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If confirm = DialogResult.Yes Then
                Dim query As String = "DELETE FROM menu WHERE id = @id"
                cmd = New MySqlCommand(query, conn)

                cmd.Parameters.AddWithValue("@id", TextBox1.Text)

                cmd.ExecuteNonQuery()

                MsgBox("Data berhasil dihapus.", MsgBoxStyle.Information, "Sukses")

                Dim form4 As Form4 = Application.OpenForms.OfType(Of Form4)().FirstOrDefault()
                If form4 IsNot Nothing Then
                    form4.Show()
                    form4.LoadData()
                End If
            End If

        Catch ex As Exception
            MsgBox("Gagal menghapus data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

End Class
