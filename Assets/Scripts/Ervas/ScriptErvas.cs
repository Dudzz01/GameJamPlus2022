using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptErvas : MonoBehaviour
{
   [SerializeField] private GameObject playerOfScene;

    private bool[] arrayOfActionsPlayer = new bool[4];

    private void Start()
    {
        try
        {
            arrayOfActionsPlayer = playerOfScene.gameObject.GetComponent<ScriptPlayer>().GetArrayOfActionPermissionPlayer();
        }
        catch(UnassignedReferenceException e)
        {
                Debug.Log(e.Message);
        }
        
    }

    private void Update()
    {
        // if(playerOfScene)
        // {
        //     throw new UnassignedReferenceException("PlayerofScene no Script Ervas é nulo");
        // }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case "Fase 3 W1":

                arrayOfActionsPlayer[0] = true;

                break;

                case "Fase 5 W1":

                arrayOfActionsPlayer[1] = true;

                break;

                case "Fase 2 W2":

                arrayOfActionsPlayer[2] = true;

                break;

                case "Fase 1 W3":

                arrayOfActionsPlayer[3] = true;

                break;

                default:
                    //Nao acontece nada
                break;
            }
        }
    }
}
