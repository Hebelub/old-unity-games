using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

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

    public Camera cam;

    public List<Code> codes = new List<Code>();

    public float showingTime = 4f;
    public float waitTimeAfterZoom = 3f;

    public TextMeshProUGUI hintTmp;

    public TMP_InputField inputFieldTmp;

    public int maxLength = 9;

    public AudioSource audioSorce;

    //public float minAlarmTime = 15f;
    //public float maxAlarmTime = 300f;
    public float chanceEveryType = 0.05f;
    public bool AlarmOn = false;
    public AudioClip ac;

    //public IEnumerator IAlarm()
    //{
    //    while (true)
    //    {
    //        StartCoroutine

    //    }
    //}

    public void AlarmSound()
    {
        Correct("\"Alarm! Du bør gjemme deg for å ikke bli tatt.\"");

        audioSorce.clip = ac;
        audioSorce.Play();

        // audioSorce.PlayOneShot(ac, 1f);

    }

    private void Start()
    {
        // StartCoroutine(IAlarm());

        inputFieldTmp.ActivateInputField();

        inputFieldTmp.characterLimit = maxLength;

        /*
        new Code("Gullbarre", "4");
        new Code("Security", "7");
        new Code("Nøkkel", "3");
        new Code("Blueprint", "1");
        new Code("Håndjern", "5");
        new Code("Direktør", "6");
        */

        new Code("?", "\"Skriv kodeordene inn her\"");
        new Code("Hei", "\"Heisann, hva er det?\"");
        new Code("Hallo", "\"Heisann, hva er det?\"");
        new Code("Help", "\"Skriv kodeordene inn her\"");
        new Code("Hjelp", "\"Skriv kodeordene inn her\"");
        new Code("Kult", "\"Jeg vet\"");
        /*
        new Code("Penger", "\"Man kan bestikke fengselsspionen\"");
        */
        // new Code("Politi", "\"Ja pass deg. Ellers blir du arrestert!\"");

        // new Code("Brage", "\"Han er bankdirektøren!\"");

        new Code("Kode", "\"Ja de må du finne!\"");
        new Code("Koder", "\"Ja de må du finne!\"");

        new Code("Tyv", "\"De er ikke velkomne\"");
        new Code("Ole Jakob", "\"Ja han ja :)\"");
        new Code("Gabriel", "\"Ja han er kul!\"");

        new Code("Lol", "\"Hahaha, nå ler jeg!\"");
        new Code("Test", "\"Ikke test meg!\"");
        new Code("Reidar", "\"Reidar er fantastisk :)!\"");
        new Code("Hebelub", "\"I Hebelubland der kan alt gå ann. Han kan få sand til å sveve og jern til å leve!\"");
        new Code("Håkon", "\"Ja, det er det vakten heter :)\"");

        new Code("3141", "\"Ja, nå skrev du inn koden til sykkellåsen\"");
        new Code("Magnet", "\"Fest dem under bilen\"");
        new Code("Vakt", "\"Putt sovesaft blandet med vann i koppen for å få vakten til å sovne\"");

        new Code("Bibel", "\"Ja den må du lete etter, den skal du bruke! :)\"");

        new Code("Hint", "\"Vis dette til vakten, så kan du tvinge ham til å hjelpe deg med noe ;)\"");

        new Code("START", "\"Skriv inn siste ord i Johannes 3:16\"");
        new Code("Liv", "\"Skriv inn ord nummer 6 i Galaterbrevet 5:22\"");
        new Code("Glede", "\"Hvem skal vi glede oss i i følge Filipperbrevet 4:4?\"");
        new Code("Herren", "\"Koden på sykkellåsen er 3141\"");

        new Code("Sovesaft", "\"Bland vann og sovesafta oppi koppen. Lur så vakten til å drikke det. Da vil han sovne. Da kan du ta nøkkelen i fra ham :)\"");
        new Code("Bil", "\"Fest magnetene under bilen og hent nøkkelen\"");
        new Code("Radio", "\"Fest magnetene under bilen og hent nøkkelen\"");

    }

    //  private void Update()
    //  {
    //      if (Input.GetKeyDown(KeyCode.Escape))
    //      {
    ////          inputFieldTmp.text = "";
    ////          ActivateInputField();
    //      }

    //  }

    public void RemoveText()
    {
        inputFieldTmp.text = "";
    }

    public void ActivateInputField()
    {
        inputFieldTmp.ActivateInputField();
    }

    public SpriteRenderer backGround;

    public float normalCamSize = 5f;
    public float zoomedCamSize = 1f;

    public Vector3 moveCamTo = Vector3.forward * -10;
    public Vector3 moveCamFrom = Vector3.forward * -10;
    public float maxMove = 5f;

    public RectTransform hintTransform;

    public void Correct(string hint)
    {
        StartCoroutine(IFade());

        hintTmp.text = hint;
        inputFieldTmp.text = "";
        inputFieldTmp.gameObject.SetActive(false);

        hintTransform.position = moveCamTo + Vector3.forward * 10;

        IEnumerator IFade()
        {
            moveCamTo = (Vector3)Random.insideUnitCircle.normalized * maxMove + Vector3.forward * -10;

            bool hasWaited = false;

            float t = -1f;

            while(t <= 1)
            {
                if(t >= 0 && hasWaited == false)
                {
                    yield return new WaitForSeconds(hint.Length * waitTimeAfterZoom);
                    hasWaited = true;
                }

                float tt = Mathf.Abs(t);

                yield return null;

            //     cam.backgroundColor = new Color(t, t, t);
                backGround.color = new Color(1, 1, 1, tt);
                hintTmp.color = new Color(1, 1, 1, 1 - tt);

                cam.orthographicSize = zoomedCamSize + tt * tt * (normalCamSize - zoomedCamSize);

                cam.transform.position = Vector3.Lerp(moveCamFrom, moveCamTo, 1 - tt * tt);

                t += (Time.deltaTime / showingTime) * 2f;
            }

        //      cam.backgroundColor = new Color(1f, 1f, 1f);
            backGround.color = new Color(1, 1, 1, 1);
            hintTmp.color = new Color(1, 1, 1, 0);
            cam.orthographicSize = normalCamSize;
            cam.transform.position = moveCamFrom;

            inputFieldTmp.gameObject.SetActive(true);
            inputFieldTmp.ActivateInputField();
        }
    }

    int position = 0;

    public void CheckCodes()
    {

        string b = inputFieldTmp.text.ToLower().Trim();
        if (b == "herren" && position < 1)
        {
            position = 1;
        }

        foreach (Code code in codes)
        {
            string a = code.key.ToLower().Trim();

            if (a == b)
            {
                Correct(code.hint);
                return;
            }
        }

        if(int.TryParse(b, out int number))
        {
            if (number >= 100000 && number < 1000000)
            {
                if(position >= 1)
                {
                    Correct("\"Nummeret til vakten er \"41 761 998. Ring det!\"");
                }
            }

        }

        if (AlarmOn && Random.value < chanceEveryType)
        {
            inputFieldTmp.text = "";
            AlarmSound();
        }
    }

}

public class Code
{
    public string key;
    public string hint;

    public Code (string key, string hint)
    {
        this.key = key;
        this.hint = hint;

        GameManager.instance.codes.Add(this);
    }

}
