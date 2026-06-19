using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIMgr : MonoBehaviour
{
    public GameObject scttingsPlan;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (scttingsPlan!=null)
        {
            scttingsPlan.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpensScttings()
    {
        scttingsPlan.SetActive(true);
    }

    public void ClenScttings()
    {
        scttingsPlan.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void playGame()
    {
        SceneManager.LoadScene("Preface");
    }
    
}
