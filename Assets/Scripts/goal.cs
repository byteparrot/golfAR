using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<golfball>() != null)
            GameManager.Instance.levelEnded();
    }


}
