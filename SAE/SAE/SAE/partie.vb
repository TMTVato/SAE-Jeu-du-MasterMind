Imports System.IO
Imports System.Data.SqlTypes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status

Module partie
    Public Structure Joueur
        Public nom As String
        Public point As Integer
        Public tempTotal As Integer
        Public tempBest As Integer
        Public Nbpartie1 As Integer
        Public Nbpartie2 As Integer
    End Structure

    Const NBRECOUPS As Integer = 15
    Const TEMPS = 90
    Const TAILLE_MAX_TAB = 5

    Private chemin As String = "."
    Private find(TAILLE_MAX_TAB) As String 'chaine a deviner
    Private carac As String() = {"#", "£", "%", "$", "@"} 'caractères jouables
    Private TempsChronoMax = TEMPS 'secondes
    Private NBCoups = NBRECOUPS 'nb de coups 

    Private couleurs As Color() = {Color.Blue, Color.Green} '1er correpond à présent, 2ème présent et bien placé



    Public Sub SetCouleur(i As Integer, c As Color)
        couleurs(i) = c
    End Sub

    Public Sub SetChemin(c As String)
        chemin = c
    End Sub
    Public Function getChemin()
        Return chemin
    End Function
    Public Function getCouleur(i As Integer)
        Return couleurs(i)
    End Function
    Public Sub SetTempsChronoMax(valeur As Integer)
        TempsChronoMax = valeur
    End Sub

    Public Sub SetNBCoups(valeur As Integer)
        NBCoups = valeur
    End Sub

    Public Function getTempsChronoMax() As Integer
        Return TempsChronoMax
    End Function

    Public Function getNBCoups() As Integer
        Return NBCoups
    End Function
    Public Sub SetFind(i As Integer, lettre As String)
        find(i) = lettre
    End Sub
    Public Function getFind(i As Integer) As String
        Debug.Assert(i >= 0 And i < find.Length)
        Return find(i)
    End Function
    Public Sub SetCarac(i As Integer, lettre As String)
        carac(i) = lettre
    End Sub
    Public Function getCarac(i As Integer) As String
        'Debug.Assert(i >= 0 And i < carac.Length)
        Return carac(i)
    End Function

    Private joueurs As New List(Of Joueur)() 'liste des joueurs

    Public Sub SetJoueur(i As Integer, j As Joueur)
        joueurs(i) = j
    End Sub
    Public Function GetJoueurs() As List(Of Joueur)
        Return joueurs
    End Function

    Public Function getJoueur(i As Integer) As Joueur
        Debug.Assert(i >= 0 And i < joueurs.Count)
        Return joueurs(i)
    End Function

    '* @brief ajoute un joueur à la liste de joueurs
    '* @param[in] joueur As Joueur, un joueur
    '*/
    Public Sub AjouterJoueur(joueur As Joueur)
        ' Vérifier si le joueur existe déjà
        If joueurs.Any(Function(j) j.nom = joueur.nom) Then
            ' Le joueur existe déjà
            Return
        End If

        ' Ajouter le nouveau joueur à la liste
        joueurs.Add(joueur)

    End Sub

    '* @brief ajoute un joueur à la liste de joueurs depuis un fichier
    '* @param[in] values As String(), tableau où sont stockées les valeurs de la ligne du fichier 
    '*/
    Public Sub AjouterJoueurDepuisFichier(valeurs As String())
        Dim joueur As New Joueur() 'on crée un joueur
        joueur.nom = valeurs(0)
        'on convertit la chaîne de caractères en int pour les valeurs numériques
        Integer.TryParse(valeurs(1), joueur.point)
        Integer.TryParse(valeurs(2), joueur.tempTotal)
        Integer.TryParse(valeurs(3), joueur.tempBest)
        Integer.TryParse(valeurs(4), joueur.Nbpartie1)
        Integer.TryParse(valeurs(5), joueur.Nbpartie2)

        joueurs.Add(joueur) 'on ajoute à la liste
    End Sub



End Module
