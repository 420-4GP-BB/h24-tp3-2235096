using UnityEngine;
using UnityEngine.AI;

public class MouvementPoulet : MonoBehaviour
{
    private UnityEngine.GameObject _zoneRelachement;
    private float _angleDerriere;  // L'angle pour que le poulet soit derrière le joueur
    private ComportementJoueur _joueur;
    private bool _suivreJoueur = true;

    private NavMeshAgent _agent;
    private Animator _animator;

    private GameObject[] _pointsDeDeplacement;

    private float distanceJoueurPoule = 1.5f;

    void Start()
    {
        _zoneRelachement = UnityEngine.GameObject.Find("NavMeshObstacle");
        _joueur = GameObject.Find(ParametresParties.Instance.ChoixPersonnage).GetComponent<ComportementJoueur>();
        _suivreJoueur = true;
        _angleDerriere = Random.Range(-60.0f, 60.0f);

        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _pointsDeDeplacement = GameObject.FindGameObjectsWithTag("PointsPoulet");
        _animator.SetBool("Walk", true);
        Initialiser();
    }

    void Initialiser()
    {
        // Position initiale sur la ferme
        _agent.enabled = false;

        Vector3 directionAvecJoueur = Quaternion.AngleAxis(_angleDerriere, Vector3.up) * _joueur.transform.forward;
        transform.position = _joueur.transform.position - directionAvecJoueur;
        transform.rotation = _joueur.transform.rotation;

        _agent.enabled = true;
        gameObject.GetComponent<PondreOeufs>().enabled = true;
    }

    void ChoisirDestinationAleatoire()
    {
        GameObject point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Length)];
        _agent.SetDestination(point.transform.position);
    }

    void Update()
    {
        if (_suivreJoueur)
        {
            Vector3 distance = _joueur.transform.position - transform.position;
            if (distance.magnitude > distanceJoueurPoule)
            {
                _agent.speed = 4f;
                _agent.SetDestination(_joueur.transform.position);
            }
            else if(distance.magnitude <= distanceJoueurPoule)
            {
                _agent.SetDestination(transform.position);
            }
        }

        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            ChoisirDestinationAleatoire();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _zoneRelachement)
        {
            _suivreJoueur = false;
            _agent.speed = 0.25f;
            ChoisirDestinationAleatoire();
        }
    }
}