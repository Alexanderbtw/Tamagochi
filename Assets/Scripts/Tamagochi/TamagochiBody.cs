using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Animator))]
public class TamagochiBody : MonoBehaviour
{
    private Tamagochi tamagochi;
    private ParticleSystem ps;
    private Animator animator;

    private void Awake()
    {
        tamagochi = transform.parent.gameObject.GetComponent<Tamagochi>();
        ps = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
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

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Shower":
                ToggleWashBehavior(other.gameObject);
                break;
            default: 
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        tamagochi.Wash();
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Shower":
                ToggleWashBehavior(other.gameObject);
                break;
            default:
                break;
        }
    }

    private void ToggleWashBehavior(GameObject environment)
    {
        var currWashing = !animator.GetBool("IsWashing");
        var ps = environment.GetComponent<ParticleSystem>();

        if (currWashing)
            ps.Play();
        else
            ps.Stop();

        animator.SetBool("IsWashing", currWashing);
    }

    public void DieBehavior()
    {
        animator.SetTrigger("Death");
    }

    public void ProgramBehavior()
    {
        animator.SetTrigger("Program");
    }
}
