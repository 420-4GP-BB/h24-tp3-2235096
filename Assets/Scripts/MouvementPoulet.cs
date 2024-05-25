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

    private float distanceJoueurPoule = 3f;

    public bool spawnDerriereJoueur = false;

    //Code pour que le poule sort dehors que la nuit
    //Soleil soleil;
    //[SerializeField] private GameObject pointDehorsMaison;

    void Start()
    {
        //soleil = FindObjectOfType<Soleil>();
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
        _agent.enabled = false;

        
        if (!spawnDerriereJoueur)
        {
            //Pour que les poules suivent le joueur
            Vector3 directionAvecJoueur = Quaternion.AngleAxis(_angleDerriere, Vector3.up) * _joueur.transform.forward;
            transform.position = _joueur.transform.position - directionAvecJoueur;
            transform.rotation = _joueur.transform.rotation;
        }
        
        _agent.enabled = true;
        
    }

    void ChoisirDestinationAleatoire()
    {
        GameObject point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Length)];
        _agent.SetDestination(point.transform.position);
    }

    void Update()
    {
        //Code pour que le poule sort dehors que la nuit

        //if (soleil.EstNuitRenard)
        //{
        //    pointDehorsMaison.gameObject.SetActive(true);
        //}
        //else
        //{
        //    pointDehorsMaison.gameObject.SetActive(false);
        //}

        if (_suivreJoueur)
        {
            //Pour que les poules ne pousse pas le joueur
            Vector3 distance = _joueur.transform.position - transform.position;
            if (distance.magnitude > distanceJoueurPoule) //Source: aidé par chatgpt
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
            gameObject.GetComponent<PondreOeufs>().enabled = true;
        }
    }
}