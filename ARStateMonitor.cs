using System;
using System.Collections.Generic;
using System.Linq;
using Collections.Hybrid.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class ARStateMonitor : MonoBehaviour {
    //private List<GameObject> linariaCharacters;
    UnityARAnchorManager uAM;
    Camera camera;
    Ray ray;
    RaycastHit raycastHit;
    public GameObject planePrefab;
    GameObject c;
    List<string> prevAnchorNames;
    public List<GameObject> characters;
    List<Vector3> reservedPosition;

    public void Start () {
        uAM = new UnityARAnchorManager ();
        camera = Camera.main;
        UnityARUtility.InitializePlanePrefab (planePrefab);

        ray = Camera.main.ScreenPointToRay (Camera.main.transform.position);

        reservedPosition = new List<Vector3> ();
        // 原点はとらせない感じ
        reservedPosition.Add (Camera.main.transform.position);
    }

    private void Update () {
        List<ARPlaneAnchorGameObject> tempAnchors = uAM.GetCurrentPlaneAnchors ();
        // reservedPosition(linariaCharactersもかもしれん)に要素入れる前に呼ばれてえらいことになる場合があるので要素チェック
        if (characters.Any () && reservedPosition.Any ()) {
            ARPlaneAnchor tempAnchor = tempAnchors.Last<ARPlaneAnchorGameObject> ().planeAnchor;
            Vector3 tempPos = (Camera.main.transform.position + tempAnchor.center) / 2.0f;
            if (!IsReservedPosition (reservedPosition, tempPos)) {
                c = characters.GetAndRemoveAtRandom ();

                GameObject.Instantiate (c, tempPos, Quaternion.identity);
                reservedPosition.Add (tempPos);
            }
        }
    }

    public void OnDestroy () {
        uAM.Destroy ();
    }

    //TODO sqrMagnitudeにそのうち変える
    // Linaria間の距離を出して、ちょうどいい場所か判断
    private bool IsReservedPosition (List<Vector3> reservedPosition, Vector3 newPosition) {
        foreach (var rp in reservedPosition) {
            Debug.Log ("reserved");
            Debug.Log (rp.x.ToString () + "," + rp.y.ToString () + "," + rp.z.ToString ());
            if (Vector3.Distance (rp, newPosition) < 0.3) {
                return true;
            }
        }
        return false;
    }

}