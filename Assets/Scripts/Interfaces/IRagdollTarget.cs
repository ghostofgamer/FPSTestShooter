using UnityEngine;

namespace Interfaces
{
    public interface IRagdollTarget
    {
        public void ApplyHit(Vector3 force, Vector3 hitPosition);
    }
}