using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollar_mase : MonoBehaviour
{
    public Material[] _matrrial;
    private int i;

	// Use this for initialization
	void Start ()
    {

        i = 0;

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            i++;
            if (i == 3)
            {
                i = 0;
            }
            this.GetComponent<Renderer>().material = _matrrial[i];
        }
	}
}
