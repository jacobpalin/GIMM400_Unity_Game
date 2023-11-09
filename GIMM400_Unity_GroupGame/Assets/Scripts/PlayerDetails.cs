using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetails : MonoBehaviour
{
    public int playerID;
    public Vector3 startPos;

    void start()
    {
        StartCoroutine(MoveSpawn());
    }
    IEnumerator MoveSpawn(){
        yield return new WaitForSeconds(.1f);
        gameObject.transform.position = startPos;

    }
}
