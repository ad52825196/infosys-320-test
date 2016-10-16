using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour {
    private bool state = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() {
        GameObject child;
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = new Color(Random.value, Random.value, Random.value);
        state = !state;
        for (int i = 0; i < transform.childCount; i++) {
            child = transform.GetChild(i).gameObject;
            if (child.name == "Data") {
                child.SetActive(state);
            }
        }
    }
}
