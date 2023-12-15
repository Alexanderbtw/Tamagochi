    using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ThrowableObject : MonoBehaviour
{
    [Range(1.5f, 2f)] public float MinSwipeDist = 0f;
    public float ResetTime = 4f;

    [Header("Throw Force")]
    [Range(0f, 100f)] public float ForceMultiplierSide = 50f;
    [Range(0f, 100f)] public float ForceMultiplierUp = 50f;
    [Range(0f, 100f)] public float ForceMultiplierForward = 50f;

    private float startTime;
    private Vector3 startPos;

    private Vector3 resetPos;
    private Quaternion resetRot;

    [HideInInspector] public bool cancelled = false;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
        resetRot = transform.rotation;
        //ResetObject();
    }

    private void OnMouseDown()
    {
        startTime = Time.time;
        startPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 5f;
        var newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = Vector3.Lerp(transform.position, newPosition, 80f * Time.deltaTime);

        if (mousePos.y < startPos.y)
        {
            startPos = mousePos;
            startTime = Time.time;
        }
    }

    private void OnMouseUp()
    {
        var endTime = Time.time;
        var endPos = Input.mousePosition;

        var swipeTime = endTime - startTime;
        var swipeDistance = Camera.main.ScreenToViewportPoint(endPos - startPos).magnitude;

        if (swipeTime < 1f && swipeTime > 0.1f && swipeDistance > MinSwipeDist)
        {
            var force = Camera.main.ScreenToViewportPoint(endPos - startPos) / swipeTime;
            rb.AddForce(new Vector3(force.x * ForceMultiplierSide, force.y * ForceMultiplierUp, force.magnitude * ForceMultiplierForward));
            rb.useGravity = true;

            Invoke(nameof(RestartObject), ResetTime);
            Invoke(nameof(OffCanceled), ResetTime + 0.1f);
        }
        else
            ResetObject();
    }

    private void OffCanceled()
    {
        cancelled = false;
    }
    public void ResetObject()
    {
        startPos = Vector3.zero;
        startTime = 0f;
        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        rb.angularVelocity = Vector3.zero;
        rb.angularDrag = 0f;
        rb.useGravity = false;
        transform.SetPositionAndRotation(resetPos, resetRot);
    }

    private void RestartObject()
    {
        if (cancelled) return;

        ResetObject();
    }
}
