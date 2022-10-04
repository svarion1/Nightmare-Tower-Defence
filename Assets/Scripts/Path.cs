using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
   private Transform[] waypoints;

   public Transform[] Waypoints
   {
      get { return waypoints; }
      set { waypoints = value; }
   }

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
