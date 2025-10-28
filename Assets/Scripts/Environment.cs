using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private bool _isStone;

    public bool IsStone => _isStone;
}