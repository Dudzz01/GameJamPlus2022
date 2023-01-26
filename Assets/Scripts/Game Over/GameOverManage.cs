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
        VerifyScenes.gameOverActive = false;
    }

    public void GoMenu()
    {
        
    }
}
