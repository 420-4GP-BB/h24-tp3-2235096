using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbreCollations : MonoBehaviour
{
    private GameObject collationTombee;

    [SerializeField] private GameObject[] lesCollations;
    [SerializeField] private Vector3 positionSpawnCollation;

    void Start()
    {
        StartCoroutine(SpawnCollationArbre());
    }

    private IEnumerator SpawnCollationArbre()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            if (collationTombee == null)
            {
                GameObject collationPrefab = lesCollations[Random.Range(0, lesCollations.Length)];
                collationTombee = Instantiate(collationPrefab, positionSpawnCollation, Quaternion.identity);
            }
        }
    }
}