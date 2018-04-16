using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollar_mase : MonoBehaviour
{
    public Material[] _matrrial;
    private int i;
    Renderer cube;


    // Use this for initialization
    void Start()
    {


        //i = 0;
        cube = GameObject.Find("Cube").GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    i++;
        //    if (i == 1)
        //    {
        //        i = 0;
        //    }
        //    this.GetComponent<Renderer>().material = _matrrial[i];
        //    this.tag = ("Player");
        //}
    }

    public void OnTriggerEnter(Collider collider)
    {
        cube.material.color = Color.red;
        this.tag = ("Player");
    }

}
