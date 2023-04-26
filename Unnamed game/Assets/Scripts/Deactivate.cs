using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    private void Dweactivate()
    {
        gameObject.SetActive(false);
        this.transform.parent.gameObject.SetActive(false);
    }
}
