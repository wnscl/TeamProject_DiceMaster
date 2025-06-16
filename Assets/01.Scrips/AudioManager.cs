using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region 싱글톤 구현
    private static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static AudioManager Instance
    {
        get
        {
            if( instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    SFXPool audioPool;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource battleAudioSource;
    [SerializeField] AudioSource reactAudioSource;
    [SerializeField] AudioSource backgroundAudioSource1;
    [SerializeField] AudioSource backgroundAudioSource2;

    private void Start()
    {
        audioPool = GetComponent<SFXPool>();
    }

    #region 단발성 오디오 재생 메서드
    public void PlayAudioOnce(MagicSFXEnum enumSFX)
    {
        battleAudioSource.PlayOneShot(audioPool.MagicSFXClips[(int)enumSFX]);
    }

    public void PlayAudioOnce(BuffSFXEnum enumSFX)
    {
        battleAudioSource.PlayOneShot(audioPool.BuffSFXClips[(int)enumSFX]);
    }

    public void PlayAudioOnce(PyhsicsSFXEnum enumSFX)
    {
        battleAudioSource.PlayOneShot(audioPool.PhysicsSFXClips[(int)enumSFX]);
    }

    public void PlayAudioOnce(ReactSFXEnum enumSFX)
    {
        reactAudioSource.PlayOneShot(audioPool.ReactSFXClips[(int)enumSFX]);
    }

    public void PlayAudioOnce(UISFXEnum enumSFX)
    {
        audioSource.PlayOneShot(audioPool.UISFXClips[(int)enumSFX]);
    }
    #endregion

    public void PlayBackGroundAudioOnStart(int stage) //배경음악이 처음 시작할때 혹은 정지됐다 다시 시작할때
    {
        StopAllCoroutines();
        IEnumerator loopeAudio;

        int randNum = GetRandomClipNum(stage);

        backgroundAudioSource1.clip = audioPool.BackGroundAudio[stage][randNum];

        loopeAudio = LoopAudio(stage);
        StartCoroutine(loopeAudio);
    }

    private int GetRandomClipNum(int stage)
    {
        int randNum = UnityEngine.Random.Range(0, audioPool.BackGroundAudio[stage].Length);
        return randNum;
    }

    public void ChangeAudio(AudioClip audio, float convertTime = 2) //배경음악을 중간에 교체할 경우
    {
        StopAllCoroutines();
        IEnumerator changeAudio;

        changeAudio = ChangeAudioCoroutine(audio, convertTime);
        StartCoroutine(changeAudio);
    }

    IEnumerator LoopAudio(int stage) //배경음악이 끝나면 다음 음악 재생
    {
        while (true)
        {
            if(backgroundAudioSource1.isPlaying == true)
            {
                yield return new WaitForSecondsRealtime(2.5f);
            }
            int randNum = GetRandomClipNum(stage);

            backgroundAudioSource1.clip = audioPool.BackGroundAudio[stage][randNum];
            backgroundAudioSource1.Play();
            yield return new WaitForSecondsRealtime(50f);
        }
    }

    IEnumerator ChangeAudioCoroutine(AudioClip changeAudio, float converttime)
    {
        AudioSource originalAudioSource;
        AudioSource alterAudioSource;

        if (backgroundAudioSource1.isPlaying == true)
        {
            originalAudioSource = backgroundAudioSource1;
            alterAudioSource = backgroundAudioSource2;
        }
        else
        {
            originalAudioSource = backgroundAudioSource2;
            alterAudioSource = backgroundAudioSource1;
        }

        alterAudioSource.clip = changeAudio;
        alterAudioSource.volume = 0;
        alterAudioSource.Play();

        for (float i = 0; i < 1; i += Time.deltaTime/ converttime)
        {
            originalAudioSource.volume = 1 - i;
            alterAudioSource.volume = i;

            yield return null;
        }

        originalAudioSource.Stop();
    }
}
