using UnityEngine;

namespace DecalContent
{
    public class Decal : MonoBehaviour
    {
        public Transform Parent { get; private set; }

        public void Init(Transform parent )
        {
            Parent = parent;
        }
    }
}