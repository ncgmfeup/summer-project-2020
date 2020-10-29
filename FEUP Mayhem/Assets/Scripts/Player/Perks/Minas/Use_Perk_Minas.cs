using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use_Perk_Minas : MonoBehaviour
{
    string perkButtonName;
    string[] layers = { "Platform", "Ground" };
    RaycastHit2D raycast;

    GameObject wallPrefab;

    GameObject wall;

    [SerializeField] float cooldown = 15f;

    float delta_y = 0;

    [SerializeField]
    float offset_x_times_wall_size = 2f;

    bool canUseWall = true;

    // Start is called before the first frame update
    void Start()
    {
        perkButtonName = GetComponent<PlayerSpecs>().PerkButtonName();
        wallPrefab = Resources.Load<GameObject>("Prefabs/Wall_Perk");
        delta_y = wallPrefab.GetComponent<BoxCollider2D>().size.y
                  / 2f
                  * wallPrefab.transform.localScale.y;
        offset_x_times_wall_size *= wallPrefab.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(perkButtonName))
        {
            if(wall == null && canUseWall) // Builds wall
            {
                raycast = Physics2D.Raycast(transform.position + new Vector3(offset_x_times_wall_size, 0, 0), new Vector2(0, -1), Mathf.Infinity, LayerMask.GetMask(layers));
                wall = Instantiate(wallPrefab, new Vector3(transform.position.x + offset_x_times_wall_size * (((int) transform.rotation.eulerAngles.y % 179) - 0.5f) * -2f , raycast.collider.transform.position.y + delta_y, transform.position.z), Quaternion.identity);
                StartCoroutine(Cooldown());
            }
            else if(wall != null) // Destroys wall
            {
                Destroy(wall);
            }
        } 
    }

    IEnumerator Cooldown()
    {
        canUseWall = false;
        yield return new WaitForSeconds(cooldown);
        canUseWall = true;
    }
}
