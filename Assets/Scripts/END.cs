using UnityEngine;

public class END : MonoBehaviour
{
     public GameObject winUI;
     

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.gameObject.tag == "end")
          {
               Time.timeScale = 0;
               winUI.SetActive(true);
          }
     }
}
