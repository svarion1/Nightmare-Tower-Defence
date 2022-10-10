using UnityEngine;

public class Base : MonoBehaviour
{
   public int hp;
   private GameManager gameManager;


   void Start()
   {
      gameManager = Camera.main.GetComponent<GameManager>();
   }

   public void TakeDamage(int damage)
   {
      hp -= damage;
      GameOverCheck();
   }

   private void GameOverCheck()
   {
      if (hp <= 0)
         gameManager.OnGameOver();
   }
}
