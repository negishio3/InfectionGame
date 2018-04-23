using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobChange : MonoBehaviour
{ 
   public GameObject[] _MobZombie;
    void Start()
    {
        //_MobZombie[0] = (GameObject)Resources.Load("mob(R)");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != transform.tag)
        {
            Debug.Log(transform.tag);
            Destroy(other.gameObject);
            switch (transform.tag)
            {
                case "Red":
                    Instantiate(_MobZombie[0], other.transform.position, Quaternion.identity);
                    break;
                case "Blue":
                    Instantiate(_MobZombie[1], other.transform.position, Quaternion.identity);
                    break;
                case "Green":
                    Instantiate(_MobZombie[2], other.transform.position, Quaternion.identity);
                    break;
                case "Yellow":
                    Instantiate(_MobZombie[3], other.transform.position, Quaternion.identity);
                    break;
            }
        }
    }
}
