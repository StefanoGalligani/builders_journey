using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private float explosionForce;
        [SerializeField] private float explosionRadius;

        //probabilmente poco realistico il fatto che se ci sono più blocchi del veicolo vicini alla mina
        //si ripete più volte la forza dell'esplosione, oppure ha senso perché c'è più superficie colpita?
        public void OnCollisionEnter2D(Collision2D other) {
            if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Vehicle"))) return;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D c in colliders) {
                Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
                if (!rb) continue;

                Vector2 direction = rb.position - (Vector2)transform.position;
                float distance = direction.magnitude;
                float actualForce = (1 - distance/explosionRadius) * explosionForce;
                direction.Normalize();
                
                rb.AddForceAtPosition(direction * actualForce, (Vector2)transform.position, ForceMode2D.Impulse);
            }

            Destroy(gameObject);
        }
    }
}
