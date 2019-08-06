using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    Player player;
    public Vector3 offSet;
    // Start is called before the first frame update
    public void Init()
    {
        player = PlayerManager.Instance.player;
        transform.position = player.transform.position + offSet;
        transform.LookAt(player.transform);
    }

    // Update is called once per frame
    public void CameraUpdate()
    {
        float dir =0;
        transform.position = player.transform.position + offSet;
        if (InputManager.Instance.inputPressed.mousePos.x > 0.75f)
        {
            dir = -1;
        }

        if (InputManager.Instance.inputPressed.mousePos.x < 0.25f)
        {
            dir = 1;
        }

        if (dir !=0)
        {
            transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), 90f * Time.deltaTime * dir);
            transform.LookAt(player.transform);
            offSet = transform.position - player.transform.position;
        }

        transform.LookAt(player.transform);
    }
}
