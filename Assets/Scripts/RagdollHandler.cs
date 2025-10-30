using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private List<Collider> _hitColliders;
    private List<Rigidbody> _rigidbodies;
    private List<Vector3> _originalPositions;
    private List<Quaternion> _originalRotations;
    private bool _isRagdollInitialized;

    public void Initialize()
    {
        _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        
        if (!_isRagdollInitialized)
        {
            _originalPositions = _rigidbodies.Select(rb => rb.transform.localPosition).ToList();
            _originalRotations = _rigidbodies.Select(rb => rb.transform.localRotation).ToList();
            _isRagdollInitialized = true;
        }

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

    public void DisableRagdoll()
    {
        for (int i = 0; i < _rigidbodies.Count; i++)
        {
            _rigidbodies[i].isKinematic = true;
            _rigidbodies[i].transform.localPosition = _originalPositions[i];
            _rigidbodies[i].transform.localRotation = _originalRotations[i];
        }
    }
}