using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameUIMgr : MonoBehaviour
{

    public GameObject settingsPanel;

    private void Awake()
    {
        if (settingsPanel !=null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); //如果狀態為開 則關閉 反之亦然
        if (settingsPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void RestartGame()
    {
        // 確保時間恢復正常（避免卡在暫停）
        Time.timeScale = 1f;

        // 重新載入目前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
