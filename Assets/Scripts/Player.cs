using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<SnapshotCamera> snapCams;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (SnapshotCamera snapCam in snapCams) {
                snapCam.CallTakeSnapshot();
            }
        }
    }
}
