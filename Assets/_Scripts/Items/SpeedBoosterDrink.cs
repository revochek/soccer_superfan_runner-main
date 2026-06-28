using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoosterDrink : MonoBehaviour
{
    [SerializeField] private float _speedBuff;
    [SerializeField] private float _durationBuff;

    private IBuff _appliedBuff;


    private void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0, 180));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IBuffable>(out IBuffable owner))
        {
            _appliedBuff = new TemporaryBuff(owner, new SpeedBuff(_speedBuff), _durationBuff);

            owner.AddBuff(_appliedBuff);
            Destroy(gameObject);
            SoundLoader.Instance.PlaySound(SoundLoader.Instance.SpeedBooster);
        }
    }
}

