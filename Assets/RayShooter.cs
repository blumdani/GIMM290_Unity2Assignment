using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class RayShooter : MonoBehaviour {
private Camera cam;

    Vector3 hitCoordinates = new Vector3(0, 0, 0);
    void Start() {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI() {
        //Style lines adapted from https://stackoverflow.com/questions/52010746/how-can-i-change-guilayout-label-font-size
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 50;

        int size = 12;
        float posX = cam.pixelWidth/2 - size/4;
        float posY = cam.pixelHeight/2 - size/2;
        GUI.Label(new Rect(posX, posY, size, size), "*", myStyle);

        GUI.Label(new Rect(cam.pixelWidth/2 + size + 50, cam.pixelHeight/2 + size -350, size, size), hitCoordinates.ToString(), myStyle);
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {  
                hitCoordinates = hit.point;
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null) {
                    target.ReactToHit();
                } else {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
}