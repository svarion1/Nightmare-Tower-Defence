using UnityEngine;

public class Tile : MonoBehaviour
{

   private bool taken = false;

   public bool Taken
   {
      get { return taken; }
      set { taken = value; }
   }
}
