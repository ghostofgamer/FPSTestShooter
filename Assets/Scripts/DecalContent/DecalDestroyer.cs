using System.Collections;
using UnityEngine;

namespace DecalContent
{
    public class DecalDestroyer : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 3.0f;

        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_lifeTime);
        }

        private void OnEnable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(TurnOff());
        }

        private IEnumerator TurnOff()
        {
            yield return _waitForSeconds;
            gameObject.SetActive(false);
        }
    }
}