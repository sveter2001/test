using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour
{
    //[SerializeField] private Body[] myBody = new Body[999];
    [SerializeField] public Sprite sprite11;
    [SerializeField] public Sprite sprite22;
    public Follow[] myBody = new Follow[999];
    
    [SerializeField] private EndWindow myEndWindow;

    private GameObject actual_food;
    private GameObject floor;
    private GameObject Snake;
    public GameObject body1;
    private GameObject Tail;
    public Follow fb1;
    public int length=1;
    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.Find("Grid");
        actual_food = GameObject.Find("Apple");
        Snake = GameObject.Find("SnakeInner");
        Tail = GameObject.Find("Tail");
        body1 = GameObject.Find("body1");
        fb1 = body1.GetComponent<Follow>();
        myBody[0]=fb1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Food"))
        {
           actual_food.transform.position = new Vector2(
                       (float)Mathf.Round((float)Random.Range(-26, 12)) + (float)0.5, 
                       (float)Mathf.Round((float)Random.Range(-31, 7)) + (float)0.5);
           
           Debug.Log(actual_food.transform.position);
           
           GameObject go1 = new GameObject();
           Follow fl1 = go1.AddComponent<Follow>();
           SpriteRenderer render = go1.AddComponent<SpriteRenderer>();
           Rigidbody2D rb = go1.AddComponent<Rigidbody2D>();
           rb.gravityScale = 0;
           
           BoxCollider2D bk = go1.AddComponent<BoxCollider2D>();
           bk.isTrigger = true;

           go1.transform.localScale = new Vector2((float)2.4,(float)2.4);
           go1.tag = "SelfBody";
           go1.name = "BodyNew"+length.ToString();
           go1.transform.position = Tail.transform.position;
           
           fl1.RF = myBody[length-1];
           myBody[length]=fl1;
           
           render.sortingOrder = 40;
           render.sprite = sprite11;
           length++; 
        }
        else if (col.CompareTag("SelfBody"))
        {
            Debug.Log("self eat");
            OpenWindow("gg wp");
        }
        
    }
    
    private void OpenWindow(string message){
        myEndWindow.gameObject.SetActive(true);
        myEndWindow.RestartBt.onClick.AddListener(RestartClicked);
        myEndWindow.ExitBt.onClick.AddListener(ExitClicked);
        myEndWindow.messageText.text = message;
    }
    
    private void RestartClicked(){
        myEndWindow.gameObject.SetActive(false);
        Debug.Log("Restart");
    }

    private void ExitClicked(){
        myEndWindow.gameObject.SetActive(false);
        Debug.Log("Exit");
    }

    public static Rect Get_Rect(GameObject gameObject)
    {
        if (gameObject != null)
        {
            RectTransform rectTransform =  gameObject.GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                return rectTransform.rect;
            }
        }
        else
        {
            Debug.Log("Game object is null.");
        }

        return new Rect();
    }

    public static float Get_Width(GameObject gameObject)
    {
        if (gameObject != null)
        {
            var rect = Get_Rect(gameObject);
            if (rect != null)
            {
                return rect.width;
            }
        }

        return 0;
    }

    public static float Get_Height(GameObject gameObject)
    {
        if (gameObject != null)
        {
            var rect = Get_Rect(gameObject);
            if (rect != null)
            {
                return rect.height;
            }
        }

        return 0;
    }
}
