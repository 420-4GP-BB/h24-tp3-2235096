using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EtatPousser : EtatJoueur
{
    public override bool EstActif => true;
    public override bool DansDialogue => false;
    public override float EnergieDepensee => ConstantesJeu.COUT_POUSSER;

    private IPousser _poussable;
    private float _tempsDePousser = 0.0f;
    private bool _estPousser;

    public EtatPousser(ComportementJoueur sujet, IPousser poussable) : base(sujet)
    {
        _poussable = poussable;
        _estPousser = false;
    }

    public override void Enter()
    {
        Animateur.SetBool("Pousser", true);
    }

    
    public override void Handle()
    {
        _tempsDePousser += Time.deltaTime;
        if (_tempsDePousser >= 2f && !_estPousser)
        {
            _poussable.Pousser(Inventaire, Sujet);
            _estPousser = true;
        }
        else if (_tempsDePousser >= 2f)
        {
            Sujet.ChangerEtat(Sujet.EtatNormal);
        }
    }

    public override void Exit()
    {
        Animateur.SetBool("Pousser", false);
    }
}
