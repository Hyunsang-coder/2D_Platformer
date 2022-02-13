using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        // singleton���� �� ���� �ÿ��� ���������� �÷���
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
            // �� ���� �ҽ��� AudioSource ������Ʈ�� �߰� 
            s.audioSource = gameObject.AddComponent<AudioSource>();

            // AudiSource ������ Sound Ŭ�������� ������ ���� ��ġ���� �ش�
            s.audioSource.clip = s._clip;
            s.audioSource.volume = s._volume;
            s.audioSource.loop = s._loop;
        }
    }

    private void Start()
    {
        // ���۰� ���ÿ� ������� �÷���
        PlayAudio("BGM");
    }

    public void PlayAudio(string name)
    {
        // Array�� ���鼭 name�� ���� ���� �÷��� 
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.audioSource.Play();
            }
        }
    }
}
