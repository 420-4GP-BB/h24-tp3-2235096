using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collations : MonoBehaviour, IRamassable
{
    private EnergieJoueur _energieJoueur;
    public void Ramasser(Inventaire inventaireJoueur)
    {
        _energieJoueur = GameObject.Find(ParametresParties.Instance.ChoixPersonnage).GetComponent<EnergieJoueur>();
        _energieJoueur.Energie += ConstantesJeu.GAIN_ENERGIE_MANGER_COLLATION;
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
