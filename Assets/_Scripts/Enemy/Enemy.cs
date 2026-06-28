using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum RadiusOptions
    {
        Small,
        Medium,
        Large
    }

    private readonly float[] _radiusVariation = { 0.95f, 1.48f, 2.02f };

    private Vector3 _center = Vector2.zero;
    [SerializeField] private float _speed = 0.8f;
    private RadiusOptions _radiusOption = RadiusOptions.Small;
    private bool _clockwise;
    private float _angle = 0f;
    private bool _isStopped = false;

    private float _radius
    {
        get
        {
            return _radiusVariation[(int)_radiusOption];
        }
    }

    private void Start()
    {
        _clockwise = Random.value > 0.5f;
        UpdateRadiusOption();
        SnapToNearestPointOnCircle();
    }

    void Update()
    {
        if (!_isStopped)
        {
            // Обчислення кута зміни
            float deltaAngle = _speed * Time.deltaTime / _radius;
            _angle += _clockwise ? deltaAngle : -deltaAngle;

            // Обчислення нової позиції
            float x = Mathf.Cos(_angle) * _radius + _center.x;
            float y = Mathf.Sin(_angle) * _radius + _center.y;
            transform.position = new Vector2(x, y);
        }
    }

    void OnDrawGizmos()
    {
        if (_center != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_center, _radius);
        }
        UpdateRadiusOption();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Hero>(out Hero hero))
        {
            hero.ReturnToStartPoint();
        }
        else StartCoroutine(ChangeDirectionAfterDelay(1.5f));
    }

    IEnumerator ChangeDirectionAfterDelay(float delay)
    {
        _isStopped = true;
        yield return new WaitForSeconds(delay);
        _clockwise = !_clockwise;
        _isStopped = false;
    }

    private void UpdateRadiusOption()
    {
        if (_center == null) return;

        float currentDistance = Vector2.Distance(transform.position, _center);
        float closestDistance = Mathf.Infinity;
        RadiusOptions closestOption = _radiusOption;

        for (int i = 0; i < _radiusVariation.Length; i++)
        {
            float distance = Mathf.Abs(currentDistance - _radiusVariation[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestOption = (RadiusOptions)i;
            }
        }

        _radiusOption = closestOption;
    }

    private void SnapToNearestPointOnCircle()
    {
        if (_center == null) return;

        // Отримуємо вектор від центру до поточної позиції ворога
        Vector2 direction = transform.position - _center;

        // Обчислюємо початковий кут на основі напрямку
        _angle = Mathf.Atan2(direction.y, direction.x);

        // Прив'язуємо ворога до найближчої точки на колі
        float x = Mathf.Cos(_angle) * _radius + _center.x;
        float y = Mathf.Sin(_angle) * _radius + _center.y;
        transform.position = new Vector2(x, y);
    }
}