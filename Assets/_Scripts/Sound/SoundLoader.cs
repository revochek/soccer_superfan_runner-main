using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundLoader : MonoBehaviour
{
    public static SoundLoader Instance { get; private set; }

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _hornPickup;
    [SerializeField] private AudioClip _successfulPurchase;
    [SerializeField] private AudioClip _unsuccessfulPurchase;
    [SerializeField] private AudioClip _startButton;
    [SerializeField] private AudioClip _victory;
    [SerializeField] private AudioClip _speedBooster; 
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _prizeCollect;
    [SerializeField] private AudioClip _popupOpen;
    [SerializeField] private AudioClip _popupClose;
    [SerializeField] private AudioClip _useSpell;

    public AudioClip Click => _click;
    public AudioClip HornPickup => _hornPickup;
    public AudioClip SuccessfulPurchase => _successfulPurchase;
    public AudioClip UnsuccessfulPurchase => _unsuccessfulPurchase;
    public AudioClip StartButton => _startButton;
    public AudioClip Victory => _victory;
    public AudioClip SpeedBooster => _speedBooster; 
    public AudioClip Hit => _hit;
    public AudioClip PrizeCollect => _prizeCollect;
    public AudioClip PopupOpen => _popupOpen;
    public AudioClip PopupClose => _popupClose;
    public AudioClip UseSpell => _useSpell;

    private Dictionary<AudioClip, float> _audioClipsWithVolumes;


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _audioClipsWithVolumes = new Dictionary<AudioClip, float>
            {
                { _hornPickup, 0.5f },
                { _successfulPurchase, 1f },
                { _unsuccessfulPurchase, 1f },
                { _startButton, 1f },
                { _victory, 0.5f },
                { _speedBooster, 1f },
                { _hit, 1f },
                { _click, 0.8f },
                { _prizeCollect, 0.5f },
                { _popupOpen, 1f },
                { _popupClose, 1f},
                { _useSpell, 1f }
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StopPlaying()
    {
        _audioSource.Stop();
        _audioSource.loop = false;
    }

    public void PlaySound(AudioClip clip)
    {
        if (_audioClipsWithVolumes.TryGetValue(clip, out float volume))
        {
            StartCoroutine(PlaySoundAsync(clip, volume));
        }
        else
        {
            Debug.LogWarning("AudioClip not found in dictionary!");
        }
    }
    private IEnumerator PlaySoundAsync(AudioClip clip, float volume)
    {
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(clip);
        yield return new WaitWhile(() => _audioSource.isPlaying);
    }
}
