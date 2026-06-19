using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S1GameMgr : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        SceneManager.LoadScene("Ending");
    }
}
