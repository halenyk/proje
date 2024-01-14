using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UnityEditor.SearchService;

public class PlayerController : MonoBehaviour
{

	private Animator anim;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Camera cam;

	float camWidth;
	public float speed;
	public float direction;
	private float move;
	public bool isRunning;

	public int skor;
	public int topSkor;
	public int can;
	public TextMeshProUGUI skorText;
	public TextMeshProUGUI canText;
	public TextMeshProUGUI bestSkor;

    void Start()
	{
		skor = 0;
		can = 3;
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		isRunning = false;
		speed = 10f;
	}
	public void Run()
	{
		isRunning = true;
        anim.SetBool("Run" , isRunning);
	}
	public void RunOff()
	{
		isRunning = false;
        anim.SetBool("Run", isRunning);
	}

	void Update()
	{
		direction = Input.GetAxis("Horizontal");
		Flip();
        AnimatonHandle();
		skorText.text = "Skor: " + skor.ToString();
		canText.text = "Can: " + can.ToString();

		if(PlayerPrefs.GetInt("TopSkor") < skor)
			PlayerPrefs.SetInt("TopSkor", skor);
        bestSkor.text = "En Yüksek Skor: " + PlayerPrefs.GetInt("TopSkor").ToString();

        if (can <= 0)
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

		cam = Camera.main;
		camWidth = 2f * cam.orthographicSize * cam.aspect;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -camWidth / 2, camWidth / 2), transform.position.y, 1);
    }

    private void FixedUpdate()
    {
        move = direction * speed * Time.fixedDeltaTime * 35;
        rb.velocity = new Vector2(move, rb.velocity.y);
    }

    private void AnimatonHandle()
    {
		if ((direction > 0 || direction < 0) && !isRunning)
		{
			Run();
		}
		else if (direction == 0 && isRunning)
		{
			RunOff();
		}
    }

    public void Flip()
	{
        if (direction > 0)
        {
			sr.flipX = false;
        }
        else if (direction < 0)
        {
			sr.flipX = true;
        }
    }
}



