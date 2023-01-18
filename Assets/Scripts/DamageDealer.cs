using System;
using UnityEngine;


public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damaged = 100;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out IInteractable player))
            {
                player.Damage(damaged);
            }
        }
    }
