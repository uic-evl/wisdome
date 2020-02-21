﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SnapshotCamera : MonoBehaviour
{
    Camera snapCam;

    int resWidth = 256;
    int resHeight = 256;


    void Awake()
    {
        snapCam = GetComponent<Camera>();
        // backup if not rendered texture is assigned to camera
        if (snapCam.targetTexture == null)
        {
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }

        snapCam.gameObject.SetActive(false);
    }

    public void CallTakeSnapshot() {
        snapCam.gameObject.SetActive(true);
    }

    void LateUpdate()
    {
        
        if (snapCam.gameObject.activeInHierarchy) {
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            snapCam.Render();
            RenderTexture.active = snapCam.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            byte[] bytes = snapshot.EncodeToPNG();
            string fileName = SnapshotName();
            System.IO.File.WriteAllBytes(fileName, bytes);
            // Debug.Log("Snapshot Taken");
            snapCam.gameObject.SetActive(false);
        }
    }

    string SnapshotName() {
        return string.Format("{0}/Snapshots/snap_{1}_{2}.png", 
            Application.dataPath,             
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"),
            this.gameObject.name);
    }
}
