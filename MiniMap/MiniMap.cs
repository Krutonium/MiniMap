﻿using MSCLoader;
using UnityEngine;

namespace MiniMap
{
    public class MiniMap : Mod
    {
        public override string ID => "MiniMap"; //Your mod ID (unique)
        public override string Name => "MiniMap"; //You mod name
        public override string Author => "BrennFuchS"; //Your Username
        public override string Version => "1.0"; //Version

        GameObject MiniMAP;
        Transform PLAYER;
        Transform MiniMAPCamera;
        Transform GUI;
        GameObject MiniMAPArrow;
        float Zoom;

        public override bool UseAssetsFolder => true;

        public override void OnLoad()
        {
            AssetBundle AB = LoadAssets.LoadBundle(this, "mscminimap");
            GameObject Prefab = AB.LoadAsset<GameObject>("MINIMAPOBJECTS.prefab");
            MiniMAP = GameObject.Instantiate<GameObject>(Prefab);
            GameObject.Destroy(Prefab);

            GameObject.Find("SATSUMA(557kg, 248)").transform.Find("DriverHeadPivot/CameraPivot/PivotSeatR").localEulerAngles = new Vector3(0f, 180f, 0f);

            PLAYER = GameObject.Find("PLAYER").transform;
            MiniMAPCamera = MiniMAP.transform.Find("MAPCAMERA");
            GUI = GameObject.Find("GUI").transform;
            MiniMAPArrow = MiniMAPCamera.Find("PLAYERMARKER").gameObject;

            MiniMAP.transform.Find("MINIMAP").SetParent(GUI.transform.Find("HUD"));

            AB.Unload(false);
        }

        public override void Update()
        {
            if (PLAYER.parent != null)
            {
                if (PLAYER.parent.parent.parent.GetComponent<Rigidbody>() != null)
                {
                    Zoom = PLAYER.parent.parent.parent.GetComponent<Rigidbody>().velocity.x + PLAYER.parent.parent.parent.GetComponent<Rigidbody>().velocity.z / 2f +- 40f;
                }
                
                MiniMAPCamera.GetComponent<Camera>().orthographicSize = Zoom;
                MiniMAPArrow.SetActive(false);
                MiniMAPCamera.eulerAngles = new Vector3(90f, PLAYER.parent.eulerAngles.y, 0f);
                MiniMAPCamera.position = new Vector3(PLAYER.parent.position.x, 15, PLAYER.parent.position.z);
            }
            else
            {
                MiniMAPCamera.GetComponent<Camera>().orthographicSize = 40f;
                MiniMAPArrow.SetActive(true);
                MiniMAPCamera.eulerAngles = new Vector3(90f, PLAYER.eulerAngles.y, 0f);
                MiniMAPCamera.position = new Vector3(PLAYER.position.x, 15f, PLAYER.position.z);
            }          
        }
    }
}