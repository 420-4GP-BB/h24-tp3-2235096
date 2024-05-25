using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamasserBuche : MonoBehaviour, IRamassable
{
    public void Ramasser(Inventaire inventaireJoueur)
    {
        //Ajoute le buche dans le inventaire
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
