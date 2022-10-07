using UnityEngine;

public class Tile : MonoBehaviour
{

   private bool taken = false;

   public bool Taken
   {
      get { return taken; }
      set { taken = value; }
   }

   void Start()
   {
      OnExit();
   }

   public void OnHover()
   {
      GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 1f, 1f, 0.5f));
   }

   public void OnExit()
   {
      GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 1f, 1f, 0.15f));
   }
}
