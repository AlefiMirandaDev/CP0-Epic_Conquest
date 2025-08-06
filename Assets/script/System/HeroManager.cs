using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public GameObject[] heroPrefabs; // Arraste os 4 prefabs na ordem no inspetor
    private int currentHeroIndex = 0;
    private GameObject currentHeroInstance;

    private void Start()
    {
        SpawnHero(currentHeroIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // tecla para trocar de her�i
        {
            SwitchHero();
        }
    }

    // Instancia o her�i com base no �ndice
    private void SpawnHero(int index)
    {
        if (currentHeroInstance != null)
        {
            Destroy(currentHeroInstance);
        }            

        currentHeroInstance = Instantiate(heroPrefabs[index], transform.position, Quaternion.identity);

        Camera.main.GetComponent<CameraFollow>()?.SetTarget(currentHeroInstance.transform);

    }

    // Alterna para o pr�ximo her�i
    private void SwitchHero()
    {
        currentHeroIndex = (currentHeroIndex + 1) % heroPrefabs.Length;
        SpawnHero(currentHeroIndex);

    }
}
