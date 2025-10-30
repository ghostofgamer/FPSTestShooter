using System;
using System.Collections.Generic;
using DecalContent;
using ObjectPoolContent;
using UnityEngine;
using Random = UnityEngine.Random;

public class HitHandler : MonoBehaviour
{
    private const string BloodHit = "BloodHit";
    private const string StoneDecal = "StoneDecal";
    private const string MetalDecal = "MetalDecal";

    [SerializeField] private Decal _decalEffectStone;
    [SerializeField] private Decal[] _bloodHitEffects;
    [SerializeField] private Transform _decalContainer;
    [SerializeField] private Decal _decalEffectMetall;
    [SerializeField] private int _count;
    [SerializeField] private float _decalDirectionFactor = 0.01f;

    private Dictionary<string, ObjectPool<Decal>> _effectsPools;

    public event Action Hit;

    private void Awake()
    {
        _effectsPools = new Dictionary<string, ObjectPool<Decal>>();
        InitializePool(BloodHit, _bloodHitEffects, _count);
        InitializePool(StoneDecal, new Decal[] { _decalEffectStone }, _count);
        InitializePool(MetalDecal, new Decal[] { _decalEffectMetall }, _count);
    }

    private void InitializePool(string key, Decal[] prefabs, int count)
    {
        ObjectPool<Decal> pool = new ObjectPool<Decal>(prefabs[0], count, _decalContainer);
        pool.EnableAutoExpand();
        _effectsPools[key] = pool;

        for (int i = 1; i < prefabs.Length; i++)
        {
            for (int j = 0; j < count; j++)
            {
                Decal newDecal = GameObject.Instantiate(prefabs[i], _decalContainer);
                newDecal.gameObject.SetActive(false);
                pool.AddObject(newDecal);
            }
        }
    }

    public void ProcessHit(RaycastHit hit, int damage, float force)
    {
        if (hit.transform.TryGetComponent(out HitPosition hitPosition))
        {
            if (_effectsPools[BloodHit]
                .TryGetObject(out Decal decal, _bloodHitEffects[Random.Range(0, _bloodHitEffects.Length)]))
            {
                decal.transform.position = hit.point;
                decal.transform.rotation = Quaternion.LookRotation(hit.normal);
                decal.transform.Translate(decal.transform.forward * 0.01f, Space.World);
                decal.gameObject.SetActive(true);
            }

            Vector3 hitForce = hit.normal * force;
            hitPosition.TakeDamage(damage, hitForce, hit.point);

            return;
        }
        
        if (hit.rigidbody != null)
            hit.rigidbody.AddForce(-hit.normal * force);

        if (hit.collider.TryGetComponent(out Environment environment))
        {
            string poolKey = environment.IsStone ? StoneDecal : MetalDecal;

            if (_effectsPools[poolKey].TryGetObject(out Decal impactDecal,
                    environment.IsStone ? _decalEffectStone : _decalEffectMetall))
            {
                impactDecal.transform.position = hit.point;
                impactDecal.transform.rotation = Quaternion.LookRotation(hit.normal);
                impactDecal.transform.Translate(impactDecal.transform.forward * _decalDirectionFactor, Space.World);
                impactDecal.gameObject.SetActive(true);
            }
        }
    }
}