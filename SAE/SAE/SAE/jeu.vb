Imports Microsoft.JScript

Public Class jeu
    Const TAILLE_MAX_TAB = 5
    Const TAILLE_TAB_SUBITEMS = 4
    Const UNE_SECONDE = 1000
    Dim Chrono = getTempsChronoMax()  'secondes
    Dim NBCoupsJeu = getNBCoups() 'nb de coups 
    Dim tabGood(TAILLE_MAX_TAB) 'tab avec les positions des caractères bien placés
    Dim tabElem(TAILLE_MAX_TAB) 'tab des elements tapés dans les textBox
    Dim commun As Boolean = True

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress
        'on récupére depuis le module les caractères jouables 
        Dim caracJouable As String() = {getCarac(0), getCarac(1), getCarac(2), getCarac(3), getCarac(4)}
        If Not Char.IsControl(e.KeyChar) AndAlso Not caracJouable.Contains(e.KeyChar.ToString()) Then
            'si on appuie sur un caractère non jouable  et si la touche appuyée n'est pas "effacer" ne fait rien
            e.Handled = True
        End If

        sender.backcolor = Color.White
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim CptCouleur = 0
        Dim textBoxes As TextBox() = {TextBox1, TextBox2, TextBox3, TextBox4, TextBox5}
        For Each textBox As TextBox In textBoxes
            If textBox.Text = "" Then
                textBox.BackColor = Color.Red
                CptCouleur += 1
            End If
        Next
        If CptCouleur <> 0 Then
            Exit Sub
        End If



        'remplir les tableaux good et elem
        Dim indice As Integer = 0
        For Each elem In Panel1.Controls
            If getFind(indice) = elem.Text Then 'find est le tableau ou est stocké la chaine de caractere a deviné
                tabGood(indice) = 1 'present et bien placé
            Else
                tabGood(indice) = -1 'pas present ou mal placé mais present 
            End If
            tabElem(indice) = elem.Text 'ajoute au tableau Elem, le texte des Textbox
            indice += 1
        Next

        indice = 0

        TextBox_Couleur(sender, e) ' change la couleur des textBox
        ListView1_Ajout(sender, e) 'ajout du texte des textBox à la listView

        'verif si la combinaison tapée est identique avec le code à deviner
        For i As Integer = 0 To Panel1.Controls.Count - 1
            If getFind(i) <> tabElem(i) Then
                commun = False
                Exit For
            End If
        Next
        If commun Then 'si commun, partie finie
            Timer1.Stop() 'on stop le timer
            Label7.Visible = True 'TROUVE
            PictureBox1.Visible = True
            PictureBox4.Visible = True
            Button1.Enabled = False 'désactiver le button Guess
            Button2.Visible = True 'button Bye pour quitter le formulaire 
        End If
        If Not commun Then 'pas commun, nombre de coups diminue de 1
            NBCoupsJeu -= 1
        End If
        commun = True

    End Sub

    Private Sub jeu_Load(sender As Object, e As EventArgs) Handles MyBase.Load, Timer1.Tick
        Label3.Text = getCarac(0) & getCarac(1) & getCarac(2) & getCarac(3) & getCarac(4)
        Label4.ForeColor = getCouleur(0) 'present
        Label6.ForeColor = getCouleur(1) 'present et bien placé
        Dim lblHeader As New Label

        Timer1.Interval = UNE_SECONDE '= 1 seconde
        Timer1.Start()
        Chrono = Chrono - 1 'diminue le chrono

        lblHeader.Text = "Il vous reste " + NBCoupsJeu.ToString + " coup(s)...s" + " temps: " + Chrono.ToString
        Label9.Text = "Il vous reste " + NBCoupsJeu.ToString + " coup(s)...s" + " temps: " + Chrono.ToString
        Me.Text = lblHeader.Text

        If (Chrono = 0 Or NBCoupsJeu = 0) Then 'si le temps imparti est terminé ou que le nombre de coups est épuisé 
            Timer1.Stop()   'stop timer
            Label8.Visible = True
            PictureBox4.Visible = True
            PictureBox2.Visible = True
            Button2.Visible = True 'button Bye pour quitter le formulaire 
            Button1.Enabled = False 'désactiver le button Guess
        End If
    End Sub



    '* @brief 'ajout du texte des textBox à la listView
    Private Sub ListView1_Ajout(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

        Dim newitem As ListViewItem = Nothing
        Dim subItems As ListViewItem.ListViewSubItem() = Nothing
        newitem = New ListViewItem()
        newitem.UseItemStyleForSubItems = False
        'crée un tab de sousItems
        subItems = New ListViewItem.ListViewSubItem(TAILLE_TAB_SUBITEMS) {New ListViewItem.ListViewSubItem(), New ListViewItem.ListViewSubItem(), New ListViewItem.ListViewSubItem(), New ListViewItem.ListViewSubItem(), New ListViewItem.ListViewSubItem()}

        For i As Integer = 0 To Panel1.Controls.Count - 1
            Dim elem As Control = Panel1.Controls(i)
            subItems(i).Text = elem.Text 'le texte du sousItem égal au texte des textBox
            If elem.BackColor.ToArgb() <> Color.White.ToArgb() Then 'si la couleur de fond de l'element est différent du blanc
                subItems(i).ForeColor = elem.BackColor  'on change la couleur du sousItem pour correspondre à l'element
            End If
        Next
        newitem.SubItems.AddRange(subItems) 'on ajoute les sousItems à l'item newItem
        ListView1.Items.Add(newitem) 'on ajoute à la ListView l'item newItem


    End Sub

    '@brief change la couleur des textBox pour correspondre à leur état : absent, présent, bien placé
    Private Sub TextBox_Couleur(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged, TextBox3.TextChanged, TextBox4.TextChanged, TextBox4.TextChanged
        Dim cpt = 0 'si le cpt = 0 signifie absent, si cpt <> 0 signifie présent 
        For i As Integer = 0 To Panel1.Controls.Count - 1
            Dim elem As Control = Panel1.Controls(i)
            For j = 0 To Panel1.Controls.Count - 1
                If tabElem(i) = getFind(j) Then 'si bien placé
                    elem.BackColor = Color.White
                    cpt = +1
                End If

            Next
            If cpt = 0 Then 'Blanc pour absent
                elem.BackColor = Color.White
            End If
            If cpt <> 0 Then
                elem.BackColor = getCouleur(0) 'present 
                If tabGood(i) = 1 Then 'si en plus dans tabGood = 1 alors present et bien placé 
                    elem.BackColor = getCouleur(1)
                End If
            End If
            cpt = 0
        Next
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'joueurs
        Dim index1 = GetJoueurs().FindIndex(Function(p) p.nom = début.ComboBox1.Text) 'cherche l'index du joueur1 dans la comboBox1
        Dim index2 = GetJoueurs().FindIndex(Function(p) p.nom = début.ComboBox2.Text) 'cherche l'index du joueur2 dans la comboBox2
        Dim joueur As Joueur = getJoueur(index1)
        Dim joueur2 As Joueur = getJoueur(index2)

        joueur.Nbpartie1 += 1
        joueur2.Nbpartie2 += 1


        'Le joueur 1 a gagné car Chrono est finie ou NbCoups épuisé
        If (Chrono = 0 Or NBCoupsJeu = 0) Then
            With joueur
                .point += 1
                Chrono = getTempsChronoMax() - Chrono
                .tempTotal += Chrono
                If (.tempBest > Chrono Or .tempBest = 0) Then
                    .tempBest = Chrono
                End If
            End With
        End If


        'Le joueur 2 a gagné car le code a deviner est identique à la combinaision rentrée 
        Dim identique = True
        For i As Integer = 0 To Panel1.Controls.Count - 1
            If getFind(i) <> tabElem(i) Then
                identique = False
                Exit For
            End If
        Next
        If identique = True Then
            With joueur2
                .point += 1
                Chrono = getTempsChronoMax() - Chrono
                .tempTotal += Chrono
                If (.tempBest > Chrono Or .tempBest = 0) Then
                    .tempBest = Chrono
                End If
            End With
        End If

        ' Mettre à jour le joueur dans la liste
        SetJoueur(index1, joueur)
        SetJoueur(index2, joueur2)


        début.Visible = True 'affiche le formulaire début

        'échange le texte des ComboBox
        Dim tmp = début.ComboBox1.Text
        début.ComboBox1.Text = début.ComboBox2.Text
        début.ComboBox2.Text = tmp

        Me.Close() 'ferme le formulaire jeu



    End Sub


End Class