using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float fadeTime = 1;
    Fader fader;

    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(LoadToNextLevel());
        }
    }

    IEnumerator LoadToNextLevel()
    {
        DontDestroyOnLoad(gameObject);
        yield return fader.FadeOut(fadeTime);
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentLevelIndex +1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        // 신 로드 전에 이전 신의 PersistentObject 파괴
        FindObjectOfType<PersistentObject>().ResetScene();
        SceneManager.LoadSceneAsync(nextSceneIndex);
        yield return fader.FadeIn(fadeTime);
        Destroy(gameObject);

    }
}
