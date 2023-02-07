using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VerifyScenes : MonoBehaviour
{
    [SerializeField] private string nameNextScene;

    [SerializeField]private GameObject gameOverObject;

    [SerializeField]private GameObject playerInScene;
    public static bool gameOverActive = false;

    private void Update() 
    {
        
        Debug.Log(GetCurrentScene());

        if(gameOverActive == true)
        {
            Destroy(playerInScene);
            gameOverObject.SetActive(true);
        }
    }

    public static string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player" && ScriptPlayer.QuantidadeErvasColetadas > 0 && ScriptContador.ConcluiuTempoDaFase == true)
        {
            Debug.Log("Player colidiu");
            ScriptPlayer.QuantidadeErvasColetadas = 0;
            SceneManager.LoadScene(nameNextScene);
            
            //O player venceu
        }
        else if(col.gameObject.tag == "Player")
        {
            //O player perdeu
            gameOverActive = true;
        }
    }
}
