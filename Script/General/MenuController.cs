using System.Collections;
using UnityEngine;

public class MenuController : MonoBehaviour {

    [Header("Variables")]
    public float speedMoveMenu;
    private int index;
    private int newIndex;
    private bool inMove;

    [Header("Object UI")]
    public GameObject[] screen; // data, games, market, profile, config, changeAvatar
    public GameObject objectLeft, objectCenter, objectRight;
    private RectTransform rectLeft, rectCenter, rectRight;

    [Header("RB")]
    private Rigidbody2D rb2dLeft, rb2dRight, rb2dCenter;

    private void Start()
    {
        rb2dLeft = objectLeft.GetComponent<Rigidbody2D>();
        rb2dCenter = objectCenter.GetComponent<Rigidbody2D>();
        rb2dRight = objectRight.GetComponent<Rigidbody2D>();

        rectLeft = objectLeft.GetComponent<RectTransform>();
        rectCenter = objectCenter.GetComponent<RectTransform>();
        rectRight = objectRight.GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        if(inMove)
        {
            if(newIndex > index)
            {
                rb2dCenter.velocity = new Vector2(-speedMoveMenu * Time.fixedDeltaTime, rb2dCenter.velocity.y);
                rb2dRight.velocity = new Vector2(-speedMoveMenu * Time.fixedDeltaTime, rb2dRight.velocity.y);

                if (objectRight.transform.position.x <= 0)
                {
                    rb2dRight.velocity = Vector2.zero;
                    objectRight.transform.position = new Vector3(0, objectRight.transform.position.y, 0);
                }
            }
            else if(newIndex < index)
            {
                rb2dCenter.velocity = new Vector2(speedMoveMenu * Time.fixedDeltaTime, rb2dCenter.velocity.y);
                rb2dLeft.velocity = new Vector2(speedMoveMenu * Time.fixedDeltaTime, rb2dLeft.velocity.y);

                if(objectLeft.transform.position.x >= Screen.width)
                {
                    rb2dLeft.velocity = Vector2.zero;
                    objectLeft.transform.position = new Vector3(Screen.width, objectLeft.transform.position.y, 0);
                }
            }
        }
        else
        {
            rb2dCenter.velocity = Vector2.zero;
        }
    }
    public void ChangeScreen(int id)
    {


        screen[index].transform.SetParent(objectCenter.transform);

        StopAllCoroutines();
        StartCoroutine(Changes(id));
    }
    private IEnumerator Changes(int id)
    {
        newIndex = id;
        inMove = true;

        if (id > index)
        {
            screen[id].transform.SetParent(objectRight.transform);
        }
        else if (id < index)
        {
            screen[id].transform.SetParent(objectLeft.transform);
        }

        yield return new WaitForSeconds(2);

        index = id;
        inMove = false;
    }
}
