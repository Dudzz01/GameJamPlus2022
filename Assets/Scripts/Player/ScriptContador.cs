using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public  class ScriptContador : MonoBehaviour
{
    [SerializeField] private Text textContador;
    [SerializeField] private Transform objectContador;
    [SerializeField] private float TempoLimiteFase; // Limite de tempo que uma fase pode ser concluida( ex:essa fase pode ser concluida no maximo em 60 segundos se não voce perde)
    private float ContadorTempoDuranteJogo{get; set;} // Tempo de decorrer do jogo
    public static bool ConcluiuTempoDaFase{get; set;}

    private void Start() {
        ContadorTempoDuranteJogo = 0;
    }


    private void Update() {

        if(VerifyScenes.gameOverActive != true) // se não for gameover, o tempo do jogo continuará sendo contado
        {
            ContadorTempoDuranteJogo+=Time.deltaTime;
        }

        
        textContador.text = ContadorTempoDuranteJogo.ToString("F2")+"/"+TempoLimiteFase.ToString();


        if(ContadorTempoDuranteJogo>TempoLimiteFase)
        {
            ConcluiuTempoDaFase = false;
        }
        else
        {
            ConcluiuTempoDaFase = true;
        }
        
    }

}
