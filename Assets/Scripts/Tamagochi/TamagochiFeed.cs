using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamagochiFeed : MonoBehaviour
{
    private Tamagochi tamagochi;
    private void Awake()
    {
        tamagochi = transform.parent.gameObject.GetComponent<Tamagochi>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            var obj = collision.gameObject.GetComponent<ThrowableObject>();
            obj.ResetObject();
            obj.cancelled = true;

            tamagochi.Feed();
        }
    }
}
