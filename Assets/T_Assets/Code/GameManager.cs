using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LockMouse(CursorLockMode.Locked);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        LockMouse(CursorLockMode.None);
        
    }

    private void LockMouse(CursorLockMode lockMode)
    {
        if (Cursor.lockState == lockMode)
            return;

        Cursor.lockState = lockMode;
    }
}
