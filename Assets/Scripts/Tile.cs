using UnityEngine;
using UnityEngine.AI;

public class Tile : MonoBehaviour
{

   private bool taken = false;
   private Renderer renderer;

   public bool Taken
   {
      get { return taken; }
      set { taken = value; }
   }

   void Start()
   {
      renderer = GetComponent<Renderer>();

      OnExit();
   }

   public void OnHover()
   {
      Debug.Log("Hovered Tile");
      renderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.5f));
      renderer.material.SetColor("_EmissionColor", new Color(1f, 1f, 1f, 1f));
   }

   public void OnSelect()
   {
      Debug.Log("Selected Tile");
      renderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.5f));
      renderer.material.SetColor("_EmissionColor", new Color(0f, 0.25f, 0.95f, 1f));
   }

   public void OnExit()
   {
      renderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.15f));
      renderer.material.SetColor("_EmissionColor", new Color(1f, 1f, 1f, 1f));
   }

   public void OnTake()
   {
      taken = true;
      GetComponent<NavMeshObstacle>().enabled = true;
   }
}
