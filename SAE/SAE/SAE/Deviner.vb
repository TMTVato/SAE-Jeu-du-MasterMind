Public Class Deviner
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress
        'on récupére depuis le module les caractères jouables 
        Dim caracJouable As String() = {getCarac(0), getCarac(1), getCarac(2), getCarac(3), getCarac(4)}
        If Not Char.IsControl(e.KeyChar) AndAlso Not caracJouable.Contains(e.KeyChar.ToString()) Then
            'si on appuie sur un caractère non jouable et si la touche appuyée n'est pas "effacer"  ne fait rien
            e.Handled = True
        End If
        sender.backcolor = Color.White
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim textBoxes As TextBox() = {TextBox1, TextBox2, TextBox3, TextBox4, TextBox5}
        For Each textBox As TextBox In textBoxes
            If textBox.Text = "" Then
                textBox.BackColor = Color.Red
            End If
        Next
        If TextBox5.BackColor = Color.White And TextBox4.BackColor = Color.White And TextBox3.BackColor = Color.White And TextBox2.BackColor = Color.White And TextBox1.BackColor = Color.White Then

            For i As Integer = 0 To Panel1.Controls.Count - 1
                Dim elem As Control = Panel1.Controls(i)
                SetFind(i, elem.Text)
            Next
            jeu.Show()
            Me.Close()


        End If
    End Sub


    Private Sub Deviner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = getCarac(0) & getCarac(1) & getCarac(2) & getCarac(3) & getCarac(4) 'ajoute au label les caractères jouables
    End Sub


End Class