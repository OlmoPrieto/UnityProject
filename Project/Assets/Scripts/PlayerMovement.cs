using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public Rigidbody2D m_rigidbody;
  public float m_speed = 5.0f;

  public bool m_grounded = false;
  public bool m_can_jump = true;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
    float movement = Input.GetAxisRaw("Horizontal");
    m_rigidbody.velocity = new Vector2(movement * m_speed * Time.deltaTime, m_rigidbody.velocity.y);
	
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);
    if (hit != null) {

    }

    if (Input.GetKey(KeyCode.Space && m_grounded == true)) {
      m_grounded = false;
      m_can_jump = false;

      m_rigidbody.velocity = new Vector2()
    }
  }
}
