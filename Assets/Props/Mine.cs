using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Effects;

namespace BuilderGame.Props
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private LayerMask _explodeOnContactWithLayers;
        [SerializeField] private EffectContainer _effects;

        public void OnCollisionEnter2D(Collision2D other) {
            if ((_explodeOnContactWithLayers.value & 1<<other.gameObject.layer) == 0) return;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
            foreach (Collider2D c in colliders) {
                Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
                if (!rb) continue;

                Vector2 direction = rb.position - (Vector2)transform.position;
                float distance = direction.magnitude;
                float actualForce = (1 - distance/_explosionRadius) * _explosionForce;
                direction.Normalize();
                
                rb.AddForceAtPosition(direction * actualForce, (Vector2)transform.position, ForceMode2D.Impulse);
                
            }

            _effects.StartEffects();

            Destroy(gameObject);
        }
    }
}
