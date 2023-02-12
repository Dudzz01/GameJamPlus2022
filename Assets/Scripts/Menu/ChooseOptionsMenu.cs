using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseOptionsMenu : MonoBehaviour
{
    
    //private AudioSource audioSourceMenu;
    //[SerializeField] private AudioClip audioClipMenu;

    private void Start() {
        //audioSourceMenu = GetComponent<AudioSource>();
    }

    public void SelectPlay()
    {
        //audioSourceMenu.clip = audioClipMenu;
        //audioSourceMenu.Play();
        if(GetComponent<SaveGame>().LoadGameOfScene() == null)
         {
             GameController.s.arrayFasesDesbloqueadas[0] = true;
             Debug.Log($"Fase {1} é igual a {GameController.s.arrayFasesDesbloqueadas[0]}");
             GetComponent<SaveGame>().SaveGameOfScene(GameController.s);
         }
         else
         {
            GameController.s.arrayFasesDesbloqueadas[0] = true;
            GameController.s =  GetComponent<SaveGame>().LoadGameOfScene();
         }
        
         GameController.s.arrayFasesDesbloqueadas[0] = true;
         
         SceneManager.LoadScene("Escolher Mundos");
    }

    public void Tutorial()
    {
         SceneManager.LoadScene("W0 - Fase1");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Créditos");
    }

    public void Exit()
    {
        Application.Quit();
    }

    
}

