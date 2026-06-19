using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUIMgr : MonoBehaviour
{
    // 返回主選單
    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // 避免遊戲曾經暫停
        SceneManager.LoadScene("MainMenu");
    }

    // 離開遊戲
    public void QuitGame()
    {
        Debug.Log("離開遊戲");

        Application.Quit();
    }
}