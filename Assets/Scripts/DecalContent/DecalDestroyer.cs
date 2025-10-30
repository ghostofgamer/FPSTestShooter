using System.Collections;
using UnityEngine;

namespace DecalContent
{
    public class DecalDestroyer : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 3.0f;

        private Coroutine _coroutine;

        private void OnEnable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(TurnOff());
        }

        private IEnumerator TurnOff()
        {
            yield return new WaitForSeconds(_lifeTime);
            gameObject.SetActive(false);
        }
    }
}