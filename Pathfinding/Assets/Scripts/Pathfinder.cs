using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathFind {
    BreadthFirst,
    DepthFirst,
    Dijkstra,
    AStar
}

public class Pathfinder : MonoBehaviour {

    public int algorithm = 0;

    public Node initialNode;
    public Node destination;

    List<Node> openNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();
    List<Node> path = new List<Node>();

    Queue<Node> breadth = new Queue<Node>();
    Stack<Node> depth = new Stack<Node>();

    Node currentNode;

    public Material pathMat;
    public Material startMat;
    public Material targetMat;


    // Use this for initialization
    void Start () {
        //initialNode = GameObject.Find("FirstNode").GetComponent<Node>();
        initialNode = GameObject.Find("Cube1").GetComponent<Node>();
        destination = GameObject.Find("Cube13").GetComponent<Node>();
        initialNode.GetComponent<MeshRenderer>().material = startMat;
        destination.GetComponent<MeshRenderer>().material = targetMat;
        OpenNode(initialNode);
	}

    void CreateMatrix() {
        //GameObject[] myNodes = GameObject.FindGameObjectsWithTag("Node");
        //int nodesIndex = 0;
        //for (int i = 0; i < 3; i++) {
        //    for (int j = 0; j < 3; j++) {
        //        matrix[i, j] = myNodes[nodesIndex].GetComponent<Node>();
        //        //Debug.Log(matrix[i, j].name);
        //        matrix[i, j].SetCoords(j, i);
        //        nodesIndex++;
        //    }
        //}

        //for (int i = 0; i < 3; i++) {
        //    for (int j = 0; j < 3; j++) {
        //        if (j > 0)
        //            matrix[i, j].AddAdjacent(matrix[i, j - 1]);

        //        if(j < 2)
        //            matrix[i, j].AddAdjacent(matrix[i, j + 1]);

        //        if (i > 0)
        //            matrix[i, j].AddAdjacent(matrix[i - 1, j]);

        //        if (i < 2)
        //            matrix[i, j].AddAdjacent(matrix[i + 1, j]);
        //    }
        //}
    }

    void FixedUpdate() {
        //GetPath();
    }

    public List<Node> GetPath() { 
        while(openNodes.Count > 0) {
            currentNode = VisitNode();
            if(currentNode == destination) {
                CreatePath(currentNode);
                //Debug.Log(currentNode);
                return path;
            }else {
                CloseNode(currentNode);
                OpenAdjacent(currentNode);
            }
        }

        return null;
    }

    Node VisitNode() {

        Node n = null;

        switch (algorithm) {
            case 0:
                n = breadth.Dequeue();
                openNodes.RemoveAt(openNodes.Count - 1);
            break;

            case 1:
                n = depth.Pop();
                openNodes.RemoveAt(0);
            break;

            case 2:
                int maxMov = 9999;
                int index = 0;

                for(int i = 0; i < openNodes.Count; i++) {
                    if(openNodes[i].distance < maxMov) {
                        n = openNodes[i];
                        maxMov = openNodes[i].distance;
                        index = i;
                    }
                }

                openNodes.RemoveAt(index);
            break;

            case 3:
                int total = 9999;
                int indexStar = 0;

                for (int i = 0; i < openNodes.Count; i++) {
                    if ((openNodes[i].distance + openNodes[i].hx) < total) {
                        n = openNodes[i];
                        total = openNodes[i].distance + openNodes[i].hx;
                        indexStar = i;
                    }else if((openNodes[i].distance + openNodes[i].hx) == total) {
                        if(openNodes[i].hx < n.hx) {
                            n = openNodes[i];
                            total = openNodes[i].distance + openNodes[i].hx;
                            indexStar = i;
                        }
                    }
                }

                openNodes.RemoveAt(indexStar);
            break;
        }

        return n;
    }

    void OpenNode(Node n) {
        if (n != initialNode)
            n.SetParent(currentNode);
        n.alreadyOpened = true;
        //openNodes.Add(n);
        switch (algorithm) {
            case 0:
                breadth.Enqueue(n);
                openNodes.Add(n);
            break;

            case 1:
                depth.Push(n);
                openNodes.Add(n);
            break;

            case 2:
                openNodes.Add(n);
            break;

            case 3:
                openNodes.Add(n);
                n.SetDestination(destination);
            break;
        }
    }

    void CloseNode(Node n) {
        closedNodes.Add(n);
    }

    void OpenAdjacent(Node n) {
        List<Node> adj = n.GetAdjacent();
        for (int i = 0; i < adj.Count; i++) {
            if(!adj[i].alreadyOpened && !adj[i].isWall) {
                OpenNode(adj[i]);
            }
        }
    }

    void CreatePath(Node n) {
        Node p;
        path.Add(n);
        p = n.GetParent();
        if (p != null) {
            p.GetComponent<MeshRenderer>().material = pathMat;
            Debug.Log(n.name + " Parent: " + p.name);
            CreatePath(p);
        }
    }
}
