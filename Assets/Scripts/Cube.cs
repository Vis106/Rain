using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private const string PLANE_TAG = "Plane";

    [SerializeField] private float _delayToEnable;
    [SerializeField] private float _maxLifeTime = 6;
    [SerializeField] private float _minLifeTime = 2;

    private Renderer _startRenderer;
    private bool _isTouched = false;
    private Coroutine _startCountdown;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == PLANE_TAG)
        {
            if (_isTouched == false)
            {
                SetRandomColor();
                _isTouched = !_isTouched;
                TurnOnCountdown();
            }
        }
    }

    private IEnumerator SetEnable()
    {
        float elapsedTime = 0f;
        var lifeTime = Random.Range(_minLifeTime, _maxLifeTime);

        var timeInterval = new WaitForSeconds(_delayToEnable);

        while (elapsedTime < lifeTime)
        {
            Debug.Log(lifeTime);
            elapsedTime += Time.deltaTime;
            yield return timeInterval;
        }

        Init();
        this.gameObject.SetActive(false);
    }

    private void TurnOnCountdown()
    {
        if (_startCountdown != null)
        {
            StopCoroutine(_startCountdown);
        }

        _startCountdown = StartCoroutine(SetEnable());
    }

    private void SetRandomColor()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    private void Init()
    {
        GetComponent<Renderer>().material.color = Color.red;
        _isTouched = false;
    }
}
