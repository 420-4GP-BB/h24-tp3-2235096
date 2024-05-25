using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EtatAction : EtatJoueur
{
    public override bool EstActif => true;
    public override bool DansDialogue => false;
    public override float EnergieDepensee => ConstantesJeu.COUT_MARCHER;


    private GameObject _destination;
    private NavMeshAgent _navMeshAgent;

    private Vector3 pointDestination;

    public EtatAction(ComportementJoueur sujet, GameObject destination) : base(sujet)
    {
        _destination = destination;
        _navMeshAgent = Sujet.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        Animateur.SetBool("Walking", true);
        ControleurMouvement.enabled = false;
        _navMeshAgent.enabled = true;
        Vector3 direction = _destination.transform.position - Sujet.transform.position;
        Sujet.transform.rotation = Quaternion.LookRotation(direction);
        Vector3 pointProche = _destination.GetComponent<Collider>().ClosestPoint(Sujet.transform.position);
        pointDestination = pointProche - direction.normalized * 0.3f;

        _navMeshAgent.SetDestination(pointDestination);
    }

    // On doit se rendre au point pour faire l'action
    public override void Handle()
    {
        float distance = Vector3.Distance(pointDestination, Sujet.transform.position);
        if (!_navMeshAgent.pathPending && distance <= 0.5f)
        {
            _navMeshAgent.enabled = false;
            pointDestination.y = Sujet.transform.position.y;
            Sujet.transform.position = pointDestination;

            var actionnable = _destination.GetComponent<IActionnable>();
            if (actionnable != null)
            {
                Sujet.ChangerEtat(actionnable.EtatAUtiliser(Sujet));
            }
        }
    }

    public override void Exit()
    {
        ControleurMouvement.enabled = true;
        _navMeshAgent.enabled = false;
        Animateur.SetBool("Walking", false);
    }
}