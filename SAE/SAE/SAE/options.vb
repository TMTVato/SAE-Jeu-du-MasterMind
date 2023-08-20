Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class options
    Const MAXVALEURSCROLL = 509
    Const CHRONO = 90
    Const CHEMIN = ".\score.txt"
    Const NBCOUPS = 15
    Const COULEUR_PRESENT As Integer = 0
    Const COULEUR_BIEN_PLACE As Integer = 1
    Dim couleur2 As DialogResult
    Dim couleur1 As DialogResult

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            'si on appuie sur un caractère qui n'est un chiffre et si la touche appuyée n'est pas "effacer" ne fait rien
            e.Handled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox6.Visible = True Then
            TextBox6.Visible = False
            HScrollBar1.Visible = False
        Else
            TextBox6.Visible = True
            HScrollBar1.Visible = True
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim textBoxes As System.Windows.Forms.TextBox() = {TextBox1, TextBox2, TextBox3, TextBox4, TextBox5}
        For Each textBox As System.Windows.Forms.TextBox In textBoxes
            If textBox.Text = "" Then
                textBox.BackColor = Color.Red
            End If
        Next


        If TextBox1.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "" And TextBox4.Text <> "" And TextBox5.Text <> "" Then
            For i As Integer = 0 To Panel1.Controls.Count - 1 'on modifie le tableau carac jouables en ajoutant les elements des textbox 
                Dim elem As Control = Panel1.Controls(i)
                SetCarac(i, elem.Text)
            Next
        End If

        If TextBox6.Text <> "" And TextBox6.Visible = True Then
            SetTempsChronoMax(Convert.ToInt32(TextBox6.Text)) 'on modifie ChronoMax selon le contenu de la textbox 
            TextBox6.BackColor = Color.White
        End If
        If TextBox6.Text = "" And TextBox6.Visible = True Then
            TextBox6.BackColor = Color.Red
        End If


        If TextBox8.Text <> " " And TextBox8.Visible = True Then
            SetNBCoups(Convert.ToInt32(TextBox8.Text)) 'on modifie le NB de coups selon le contenu de la textbox 
            TextBox8.BackColor = Color.White
        End If
        If TextBox8.Text = " " And TextBox8.Visible = True Then
            TextBox8.BackColor = Color.Red
        End If

        If couleur1 = Windows.Forms.DialogResult.OK Then
            SetCouleur(COULEUR_PRESENT, ColorDialog1.Color) 'present
        End If
        If couleur2 = Windows.Forms.DialogResult.OK Then
            SetCouleur(COULEUR_BIEN_PLACE, ColorDialog2.Color) 'present et bien placé
        End If
        If TextBox7.Text <> "" Then
            SetChemin(TextBox7.Text)
        End If

        Me.Close() 'ferme le formulaire options
        début.Visible = True 'affiche le formulaire début
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        If Panel1.Visible = True Then
            Panel1.Visible = False
        Else
            Panel1.Visible = True
        End If

    End Sub



    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            'si on appuie sur un caractère qui n'est un chiffre et si la touche appuyée n'est pas "effacer" ne fait rien
            e.Handled = True
        End If
    End Sub




    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox8.Visible = True Then
            TextBox8.Visible = False
        Else
            TextBox8.Visible = True
        End If
    End Sub


    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged, TextBox3.TextChanged, TextBox4.TextChanged, TextBox5.TextChanged
        Dim textBoxes As System.Windows.Forms.TextBox() = {TextBox1, TextBox2, TextBox3, TextBox4, TextBox5}

        For Each textBox As System.Windows.Forms.TextBox In textBoxes
            If textBox.Text <> "" Then
                textBox.BackColor = Color.White
            End If
        Next
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        début.Visible = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        FolderBrowserDialog1.ShowDialog()
    End Sub

    Private Sub FolderBrowserDialog1_Disposed(sender As Object, e As EventArgs) Handles Button4.Click

        TextBox7.Text = FolderBrowserDialog1.SelectedPath.ToString

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        couleur1 = ColorDialog1.ShowDialog()


    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        couleur2 = ColorDialog2.ShowDialog()


    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        SetCouleur(COULEUR_PRESENT, Color.Blue)
        SetCouleur(COULEUR_BIEN_PLACE, Color.Green)
        SetTempsChronoMax(CHRONO)
        SetChemin(CHEMIN)
        SetNBCoups(NBCOUPS)
        '{"#", "£", "%", "$", "@"} 
        SetCarac(0, "#")
        SetCarac(1, "£")
        SetCarac(2, "%")
        SetCarac(3, "$")
        SetCarac(4, "@")
        Me.Close() 'ferme le formulaire options
        début.Visible = True 'affiche le formulaire début
    End Sub

    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
        TextBox6.Text = HScrollBar1.Value
        HScrollBar1.Maximum = MAXVALEURSCROLL
        HScrollBar1.Minimum = 0
    End Sub



End Class