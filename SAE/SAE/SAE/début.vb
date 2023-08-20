Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports Microsoft.JScript


Public Class début


    Dim sr As StreamReader 'Créer une instance de StreamReader pour lire un fichier.

    Dim cheminFichierStats As String = getChemin() & "\score.txt"
    Dim cheminFichierBase As String = ".\score.txt"

    Private Sub début_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not File.Exists(cheminFichierStats) Then 'si le fichier n'existe pas, on le crée
            Dim d As FileStream
            d = File.Create(cheminFichierStats)
            d.Close()
        End If
        ReadFile() 'lire depuis le fichier

    End Sub

    '* @brief lire depuis un fichier
    Private Sub ReadFile()
        Try
            Using a As StreamReader = File.OpenText(cheminFichierStats)
                While a.Peek() <> -1 'tant qu'on n'est pas à la fin du fichier 
                    Dim ligne As String = a.ReadLine() 'on lit une ligne
                    Dim valeurs As String() = ligne.Split(" "c)
                    'on coupe la ligne en plusieurs valeurs qui sont séparées dans le fichier par des espaces , c = on spécifie que le délimiteur est un char
                    ComboBox1.Items.Add(valeurs(0)) 'valeurs(0)) = les noms des joueurs , qu'on ajoute aux deux ComboBox
                    ComboBox2.Items.Add(valeurs(0))

                    If valeurs.Length = 6 Then 'on a 6 valeurs: nom,points,BestTemps,TotalTemps,Nbpartie1,Nbpartie2 
                        AjouterJoueurDepuisFichier(valeurs) 'on ajoute le joueurs dans la liste de joueurs

                    End If
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show("Une erreur s'est produite lors de la lecture du fichier : " & ex.Message)
        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("Etes-vous certain d'arreter ?", vbYesNo, "Demande de confirmation") = vbYes Then 'si on quitte le fichier, on enregistre dans le fichier
            Dim cheminFichierStats As String = getChemin() & "\score.txt"

            If Not File.Exists(cheminFichierStats) Then 'si le fichier n'existe pas, on le crée
                Dim d As FileStream
                d = File.Create(cheminFichierStats)
                d.Close()
            End If

            If cheminFichierStats <> cheminFichierBase Then
                File.WriteAllText(cheminFichierStats, String.Empty)
            End If
            File.WriteAllText("./config.txt", String.Empty)
            File.WriteAllText(cheminFichierBase, String.Empty)
            For Each j As Joueur In GetJoueurs() 'pour chaque joueurs dans la liste, on les ajoute au fichier
                'on concatène les caractèristiques des joueurs
                If cheminFichierStats <> cheminFichierBase Then
                    File.AppendAllText(cheminFichierStats, j.nom & " " & j.point & " " & j.tempTotal & " " & j.tempBest & " " & j.Nbpartie1 & " " & j.Nbpartie2 & vbCrLf)
                End If
                File.AppendAllText(cheminFichierBase, j.nom & " " & j.point & " " & j.tempTotal & " " & j.tempBest & " " & j.Nbpartie1 & " " & j.Nbpartie2 & vbCrLf)
            Next
            File.AppendAllText("./config.txt", getTempsChronoMax().ToString() & " " & getCarac(0).ToString() & " " & getCarac(1).ToString() & " " & getCarac(2).ToString() & " " & getCarac(3).ToString() & " " & getCarac(4).ToString() & " " & getNBCoups().ToString() & " " & getCouleur(0).ToString() & " " & getCouleur(1).ToString() & vbCrLf) 'getChemin().ToString() & vbCrLf)

            Me.Close() 'fermeture du formulaire 
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Then
            Label2.ForeColor = Color.Red
        End If
        If ComboBox2.Text = "" Then
            Label1.ForeColor = Color.Red
        End If
        If ComboBox1.Text <> "" Then
            Label2.ForeColor = Color.Black
        End If
        If ComboBox2.Text <> "" Then
            Label1.ForeColor = Color.Black
        End If

        If ComboBox1.Text = ComboBox2.Text Then
            Label1.ForeColor = Color.Red
            Label2.ForeColor = Color.Red
        End If
        If Label1.ForeColor = Color.Black And Label2.ForeColor = Color.Black Then

            ComboBox1.Items.Clear() ' Effacer les anciennes informations des comboBox
            ComboBox2.Items.Clear()
            Dim j As Joueur
            j.nom = ComboBox1.Text
            partie.AjouterJoueur(j)
            j.nom = ComboBox2.Text

            partie.AjouterJoueur(j)

            For Each p As Joueur In GetJoueurs() 'ajout aux comboBox
                ComboBox1.Items.Add(p.nom)
                ComboBox2.Items.Add(p.nom)
            Next
            Deviner.Show()
            Me.Visible = False

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        score.ListBox1.DataSource = Nothing 'Réinitialise DataSource pour pouvoir clear les items des Listbox
        score.ListBox2.DataSource = Nothing
        score.ListBox3.DataSource = Nothing
        score.ListBox4.DataSource = Nothing
        score.ListBox1.Items.Clear() ' Effacer les anciennes informations ListBox1,ListBox2,ListBox3,ListBox4
        score.ListBox2.Items.Clear()
        score.ListBox3.Items.Clear()
        score.ListBox4.Items.Clear()
        score.ComboBox2.Items.Clear()

        For Each j As Joueur In GetJoueurs() 'ajout a la listBox
            score.ListBox1.Items.Add(j.nom)
            score.ComboBox2.Items.Add(j.nom)
            score.ListBox2.Items.Add(j.point)
            score.ListBox3.Items.Add(j.tempBest)
            Dim ligneStats As String = ""
            ligneStats = ligneStats + j.Nbpartie1.ToString + " /" + j.Nbpartie2.ToString + " /" + j.tempTotal.ToString
            score.ListBox4.Items.Add(ligneStats)

        Next

        Me.Visible = False 'cache le formulaire début
        score.Show() ' Visible = True 'affiche le formulaire score
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Visible = False 'cache le formulaire début
        options.Show() 'affiche le formulaire options
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim message As String
        message = "Crédits: John, Keryan, Ghislain, GROUPE 111 et 112" & vbCrLf & vbCrLf
        message = message & "Règles du MasterMind:" & vbCrLf
        message = message & "- Le jeu se joue avec 2 joueurs, le 1er tape une combinaison et le 2nd essaye de deviner" & vbCrLf & vbCrLf
        message = message & "- Le but est de deviner la combinaison de caractères cachée" & vbCrLf & vbCrLf
        message = message & "- Vous avez un nombre limité de tentatives et un temps limité" & vbCrLf & vbCrLf
        message = message & "- Les couleurs fournies vous indiquent si vous avez le bon"  & vbCrLf
        message = message & "  caractère à la bonne place ou est présente " & vbCrLf
        message = message & "  mais à la mauvaise place ou encore juste absent" & vbCrLf & vbCrLf
        message = message & "- Vos scores sont enregistrés dans un fichier" & vbCrLf & vbCrLf
        message = message & "Bonne chance !" & vbCrLf & vbCrLf


        message = message & "Sources des images : https://www.irasutoya.com/ et Logo du jeu de société créé par Mordecai Meirowitz, Édité par Hasbro "
        MessageBox.Show(message, "MasterMind", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
End Class
