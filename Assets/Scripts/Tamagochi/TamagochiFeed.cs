using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TamagochiFeed : MonoBehaviour
{
    private Tamagochi tamagochi;
    private ParticleSystemRenderer psr;
    private ParticleSystem ps;
    private void Awake()
    {
        tamagochi = transform.parent.gameObject.GetComponent<Tamagochi>();
        ps = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            var obj = collision.gameObject.GetComponent<ThrowableObject>();
            obj.ResetObject();
            obj.cancelled = true;

            tamagochi.Feed();

            //var material = collision.gameObject.GetComponentInChildren<Renderer>().material;
            //psr.material = material;
            ps.Play();
        }
    }
}
