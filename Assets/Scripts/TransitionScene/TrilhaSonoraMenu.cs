using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrilhaSonoraMenu : ScriptTrilhaSonoraManager
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip[] soundClip = new AudioClip[2];

    private bool switchMusic;
    private string scene;
    
    private void Start() {
        Debug.Log("aa");
    }

    private void Update() {
        scene = SceneManager.GetActiveScene().name;

        DestroySoundObject();
    }

    public override void DestroySoundObject()
    {
        if(scene == "Menu Original" || scene == "W0 - Fase1" || scene == "W0 - Fase2" || scene == "W0 - Fase3" || scene == "Escolher Mundos" || scene == "Levels of W1" || scene == "Levels of W2" || scene == "Levels of W3" )
        {
                if(switchMusic == false)
                {
                    soundSource.clip = soundClip[0];
                    soundSource.volume = 0.2f;
                    soundSource.Play();
                    switchMusic = true; 
                }
                
        }
         if(scene == "Fase 1 W1" || scene == "Fase 2 W1" || scene == "Fase 3 W1" || scene == "Fase 4 W1" || scene == "Fase 5 W1" || scene == "Fase 1 W2" || scene == "Fase 2 W2" || scene == "Fase 3 W2" || scene == "Fase 4 W2" || scene == "Fase 5 W2" || scene == "Fase 1 W3" || scene == "Fase 2 W3" || scene == "Fase 3 W3" || scene == "Fase 4 W3" || scene == "Fase 5 W3" || scene == "FinalFeliz" )
        {       
                if(switchMusic == true)
                {
                    soundSource.clip = soundClip[1];
                    soundSource.volume = 0.1f;
                    soundSource.Play();
                    switchMusic = false;
                }
                
        }

    }

}
