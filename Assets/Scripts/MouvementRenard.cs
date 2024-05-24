using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouvementRenard : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private GameObject[] _pointsDeDeplacement;
    [SerializeField] private GameObject _prefabRenard;
    private GameObject unPoule;
    private float distanceAttraper = 1f;
    private float distanceSuivre = 5f;



    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _pointsDeDeplacement = GameObject.FindGameObjectsWithTag("PointsRenard");
        Initialiser();
    }

    void Initialiser()
    {
        // Position initiale sur la ferme
        _agent.enabled = false;
        
        var point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Length)];
        transform.position = point.transform.position;
        _agent.enabled = true;

        ChoisirDestinationAleatoire();
    }

    void ChoisirDestinationAleatoire()
    {
        GameObject point = _pointsDeDeplacement[Random.Range(0, _pointsDeDeplacement.Length)];
        _agent.SetDestination(point.transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        if (unPoule != null)
        {
            _agent.SetDestination(unPoule.transform.position);
            
            if (Vector3.Distance(transform.position, unPoule.transform.position) <= distanceAttraper)
            {
                Destroy(unPoule);
                unPoule = null;

                //Source: Aidé par chatgpt
                GameObject autrePoule = ChercherPoule();

                if (autrePoule != null)
                {
                    unPoule = autrePoule;
                }
                else
                {
                    ChoisirDestinationAleatoire();
                }
            }
        }
        else
        {
            GameObject autrePoule = ChercherPoule();

            if (autrePoule != null)
            {
                unPoule = autrePoule;

            }
            else if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                ChoisirDestinationAleatoire();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Poule") && unPoule == null)
        {
            unPoule = other.gameObject;
        }
    }

    GameObject ChercherPoule()
    {
        GameObject[] desPoules = GameObject.FindGameObjectsWithTag("Poule");

        foreach (GameObject poule in desPoules)
        {
            //Source: Aidé par chatgpt
            if (Vector3.Distance(transform.position, poule.transform.position) <= distanceSuivre)
            {
                return poule;
            }
        }
        return null;
    }
}
