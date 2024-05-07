using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestChoux
{
    private GameObject chou, soleil;
    private Inventaire inventaire;

    [SetUp]
    public void CreerObjets()
    {
        soleil = new GameObject("Directional Light");
        soleil.AddComponent<Light>();
        soleil.AddComponent<Soleil>();
        chou = GameObject.Instantiate(PrefabUtility.LoadPrefabContents("Assets/Prefabs/Chou.prefab"));

        var joueur = new GameObject("Joueur");
        inventaire = joueur.AddComponent<Inventaire>();
    }

    [TearDown]
    public void DetruireObjets()
    {
        GameObject.Destroy(soleil);
        GameObject.Destroy(chou);
        GameObject.Destroy(inventaire.gameObject);
    }

    [UnityTest]
    public IEnumerator TestChouCueillir()
    {
        // ====== EXEMPLE DE TEST DÉJÀ FONCTIONNEL ======
        // Valide ce qui se passe quand on plante un chou, qu'on attend 3 jours, puis qu'on le cueille.
        // On vérifie que le nombre de choux


        // ARRANGE: dans le SetUp + ici
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 1;
        inventaire.Choux = 0;
        emplacement.Planter(inventaire);
        yield return null;

        var chouCroissant = chou.GetComponent<ChouCroissant>();
        yield return null;

        // Trois jours pour pousser :
        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        var chouPret = chou.GetComponent<ChouPret>();

        chouPret.Ramasser(inventaire);
        yield return null;

        // ASSERT
        Assert.AreEqual(inventaire.Choux, 1);
    }

    [UnityTest]
    public IEnumerator TestChouPerdGraine()
    {
        // TODO: Tester que quand on vient de planter un chou, l'inventaire a une graine en moins
        //
        // Faites un :         yield return null;
        // après avoir planté le chou, question de simuler qu'au moins 1 frame s'est écoulée avant que
        // vous fassiez votre test

        // ARRANGE
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        //Act
        inventaire.Graines = 1;
        emplacement.Planter(inventaire);
        yield return null;

        //ASSERT
        Assert.AreEqual(inventaire.Graines, 0);

        //Assert.IsTrue(1 == 2);
    }

    [UnityTest]
    public IEnumerator TestChouJourneesPassees()
    {
        // TODO: Tester qu'au bout de 3 jours, le chou est prêt �  se faire cueillir
        //
        // Faites un :         yield return null;
        // après chaque appel de la méthode JourneePassee(); du composant ChouCroissant, question de simuler
        // qu'au moins 1 frame s'écoule entre chaque appel

        // ARRANGE:
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 1;
        inventaire.Choux = 0;
        emplacement.Planter(inventaire);
        yield return null;

        var chouCroissant = chou.GetComponent<ChouCroissant>();
        yield return null;

        //Trois jours
        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        Assert.IsTrue(chouCroissant.Pret);
    }

    [UnityTest]
    public IEnumerator TestChouReplanter()
    {
        // TODO: Vérifier qu'on peut replanter un deuxième chou sur le même emplacement
        // après l'avoir cueilli
        
        inventaire.Graines= 2;
        inventaire.Choux= 0;

        for (int i = 0; i < 2; i++)
        {
            chou.GetComponent<EmplacementChouVide>().Planter(inventaire);
            yield return null;
            var chouCroissant = chou.GetComponent<ChouCroissant>();

            for(int j= 0; j < 3; j++)
            {
                chouCroissant.JourneePassee();
                yield return null;
            }

            chou.GetComponent<ChouPret>().Ramasser(inventaire);
            yield return null;
        }
        Assert.AreEqual(inventaire.Graines, 0);
    }

}
