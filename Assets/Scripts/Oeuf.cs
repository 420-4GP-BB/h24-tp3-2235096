using UnityEngine;

public class Oeuf : MonoBehaviour, IRamassable
{
    public void Ramasser(Inventaire inventaireJoueur)
    {
        inventaireJoueur.Bois++;
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