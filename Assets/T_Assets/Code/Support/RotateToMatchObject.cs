using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMatchObject : MonoBehaviour
{
    [SerializeField] private GameObject matchedObject;
/*    [SerializeField] private bool isMatchedObjectHorizontal;
*/
    // Update is called once per frame
    void Update()
    {
        if (matchedObject == null)
            return;

        transform.rotation = matchedObject.transform.rotation;
        transform.eulerAngles = matchedObject.transform.eulerAngles;
    }
}
