using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Height : MonoBehaviour
{
    [SerializeField] private Transform prevLvl;
    [SerializeField] private Transform nextLvl;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            if(col.transform.position.y < transform.position.y){
                cam.MoveToNewHeight(nextLvl);
            }
            else{
                cam.MoveToNewHeight(prevLvl);
            }
        }
    }

}
