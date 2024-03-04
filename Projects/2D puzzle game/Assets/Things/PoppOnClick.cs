using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppOnClick : MonoBehaviour {

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

}
