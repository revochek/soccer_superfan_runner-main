using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hero))]
[RequireComponent(typeof(HeroStats))]
[RequireComponent(typeof(Rigidbody2D))]
public class HeroMovement : MonoBehaviour
{
    private float _movementSpeed;

    private HeroStats _stats;
    [SerializeField] private InputManager _inputManager;

    private Rigidbody2D _rb;
    private Vector2 _moveVector;
    private bool _isStopped = false;

    private void Start()
    {
        _stats = GetComponent<HeroStats>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_isStopped)
        {
            _movementSpeed = _stats.MovementSpeed;
            Move(_inputManager.GetMoveDirection(), _movementSpeed);
        }
    }

    public void Move(Vector2 direction, float speed)
    {
        if (direction.sqrMagnitude < 0.1)
            return;

        _moveVector = Vector2.zero;
        _moveVector.x = direction.x * speed * Time.deltaTime;
        _moveVector.y = direction.y * speed * Time.deltaTime;

        _rb.MovePosition(_rb.position + _moveVector);        
    }

    public void ResetVelocity()
    {
        _rb.velocity = Vector2.zero;
    }

    public IEnumerator StopAndMoveAfterDelay(float delay)
    {
        _isStopped = true;
        yield return new WaitForSeconds(delay);
        _isStopped = false;
    }
}