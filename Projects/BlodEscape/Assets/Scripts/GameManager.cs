using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Lung lung;

    // public List<Connection> conections;
    public List<Link> links;

    public List<GameObject> modules;

    public GameObject redBloodCell;

    public Link GetRandomLinkAndRemoveItFromList()
    {
        Debug.Log("A");
        int nrOfLinks = links.Count;
        Debug.Log("B nrOfLinks: " + nrOfLinks);

        int index = Random.Range(0, nrOfLinks);
        Debug.Log("C index: " + index);

        Link returner = links[index];
        Debug.Log("D");

        links.RemoveAt(index);
        Debug.Log("E");

        return returner;
    }

    private void Start()
    {
        StartCoroutine(ISpawnStuff());
    }

    public IEnumerator ISpawnStuff()
    {

        while(true)
        {
            yield return new WaitForSecondsRealtime(0.05f);

            Debug.Log("AA");
            Link linkForModule = GetRandomLinkAndRemoveItFromList();
            GameObject module = Instantiate(GetRandomModule(), lung.transform);
            ConnectSomethingToLink(module, linkForModule);

            Link linkForBlod = GetRandomLinkAndRemoveItFromList();
            GameObject blod = Instantiate(redBloodCell, lung.transform);
            ConnectSomethingToLink(blod, linkForBlod);

        }
    }

    public void ConnectSomethingToLink(GameObject something, Link link)
    {
        something.transform.SetParent(link.transform);
        something.transform.position = link.transform.position;
    }

    public GameObject GetRandomModule()
    {
        return modules[Random.Range(0, modules.Count)];
    }

}
