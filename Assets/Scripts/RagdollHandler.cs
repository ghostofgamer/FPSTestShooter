using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

public class RagdollHandler : MonoBehaviour, IRagdollTarget
{
    private List<Rigidbody> _rigidbodies;

    public void Initialize()
    {
        _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        DisableRagdoll();
    }

    public void Hit(Vector3 force, Vector3 hitPosition)
    {
        Debug.Log($"Hit object: at position {hitPosition}");

        Rigidbody injuredRigidbody =
            _rigidbodies
                .OrderBy(r => Vector3.Distance(r.position, hitPosition))
                .FirstOrDefault();

        if (injuredRigidbody != null)
        {
            Debug.Log("injuredRigidbody" + injuredRigidbody.gameObject.name);
            Debug.Log("force" + force);
            
            injuredRigidbody.AddForceAtPosition(force, hitPosition, ForceMode.Impulse);
        }
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

    public void ApplyHit(Vector3 force, Vector3 hitPosition)
    {
        if (_rigidbodies == null || _rigidbodies.Count == 0) return;

        Rigidbody injuredRigidbody =
            _rigidbodies.OrderBy(r => Vector3.Distance(r.position, hitPosition)).FirstOrDefault();

        if (injuredRigidbody != null)
            injuredRigidbody.AddForceAtPosition(force, hitPosition, ForceMode.Impulse);
    }
}