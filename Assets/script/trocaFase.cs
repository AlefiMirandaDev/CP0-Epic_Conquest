using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// para usar comandos de cena (trocar de fase)
using UnityEngine.SceneManagement;


public class trocaFase : MonoBehaviour
{

    public string nomeDaFase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            carregarNovaFase();

        }
        
    }

    // metodo para trocar de fase
    private void carregarNovaFase()
    {
        //usar para arregar a nova fase
        SceneManager.LoadScene(nomeDaFase);
    }
}
