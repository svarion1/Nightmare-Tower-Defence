using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
   public Transform[] waypoints;

   void Start()
   {
      List<Transform> waypointsList = new List<Transform>();

      for (int i = 0; i < transform.childCount; i++)
      {
         waypointsList.Add(transform.GetChild(i));
      }

      waypoints = waypointsList.ToArray();
   }
}
