using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCoinCollector : MonoBehaviour
{
    [SerializeField] private CoinManager _manager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<ICoinsHolder>(out ICoinsHolder coinHolder))
        {
            _manager.AddCoins(coinHolder);           
            Destroy(other.gameObject);

            if(other.gameObject.TryGetComponent<Horn>(out Horn horn))
                SoundLoader.Instance.PlaySound(SoundLoader.Instance.HornPickup);
        }
    }
}
