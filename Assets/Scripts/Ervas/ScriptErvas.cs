using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptErvas : MonoBehaviour
{
   [SerializeField] private GameObject playerOfScene;
    [SerializeField] private GameObject textErva;
    private bool[] arrayOfActionsPlayer = new bool[4];

    private void Start()
    {
        
            arrayOfActionsPlayer = playerOfScene.gameObject.GetComponent<ScriptPlayer>().GetArrayOfActionPermissionPlayer();
        
    }

    
   

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case "Fase 3 W1":

                arrayOfActionsPlayer[0] = true; // double jump
                Text texto = textErva.gameObject.GetComponent<Text>();
                texto.enabled = true;
                //StartCoroutine(DesabiltyText());
                Debug.Log(texto.enabled);
                break;

                case "W0 - Fase2":

                arrayOfActionsPlayer[0] = true; // double jump
                Text texto7 = textErva.gameObject.GetComponent<Text>();
                texto7.enabled = true;
                //StartCoroutine(DesabiltyText());
                Debug.Log(texto7.enabled);
                break;

                case "Fase 5 W1":

                arrayOfActionsPlayer[1] = true; // Wall jump
                Text texto2 = textErva.gameObject.GetComponent<Text>();
                texto2.enabled = true;
               // StartCoroutine(DesabiltyText());
                break;

                case "Fase 2 W2":

                arrayOfActionsPlayer[2] = true;// dash
                Text texto3 = textErva.gameObject.GetComponent<Text>();
                texto3.enabled = true;
               // StartCoroutine(DesabiltyText());
                break;

                case "Fase 1 W3":

                arrayOfActionsPlayer[3] = true;// tiro
                Text texto4 = textErva.gameObject.GetComponent<Text>();
                texto4.enabled = true;
               // StartCoroutine(DesabiltyText());
                break;

                default:
                    //Nao acontece nada
                break;
            }
        }
    }

   
}
