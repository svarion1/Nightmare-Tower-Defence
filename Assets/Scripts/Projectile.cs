using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

   public float speed = 3.0f, damage = 25f, lifeTime = 4.0f;
   public Transform target;

   void Start()
   {
      Destroy(gameObject, lifeTime);
   }

   // Update is called once per frame
   void Update()
   {
      if (target)
      {
         transform.LookAt(target);
         transform.Translate(Vector3.forward * speed * Time.deltaTime);
      }
      else Destroy(gameObject);
   }

   void OnTriggerEnter(Collider other)
   {
      //Debug.Log("Projectile Collision");

      if (other.CompareTag("Enemy"))
      {
         other.GetComponent<Enemy>().TakeDamage(damage);
         Destroy(gameObject);
      }
   }
}
