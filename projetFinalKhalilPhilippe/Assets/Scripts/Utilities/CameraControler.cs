using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    Player player;
    public float offSetY;
    public float offSetZ;
    float dir;
    // Start is called before the first frame update
    public void Init()
    {
        player = PlayerManager.Instance.player;
        transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + offSetY), (player.transform.position.z - offSetZ));
        transform.LookAt(player.transform);
    }

    // Update is called once per frame
    public void CameraUpdate()
    {
        if ((InputManager.Instance.inputPressed.deltaMouse.x - player.transform.position.x) > 1)
        {
            dir = -1;
        }

        if ((InputManager.Instance.inputPressed.deltaMouse.x - player.transform.position.x) < -1)
        {
            dir = 1;
        }

        if ((InputManager.Instance.inputPressed.deltaMouse.x - player.transform.position.x) > -1 && (InputManager.Instance.inputPressed.deltaMouse.x - player.transform.position.x) < 1)
        {
            dir = 0;
        }

        transform.position = new Vector3(player.transform.position.x + dir, (player.transform.position.y + offSetY), (player.transform.position.z - offSetZ));
        transform.LookAt(player.transform);
    }
}
