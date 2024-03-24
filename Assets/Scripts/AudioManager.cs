using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _deadSound;
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _killEnemySound;

    public static AudioClip deadSound;
    public static AudioClip shotSound;
    public static AudioClip killEnemySound;

    private void Awake()
    {
        deadSound = _deadSound;
        shotSound = _shotSound;
        killEnemySound = _killEnemySound;
    }
}
