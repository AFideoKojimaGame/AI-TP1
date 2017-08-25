using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    Pathfinder p;
    List<Node> myPath;

    float moveSpeed = 0.02f;

    float speedX, speedY;

    bool reachedTarget = false;
    int i = 0;

	// Use this for initialization
	void Start () {
        p = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (myPath == null) {
            myPath = p.GetPath();
            i = myPath.Count - 1;
        }else if(!reachedTarget){
            Follow();
        }
	}

    void Follow() {

        transform.position = Vector3.MoveTowards(transform.position, myPath[i].transform.position, Time.deltaTime * 2);
        if(transform.position.x > myPath[i].transform.position.x - 0.01f && transform.position.x < myPath[i].transform.position.x + 0.01f &&
            transform.position.y > myPath[i].transform.position.y - 0.01f && transform.position.y < myPath[i].transform.position.y + 0.01f) {
            transform.position = myPath[i].transform.position;
            i--;
            if(i < 0) {
                reachedTarget = true;
            }
        }
    }
}
