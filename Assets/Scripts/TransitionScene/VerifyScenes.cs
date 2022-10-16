using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VerifyScenes : MonoBehaviour
{
    [SerializeField] protected string nameNextScene;
    
    

    private void Update() {
        GetNextScene();
    }
    public virtual string GetNextScene()
    {
        SceneManager.GetActiveScene();
        if(SceneManager.GetActiveScene().name == "Fase 3")
        {
            if(ScriptContador.ContadorTempoJogo <60 && ScriptPlayer.quantidadeErvasColetadas==3)
            {
                
                nameNextScene = "FinalFeliz";
            }
            else
            {
                nameNextScene = "FinalTriste";
            }

            
        
        }
        
        return nameNextScene;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(nameNextScene);
        }
    }
}
