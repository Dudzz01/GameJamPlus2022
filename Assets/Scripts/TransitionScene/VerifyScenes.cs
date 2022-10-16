using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VerifyScenes : MonoBehaviour
{
    [SerializeField] private string nameNextScene;
    private string[] nameScenes = new string[6]{"Menu", "Fase 1", "Fase 2", "Fase 3", "Vitoria", "Derrota"};

    

    public string GetNextScene()
    {
        return nameNextScene;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(nameNextScene);
        }
    }
}
