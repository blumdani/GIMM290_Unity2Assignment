using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class WanderingAI : MonoBehaviour {
    private bool isAlive;
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;
    void Start() {
        isAlive = true;
    }
    void Update() {
        if(isAlive) {
            transform.Translate(0, 0, speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // Draw the ray for a duration of 1 second
            Debug.DrawRay(ray.origin, ray.direction * obstacleRange, Color.red, 1.0f);
            
            if (Physics.SphereCast(ray, 0.75f, out hit)) {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>()) {
                    if (fireball == null) {
                        fireball = Instantiate(fireballPrefab) as GameObject;
                        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        fireball.transform.rotation = transform.rotation;
                    }
                } else if (hit.distance < obstacleRange) {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive (bool alive) {
        this.isAlive = alive;
    }
}