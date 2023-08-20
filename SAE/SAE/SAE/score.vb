Imports System.Linq

Public Class score
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'cache le formulaire score, retour sur le formulaire début 
        Me.Close()
        début.Visible = True
    End Sub

    Private Sub ListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged, ListBox2.SelectedIndexChanged, ListBox3.SelectedIndexChanged, ListBox4.SelectedIndexChanged
        Dim index As Integer = CType(sender, ListBox).SelectedIndex
        'synchronise l'index selectionné des ListBox
        ListBox2.SelectedIndex = index
        ListBox1.SelectedIndex = index
        ListBox3.SelectedIndex = index
        ListBox4.SelectedIndex = index
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'on trie selon l'option choisie dans la ComboBox
        If ComboBox1.SelectedItem = "Joueurs" Then
            triParJoueur(ListBox1, ListBox2, ListBox3, ListBox4)
        ElseIf ComboBox1.SelectedItem = "Meilleur score" Then
            triParPoint(ListBox1, ListBox2, ListBox3, ListBox4)
        ElseIf ComboBox1.SelectedItem = "Meilleur temps" Then
            triParTempBest(ListBox1, ListBox2, ListBox3, ListBox4)

        End If
    End Sub

    Private Sub score_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Joueurs")
        ComboBox1.Items.Add("Meilleur score")
        ComboBox1.Items.Add("Meilleur temps")
    End Sub


    Public Sub triParJoueur(listBox1 As ListBox, listBox2 As ListBox, listBox3 As ListBox, listBox4 As ListBox)
        ' Récupère les joueurs triés de la liste des joueurs en fonction de leur nom de manière alphabétique'
        Dim ListeJoueursTries = GetJoueurs().OrderBy(Function(joueur) joueur.nom).ToList()

        ' Mise à jour des ListBox des joueurs avec les noms, les points et leur tempsBest en fonction des noms
        listBox1.DataSource = ListeJoueursTries.Select(Function(joueur) joueur.nom).ToList()
        listBox2.DataSource = ListeJoueursTries.OrderBy(Function(joueur) joueur.nom).Select(Function(joueur) joueur.point).ToList()
        listBox3.DataSource = ListeJoueursTries.OrderBy(Function(joueur) joueur.nom).Select(Function(joueur) joueur.tempBest).ToList()

        ' Mise à jour de la quatrième ListBox avec une chaîne combinée de tempTotal, Nbpartie1, et Nbpartie2  en fonction du classement des noms
        listBox4.DataSource = ListeJoueursTries.OrderBy(Function(joueur) joueur.nom).Select(Function(joueur) $" {joueur.Nbpartie1} / {joueur.Nbpartie2} /{joueur.tempTotal}").ToList()
    End Sub

    Public Sub triParTempBest(listBox1 As ListBox, listBox2 As ListBox, listBox3 As ListBox, listBox4 As ListBox)
        ' Récupère les joueurs triés de la liste des joueurs en fonction de tempBest '
        Dim listeJoueursTries = GetJoueurs().OrderBy(Function(joueur) joueur.tempBest).ToList()

        ' Mise à jour des ListBox des joueurs avec les noms, les points et leur tempsBest en fonction du classement du meilleur temps
        listBox1.DataSource = listeJoueursTries.Select(Function(joueur) joueur.nom).ToList()
        listBox2.DataSource = listeJoueursTries.Select(Function(joueur) joueur.point).ToList()
        listBox3.DataSource = listeJoueursTries.Select(Function(joueur) joueur.tempBest).ToList()

        ' Mise à jour de la quatrième ListBox avec une chaîne combinée de tempTotal, Nbpartie1, et Nbpartie2  en fonction du classement du meilleur temps
        listBox4.DataSource = listeJoueursTries.Select(Function(joueur) $" {joueur.Nbpartie1} / {joueur.Nbpartie2} /{joueur.tempTotal}").ToList()
    End Sub

    Public Sub triParPoint(listBox1 As ListBox, listBox2 As ListBox, listBox3 As ListBox, listBox4 As ListBox)
        ' Obtenir les joueurs triés de la liste des joueurs en fonction du meilleur score (point) 
        Dim listeJoueursTries = GetJoueurs().OrderByDescending(Function(joueur) joueur.point).ToList()

        ' Mise à jour des ListBox des joueurs avec les noms, les points et leur tempsBest en fonction du classement du meilleur score 
        listBox1.DataSource = listeJoueursTries.Select(Function(joueur) joueur.nom).ToList()
        listBox2.DataSource = listeJoueursTries.Select(Function(joueur) joueur.point).ToList()
        listBox3.DataSource = listeJoueursTries.Select(Function(joueur) joueur.tempBest).ToList()

        ' Mise à jour de la quatrième ListBox avec une chaîne combinée de tempTotal, Nbpartie1, et Nbpartie2  en fonction du classement du meilleur score 
        listBox4.DataSource = listeJoueursTries.Select(Function(joueur) $" {joueur.Nbpartie1} / {joueur.Nbpartie2} /{joueur.tempTotal} ").ToList()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        For Each j As Joueur In GetJoueurs() 'on parcours les joueurs
            If ComboBox2.SelectedItem = j.nom Then 'si le joueur selectionné dans la ComboBox correspond au nom d'un joueur
                'on affiche  ces statistisques 
                MsgBox("Statistiques du Joueur :" + j.nom.ToString + vbCrLf + " Score= " + j.point.ToString + vbCrLf + " tempTotal=" + j.tempTotal.ToString + vbCrLf + " bestTemp=" + j.tempBest.ToString + vbCrLf + " nbPartieEnJ1=" + j.Nbpartie1.ToString + vbCrLf + " nbPartieEnJ2=" + j.Nbpartie2.ToString)
            End If

        Next
    End Sub
End Class