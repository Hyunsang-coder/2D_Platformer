using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        // singleton으로 신 변경 시에도 연속적으로 플레이
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            // 각 사운드 소스에 AudioSource 컴포넌트를 추가 
            s.audioSource = gameObject.AddComponent<AudioSource>();

            // AudiSource 값들을 Sound 클래스에서 설정한 값과 일치시켜 준다
            s.audioSource.clip = s._clip;
            s.audioSource.volume = s._volume;
            s.audioSource.loop = s._loop;
        }
    }

    private void Start()
    {
        // 시작과 동시에 배경음악 플레이
        PlayAudio("BGM");
    }

    public void PlayAudio(string name)
    {
        // Array를 돌면서 name이 같은 파일 플레이 
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.audioSource.Play();
            }
        }
    }
}
