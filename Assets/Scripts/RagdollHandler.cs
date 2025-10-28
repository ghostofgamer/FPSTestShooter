using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private List<Rigidbody> _rigidbodies;

    public void Initialize()
    {
        _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        DisableRagdoll();
    }

    public void Hit(Vector3 force, Vector3 hitPosition)
    {
        Rigidbody injuredRigidbody =
            _rigidbodies
                .OrderBy(r => Vector3.Distance(r.position, hitPosition))
                .FirstOrDefault();

        if (injuredRigidbody != null)
            injuredRigidbody.AddForceAtPosition(force, hitPosition, ForceMode.Impulse);
    }

    public void EnableRagdoll()
    {
        foreach (Rigidbody rb in _rigidbodies)
            rb.isKinematic = false;
    }

    private void DisableRagdoll()
    {
        foreach (Rigidbody rb in _rigidbodies)
            rb.isKinematic = true;
    }
}