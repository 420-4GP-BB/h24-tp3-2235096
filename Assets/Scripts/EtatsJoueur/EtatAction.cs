using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EtatAction : EtatJoueur
{

    //public override bool EstActif => true;
    //public override bool DansDialogue => false;
    //public override float EnergieDepensee => ConstantesJeu.COUT_MARCHER;

    //private GameObject _destination;
    //private NavMeshAgent _navMeshAgent;
    //private Vector3 _targetDirection;
    //private Quaternion _targetRotation;
    //private Vector3 _initialPoint;
    //private Quaternion _initialRotation;
    //private float _rotationPercentage = 0.0f;
    //private float _rotationSpeed = 2.0f;
    //private Vector3 pointDestination;


    //public EtatAction(ComportementJoueur sujet, GameObject destination) : base(sujet)
    //{
    //    _destination = destination;
    //    _navMeshAgent = Sujet.GetComponent<NavMeshAgent>();
    //}


    //public override void Enter()
    //{
    //    Animateur.SetBool("Walking", true);
    //    ControleurMouvement.enabled = false;
    //    _navMeshAgent.enabled = true;
    //    _initialPoint = Sujet.transform.position;
    //    _targetDirection = Vector3.Normalize(_destination.transform.position - Sujet.transform.position);
    //    _targetRotation = Quaternion.LookRotation(_targetDirection);
    //    _initialRotation = Sujet.transform.rotation;
    //    _rotationPercentage = 0.0f;
    //    Vector3 pointProche = _destination.GetComponent<Collider>().ClosestPoint(Sujet.transform.position);
    //    pointDestination = pointProche - _targetDirection.normalized * 0.3f;
    //    _navMeshAgent.SetDestination(pointDestination);
    //    Sujet.StartCoroutine(RotationPourAction());
    //}

    //public override void Handle()
    //{
    //    if (_rotationPercentage < 1.0f)
    //    {

    //        return;
    //    }

    //    float distance = Vector3.Distance(pointDestination, Sujet.transform.position);
    //    if (!_navMeshAgent.pathPending && distance <= 0.3f)
    //    {
    //        _navMeshAgent.enabled = false;
    //        pointDestination.y = Sujet.transform.position.y;
    //        Sujet.transform.position = pointDestination;

    //        var actionnable = _destination.GetComponent<IActionnable>();
    //        if (actionnable != null)
    //        {
    //            Sujet.ChangerEtat(actionnable.EtatAUtiliser(Sujet));
    //        }
    //    }

    //    RotationPourAction();
    //}

    //private IEnumerator RotationPourAction()
    //{
    //    while (_rotationPercentage < 1.0f)
    //    {
    //        _rotationPercentage += Time.deltaTime;

    //        Quaternion rotation = Quaternion.Slerp(_initialRotation, _targetRotation, _rotationPercentage);
    //        Sujet.transform.rotation = rotation;

    //        yield return null;
    //    }
    //}

    //public override void Exit()
    //{
    //    ControleurMouvement.enabled = true;
    //    _navMeshAgent.enabled = false;
    //    Animateur.SetBool("Walking", false);
    //}




    //old code:

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
        if (!_navMeshAgent.pathPending && distance <= 0.3f)
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

        //Vector3 direction = _destination.transform.position - Sujet.transform.position;
        //Sujet.transform.rotation = Quaternion.LookRotation(direction);
        //Vector3 pointProche = _destination.GetComponent<Collider>().ClosestPoint(Sujet.transform.position);
        //Vector3 pointDestination = pointProche - direction.normalized * 0.1f;

        //if (Vector3.Distance(Sujet.transform.position, pointDestination) > 0.1f)
        //{
        //    float distanceAvant = Vector3.Distance(Sujet.transform.position, pointDestination);
        //    ControleurMouvement.SimpleMove(Sujet.transform.forward * (Sujet.VitesseDeplacement));

        //    Il faudrait peut - ?tre essayer avec un NavMesh ici
        //    Sujet.transform.Translate(Sujet.transform.forward * (Sujet.VitesseDeplacement * Time.deltaTime), Space.World);
        //    Sujet.transform.rotation = Quaternion.Euler(0, Sujet.transform.rotation.eulerAngles.y, 0);
        //    float distanceApres = Vector3.Distance(Sujet.transform.position, pointDestination);

        //}
        //else
        //{
        //    ControleurMouvement.enabled = false;
        //    Sujet.transform.position = pointDestination;

        //    Chou chou = _destination.GetComponent<Chou>();
        //    if (chou != null)
        //    {
        //        Sujet.ChangerEtat(new PlanterChou(Sujet, chou));
        //    }

        //    Oeuf oeuf = _destination.GetComponent<Oeuf>();
        //    if (oeuf != null)
        //    {
        //        Sujet.ChangerEtat(new RamasserOeuf(Sujet, oeuf));
        //    }
        //    ControleurMouvement.enabled = true;
        //}
    }

    public override void Exit()
    {
        ControleurMouvement.enabled = true;
        _navMeshAgent.enabled = false;
        Animateur.SetBool("Walking", false);
    }
}