using System.Collections;
using UnityEngine;
public class PauseEffect : MonoBehaviour {
    [Range(0f, 1f)]
    [SerializeField] private float duration=1f;
    private float _pendingFreezeDuration = 0f;
    private bool _isFrozen;
    // Update is called once per frame
    void Update () {
        if (_pendingFreezeDuration > 0 && !_isFrozen)
        {
            StartCoroutine(DoFreeze());
        }

	}
    public void Freeze()
    {
        _pendingFreezeDuration = duration;
    }
    IEnumerator DoFreeze()
    {
        _isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        _pendingFreezeDuration = 0f;
        _isFrozen = false;
    }
}
