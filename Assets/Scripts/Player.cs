using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    Rigidbody2D rigid;
    public Button uiButton;
    public Sprite defaultButtonImage;
    public Sprite changedButtonImage;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        
        Manager.Input.KeyAction -= OnKeyboard;
        Manager.Input.KeyAction += OnKeyboard;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Doll")
        {
            Debug.Log("충돌 시작!");
            ChangeButtonImage(changedButtonImage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Doll")
        {
            Debug.Log("충돌 중!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Doll")
        {
            Debug.Log("충돌 종료!");
            ChangeButtonImage(defaultButtonImage);
        }
    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void ChangeButtonImage(Sprite shape)
    {
        Image buttonImage = uiButton.GetComponent<Image>();

        buttonImage.sprite = shape;
    }
}
