using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _delayToEnable;
    [SerializeField] private float _maxLifeTime = 6;
    [SerializeField] private float _minLifeTime = 2;

    private Renderer _startRenderer;
    private bool _isTouched = false;
    private Coroutine _startCountdown;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Floor>(out _))
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
        var lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
        var timeInterval = new WaitForSeconds(lifeTime);
                
        yield return timeInterval;

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
        _renderer.material.color = Random.ColorHSV();
    }

    private void Init()
    {
        _renderer.material.color = Color.red;
        _isTouched = false;
    }
}
