using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameUIMgr : MonoBehaviour
{

    public GameObject settingsPanel;
    public GameObject musicPanel;
    public Toggle infiniteJumpToggle;

    public static Action<bool> OnInfiniteJumpChanged;
    
    private void Awake()
    {
        if (settingsPanel !=null)
        {
            settingsPanel.SetActive(false);
        }

        if (musicPanel!=null)
        {
            musicPanel.SetActive(false);
        }
    }
    
    public void OpenSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); //如果狀態為開 則關閉 反之亦然
        musicPanel.SetActive(false);
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

    public void Openmusic()
    {
        musicPanel.SetActive(true);
    }
    public void OnInfiniteJumpToggleChanged()
    {
        bool isOn = infiniteJumpToggle.isOn;

        // 👉 發送訊號給玩家
        OnInfiniteJumpChanged?.Invoke(isOn);
    }
    
}
