using UnityEngine;

namespace DecalContent
{
    public class Decal : MonoBehaviour
    {
        public Transform Parent { get; private set; }

        public void Init(Transform parent )
        {
            Debug.Log("Init Decal Parent");
            Parent = parent;
        }
    }
}