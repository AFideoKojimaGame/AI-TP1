using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    Node parent = null;
    List<Node> adjNodes = new List<Node>();

    int col;
    int row;
    Vector2 pos;

    public bool isWall = false;
    public bool alreadyOpened = false;

    public Node left;
    public Node up;
    public Node right;
    public Node down;

    public int distance = 0;
    public int hx = 0;
    public int accumulated = 0;

    public Material wallMat;

	// Use this for initialization
	void Start () {
        pos = transform.position;

        if (isWall)
            gameObject.GetComponent<MeshRenderer>().material = wallMat;

        if (left)
            AddAdjacent(left);

        if (right)
            AddAdjacent(right);

        if (up)
            AddAdjacent(up);

        if (down)
            AddAdjacent(down);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    public List<Node> GetAdjacent() {
        return adjNodes;
    }

    public void AddAdjacent(Node n) {
        adjNodes.Add(n);
    }

    public void SetCoords(int x, int y) {
        col = x;
        row = y;
    }

    public void SetParent(Node n) {
        parent = n;
        int x, y = 0;
        if (transform.position.x > parent.transform.position.x)
            x = (int)Mathf.Abs(transform.position.x - parent.transform.position.x);
        else
            x = (int)Mathf.Abs(parent.transform.position.x - transform.position.x);

        if (transform.position.y > parent.transform.position.y)
            y = (int)Mathf.Abs(transform.position.y - parent.transform.position.y);
        else
            y = (int)Mathf.Abs(parent.transform.position.y - transform.position.y);

        distance = x + y;
        accumulated = distance + parent.accumulated;
    }

    public void SetDestination(Node n) {
        int x, y = 0;
        if (transform.position.x > n.transform.position.x)
            x = (int)Mathf.Abs(transform.position.x - n.transform.position.x);
        else
            x = (int)Mathf.Abs(n.transform.position.x - transform.position.x);

        if (transform.position.y > n.transform.position.y)
            y = (int)Mathf.Abs(transform.position.y - n.transform.position.y);
        else
            y = (int)Mathf.Abs(n.transform.position.y - transform.position.y);

        hx = x + y;

        if(parent)
        accumulated = hx + parent.accumulated;
    }

    public Node GetParent() {
        return parent;
    }
}
