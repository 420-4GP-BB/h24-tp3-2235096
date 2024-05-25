using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestionnaireInterface : MonoBehaviour
{
    [SerializeField] private Button _boutonDemarrer;
    [SerializeField] private GameObject fermierMale;
    [SerializeField] private GameObject fermierFemale;
    [SerializeField] private TMP_Dropdown choixPersonnageDropDown;
    //[SerializeField] private TMP_Dropdown choixForetDropDown;

    enum Personnage
    {
        Fermier,
        Fermiere
    }

    private Personnage personnage;

    enum Difficulte
    {
        Facile,
        Moyen,
        Difficile
    }

    private Difficulte difficulte;

    //enum Foret
    //{
    //    Rangs,
    //    Hasard,
    //    Simuler
    //}
    //private Foret foret;

    [SerializeField] private TMP_InputField nomJoueur;
    [SerializeField] private TMP_Text presentation;

    [SerializeField] private int[] valeursFacile;
    [SerializeField] private int[] valeursMoyen;
    [SerializeField] private int[] valeursDifficile;

    [SerializeField] private TMP_Text[] valeursDepart;
    [SerializeField] private TMP_Dropdown difficulteDropdown;

    // Start is called before the first frame update
    void Start()
    {
        nomJoueur.text = "Mathurin";
        ChangerNomJoueur();

        difficulte = Difficulte.Facile;
        MettreAJour(valeursFacile);
        personnage = Personnage.Fermier;
        fermierFemale.SetActive(false);

        //foret = Foret.Rangs;
        //choixForetDropDown.onValueChanged.AddListener(delegate { ChoisirTypeForet(); });
    }

    void Update()
    {
        _boutonDemarrer.interactable = nomJoueur.text != string.Empty;
        ChangerPersonnage();
    }

    public void ChangerDifficulte()
    {
        difficulte = (Difficulte)difficulteDropdown.value;

        switch (difficulte)
        {
            case Difficulte.Facile:
                MettreAJour(valeursFacile);
                break;
            case Difficulte.Moyen:
                MettreAJour(valeursMoyen);
                break;
            case Difficulte.Difficile:
                MettreAJour(valeursDifficile);
                break;
        }
    }

    public void ChangerPersonnage()
    {   
        personnage = (Personnage)choixPersonnageDropDown.value;
        switch (personnage)
        {
            case Personnage.Fermier:
                fermierMale.SetActive(true);
                fermierFemale.SetActive(false);
                ParametresParties.Instance.ChoixPersonnage = "Joueur";
                break;
            case Personnage.Fermiere:
                fermierMale.SetActive(false);
                fermierFemale.SetActive(true);
                ParametresParties.Instance.ChoixPersonnage = "Joueuse";
                break;
        }
    }

    //Pour Instancier des arbres

    //public void ChoisirTypeForet()
    //{
    //    foret = (Foret)choixForetDropDown.value;
    //    switch (foret)
    //    {
    //        case Foret.Rangs:
    //            ParametresParties.Instance.TypeForet = "Grille";
    //            break;
    //        case Foret.Hasard:
    //            ParametresParties.Instance.TypeForet = "Hasard";
    //            break;
    //        case Foret.Simuler:
    //            ParametresParties.Instance.TypeForet = "Simuler";
    //            break;
    //    }
    //}

    public void DemarrerPartie()
    {
        int[] valeursActuelles = null;
        switch (difficulte)
        {
            case Difficulte.Facile:
                valeursActuelles = valeursFacile;
                break;
            case Difficulte.Moyen:
                valeursActuelles = valeursMoyen;
                break;
            case Difficulte.Difficile:
                valeursActuelles = valeursDifficile;
                break;
        }

        ParametresParties.Instance.NomJoueur = nomJoueur.text;
        ParametresParties.Instance.OrDepart = valeursActuelles[0];
        ParametresParties.Instance.OeufsDepart = valeursActuelles[1];
        ParametresParties.Instance.SemencesDepart = valeursActuelles[2];
        ParametresParties.Instance.TempsCroissance = valeursActuelles[3];
        ParametresParties.Instance.DelaiCueillete = valeursActuelles[4];

        if (nomJoueur.text != string.Empty)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ferme");
        }
    }

    public void QuitterJeu()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void MettreAJour(int[] valeurs)
    {
        for (int i = 0; i < valeursDepart.Length; i++)
        {
            valeursDepart[i].text = valeurs[i].ToString();
        }
    }

    public void ChangerNomJoueur()
    {
        presentation.text = $"\u266A \u266B Dans la ferme \u00e0  {nomJoueur.text} \u266B \u266A";
    }
}