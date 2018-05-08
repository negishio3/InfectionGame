using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobChange : MonoBehaviour
{ 
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != transform.tag)
        {
            Debug.Log(transform.tag);
            Destroy(other.gameObject);
            switch (transform.tag)
            {
                case "Red":
                    Instantiate(Resources.Load("MobFolder/mobRed"), other.transform.position, other.transform.rotation);
                    break;
                case "Blue":
                    Instantiate(Resources.Load("MobFolder/mobBlue"), other.transform.position, other.transform.rotation);
                    break;
                case "Green":
                    Instantiate(Resources.Load("MobFolder/mobGreen"), other.transform.position, other.transform.rotation);
                    break;
                case "Yellow":
                    Instantiate(Resources.Load("MobFolder/mobYellow"), other.transform.position, other.transform.rotation);
                    break;
            }
           
        }
    }
}
