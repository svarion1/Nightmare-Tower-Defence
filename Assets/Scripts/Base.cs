using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour
{

   public int hp;

   private GameManager gameManager;

   void Start()
   {
      gameManager = Camera.main.GetComponent<GameManager>();
   }

   void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Enemy")
      {
         hp -= other.GetComponent<Enemy>().damage;
         GameOverCheck();
      }
   }

   private void GameOverCheck()
   {
      if (hp <= 0)
         gameManager.GameOver();
   }
}
