using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrefaceUIMgr : MonoBehaviour
{
    public GameObject preface;
    public GameObject howPlay;

    private void Awake()
    {
        if (preface!=null)
        {
            howPlay.SetActive(false);
        }
    }

    public void NextStep()
    {
        preface.SetActive(false);
        howPlay.SetActive(true);
    }

    public void playerGame()
    {
        SceneManager.LoadScene("Game");
    }

}
