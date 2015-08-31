using UnityEngine;
using System.Collections;

public class ChestControllerScript : MonoBehaviour {

    Animator anim;
    public bool opened = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (opened) { anim.Play("Chest_Opened"); }
        //

	
	}

    void OnTriggerEnter (Collider other){
        anim.Play("Chest_Opening");
            opened = true;
        
        //Destroy(other.gameObject);
    }
}
