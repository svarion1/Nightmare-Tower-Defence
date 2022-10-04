using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

   public float speed = 3.0f, damage = 2.5f, lifeTime = 5.0f;
   public Transform target;

   void Start()
   {
      Destroy(gameObject, lifeTime);
   }

   // Update is called once per frame
   void Update()
   {
      transform.LookAt(target);
      transform.Translate(Vector3.forward * speed * Time.deltaTime);
   }

   void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Enemy")
      {
         Destroy(gameObject);
      }
   }
}
