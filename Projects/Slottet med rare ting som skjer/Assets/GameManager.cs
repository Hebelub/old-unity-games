using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    public TextMeshProUGUI text;
    public Image picture;

    public float waitTime = 1f;

    public Happening[] allHappenings;

    public AudioClip audioClip;

    public AudioSource audioSource;

    public float minTime = 0;

    public float time = 0f;

    private void Start()
    {
        StartCoroutine(IStartIt());
    }



    public IEnumerator IStartIt()
    {
        while(true)
        {
            time += waitTime;

            Debug.Log(allHappenings.Length);

            if (time > minTime)
            {
                foreach (Happening h in allHappenings)
                {
                    if (Random.value < h.chanceOfItHappening / 100f)
                    {
                            audioSource.PlayOneShot(audioClip, 1f);
                            ActivateHappening(h);
                            time = 0;
                            break;
                    }
                }
            }

            yield return new WaitForSeconds(waitTime);
        }
    }

    public void ActivateHappening(Happening happening)
    {
        text.text = happening.whatHappenes;
        picture.sprite = happening.sprite;
    }

}

[CreateAssetMenu(fileName = "New Happening", menuName = "Happening", order = 1)]
public class Happening : ScriptableObject
{
    public string whatHappenes;
    public float chanceOfItHappening;
    public Sprite sprite;

}