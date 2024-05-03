using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbres : MonoBehaviour, IPousser
{
    [SerializeField] private GameObject _prefabBuche;
    private float dureeTombee = 2f;
    private float tempsPassee = 0f;
    private float tempsDescente = 0.5f;
    private bool arbreTombe = false;

    private void Update()
    {
        if (arbreTombe)
        {
            float descendreArbre = Time.deltaTime * tempsDescente;
            transform.position += Vector3.down * descendreArbre;

            tempsPassee += Time.deltaTime;
            if (tempsPassee >= tempsDescente)
            {
                Destroy(gameObject);
                SpawnBuche();
            }
            
        }
    }

    private void SpawnBuche()
    {
        GameObject unBuche = Instantiate(_prefabBuche, transform.position, Quaternion.identity);
        unBuche.transform.rotation = Quaternion.Euler(90, 0, 0);
        Vector3 rePositionnerBuche = Vector3.up * 0.5f;
        unBuche.transform.position += rePositionnerBuche;
    }

    public void Pousser(Inventaire inventaireJoueur, ComportementJoueur sujet)
    {
        StartCoroutine(TomberArbre(sujet));
    }


    private IEnumerator TomberArbre(ComportementJoueur sujet)
    {
        float vitesseTombee = 90.0f / dureeTombee;
        while (dureeTombee > tempsPassee)
        {
            transform.Rotate(sujet.transform.right, Time.deltaTime * vitesseTombee, Space.World);
            tempsPassee += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        arbreTombe = true;
        tempsPassee = 0f;
    }

    public EtatJoueur EtatAUtiliser(ComportementJoueur sujet)
    {
        return new EtatPousser(sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true;
    }
}
