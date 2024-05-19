using UnityEngine;

public class Oeuf : MonoBehaviour, IRamassable
{
    [SerializeField] private GameObject _prefabPoule;
    private float chanceEclot = 0.25f;
    private int nbreJournees = 3;
    private bool oeufRamasser = false;
    private int joursPassee;

    Soleil soleil;

    void Start()
    {
        soleil = GameObject.FindObjectOfType<Soleil>();
        soleil.OnJourneeTerminee += SpawnOuPourrir;
    }

    private void SpawnOuPourrir()
    {
        joursPassee++;

        if (joursPassee >= nbreJournees)
        {
            float ran = Random.Range(0f, 1f);

            if (ran <= chanceEclot)
            {
                Instantiate(_prefabPoule, transform.position, Quaternion.identity);
                MouvementPoulet mouvementPoulet = GameObject.FindObjectOfType<MouvementPoulet>();
                mouvementPoulet.spawnDerriereJoueur = true;
            }
            soleil.OnJourneeTerminee -= SpawnOuPourrir; //Source: Aide par chatgpt
            Destroy(gameObject);
        }
    }
    
    public void Ramasser(Inventaire inventaireJoueur)
    {
        inventaireJoueur.Oeuf++;
        oeufRamasser = true;
        soleil.OnJourneeTerminee -= SpawnOuPourrir;
        Destroy(gameObject);
    }

    public EtatJoueur EtatAUtiliser(ComportementJoueur Sujet)
    {
        return new EtatRamasserObjet(Sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
}