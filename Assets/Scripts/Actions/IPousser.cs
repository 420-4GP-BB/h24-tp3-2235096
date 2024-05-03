using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPousser : IActionnable
{
    void Pousser(Inventaire inventaireJoueur, ComportementJoueur sujet);
}
