using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
   public int hp;
   private int maxHp;
   
   public RectTransform hpIndicator;
   public Sprite intactLightbulb;
   public Sprite brokenLightbulb;
   
   private Image[] bulbs;
   private GameManager gameManager;


   void Start()
   {
      maxHp = hp;
      
      gameManager = Camera.main.GetComponent<GameManager>();
      
      bulbs = new Image[hpIndicator.childCount];

      for (int i = 0; i < bulbs.Length; i++)
      {
         bulbs[i] = hpIndicator.GetChild(i).GetComponent<Image>();
      }
   }

   public void TakeDamage(int damage)
   {
      hp -= damage;
      
      UpdateHpUI();
      
      
      GameOverCheck();
   }

   private void GameOverCheck()
   {
      if (hp <= 0)
         gameManager.OnGameOver();
   }

   private void UpdateHpUI()
   {
      //maxHP : count = hp : x
      float count = hpIndicator.childCount;
      float intactLightbulbs = (count * hp) / maxHp;

      for (int i = 0; i < count; i++)
      {
         if (i < intactLightbulbs)
         {
            bulbs[i].sprite = intactLightbulb;
         }
         else
         {
            bulbs[i].sprite = brokenLightbulb;
         }
      }
   }
}
