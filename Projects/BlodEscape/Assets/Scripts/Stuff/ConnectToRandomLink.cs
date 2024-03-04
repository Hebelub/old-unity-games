using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToRandomLink : MonoBehaviour {

    public Transform connectedTo;

    private GameManager gm;

	void Start () {
        gm = GameManager.instance;

        ConnectoToRandomLink();

        StartCoroutine(IConnect());
    }
	
	public void ConnectoToRandomLink()
    {
        Debug.Log("BA");
        connectedTo = gm.GetRandomLinkAndRemoveItFromList().transform;
    }

    IEnumerator IConnect()
    {
        while (true)
        {
            transform.position = connectedTo.position;

            yield return null;
        }
    }

}
