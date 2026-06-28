using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VictoryZone : MonoBehaviour
{
    public event UnityAction HeroFinished;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Hero>(out Hero hero))
        {
            HeroFinished?.Invoke();
            Debug.Log("VictoryZone");
        }
    }
}
