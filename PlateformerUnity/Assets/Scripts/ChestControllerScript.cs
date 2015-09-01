using UnityEngine;
using System.Collections;

public class ChestControllerScript : MonoBehaviour {

    Animator anim;
    public bool isOpened = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpened)
        {
            if (other.gameObject.tag == "Player")
            {
                anim.Play("Chest_Opening");
                isOpened = true;
            }
        }
    }
}
