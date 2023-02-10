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

    private void Start()
    {

         if(GetCurrentScene() == "FinalFeliz")
         {
            GameController.s.gameZerado = true;
            GetComponent<SaveGame>().SaveGameOfScene(GameController.s);
         }
    }

    private void Update() 
    {
        GetCurrentScene();

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
            if(GameController.s.gameZerado == true || GetCurrentScene() == "Fase 5 W3")
            {
                Debug.Log("Game ja está todo zerado, nao precisa mais contabilizar");
            }
            else
            {
                GameController.s.arrayFasesDesbloqueadas[SceneManager.GetActiveScene().buildIndex+1] = true;
            }
            
            Debug.Log($"Fase {SceneManager.GetActiveScene().buildIndex+2} é igual a {GameController.s.arrayFasesDesbloqueadas[SceneManager.GetActiveScene().buildIndex]}");
            GetComponent<SaveGame>().SaveGameOfScene(GameController.s);
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
