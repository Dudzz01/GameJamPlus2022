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

    private void Start() {
        Debug.Log($"Fase {1} é igual a {GameController.s.arrayFasesDesbloqueadas[1]}");
    }

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
            
            GameController.s.arrayFasesDesbloqueadas[GameController.s.numeroDeFases] = true;
            GameController.s.numeroDeFases+=1;
            Debug.Log($"Fase {GameController.s.numeroDeFases} é igual a {GameController.s.arrayFasesDesbloqueadas[GameController.s.numeroDeFases]}");
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
