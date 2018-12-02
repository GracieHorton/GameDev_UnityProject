using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour {
    Dictionary<string, Transform> parentDict = new Dictionary<string, Transform>();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //TODO: character movement capture here

    void OnCollisionEnter2D(Collision2D col)
    {
        parentDict.Add(col.gameObject.name, col.gameObject.transform.parent);
        col.gameObject.transform.parent = this.transform;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (parentDict.ContainsKey(col.gameObject.name))
        {
            col.gameObject.transform.parent = parentDict[col.gameObject.name];
            parentDict.Remove(col.gameObject.name);
        }
    }
}
