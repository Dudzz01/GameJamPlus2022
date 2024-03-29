using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManage : MonoBehaviour
{
    
    public void RestartGame()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(VerifyScenes.GetCurrentScene());
        ScriptPlayer.QuantidadeErvasColetadas = 0;
        VerifyScenes.gameOverActive = false;
    }

    public void GoMenu()
    {
        VerifyScenes.gameOverActive = false;
        ScriptPlayer.QuantidadeErvasColetadas = 0;
        SceneManager.LoadScene("Menu Original");
    }
}
