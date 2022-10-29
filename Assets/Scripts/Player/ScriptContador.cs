using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScriptContador : MonoBehaviour
{
    [SerializeField] private Text textContador;
    [SerializeField] private Transform objectContador;
    public static float ContadorTempoJogo{get; set;}

    private void Start() {
        ContadorTempoJogo = 0;
    }


    private void Update() {
        ContadorTempoJogo+=Time.deltaTime;
        textContador.text = ContadorTempoJogo.ToString("F2");
        
         DontDestroyOnLoad(this.transform.root.gameObject);
         DontDestroyOnLoad(this.objectContador.root.gameObject);
         
         
         DestroySystemContador();//So acontecera na fase final ou menu
    
        //Debug.Log(contadorTempoJogo);
    }

    public void DestroySystemContador()
    {
         if(SceneManager.GetActiveScene().name == "FinalFeliz" || SceneManager.GetActiveScene().name == "FinalTriste" || SceneManager.GetActiveScene().name == "Menu")
         {
             Destroy(this.gameObject);
             Destroy(this.objectContador.gameObject);
           //  Destroy(this.objectsContador[1]);
         }
        
    }
}
