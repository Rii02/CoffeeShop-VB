Public Class Form6

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form1 As New Form1
        form1.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form2 As New Form2
        form2.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim mainForm As Form1 = Application.OpenForms.OfType(Of Form1)().FirstOrDefault()
        Dim form4 As Form4 = Application.OpenForms.OfType(Of Form4)().FirstOrDefault()

        If form4 Is Nothing Then
            If mainForm IsNot Nothing Then
                form4 = New Form4(mainForm)
                form4.Show()
            Else
                MsgBox("Form1 tidak ditemukan. Harap buka Form1 terlebih dahulu.")
            End If
        Else
            form4.BringToFront()
        End If

        If form4 IsNot Nothing Then
            form4.LoadData()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim form5 As New Form5
        form5.Show()
    End Sub
End Class