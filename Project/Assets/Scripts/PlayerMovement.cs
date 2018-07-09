using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public Rigidbody2D m_rigidbody;
  public float m_speed = 5.0f;
  public float m_jump_force = 5.0f;
  public float m_x_wall_force = 1.0f;

  public bool m_grounded = false;
  public bool m_can_jump = true;
  public bool m_touching_wall = false;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
    float movement = Input.GetAxisRaw("Horizontal");

    Vector2 velocity = m_rigidbody.velocity;
    float speed = m_speed;

    velocity.x = movement * speed * Time.deltaTime;

    //m_rigidbody.velocity = new Vector2(movement * m_speed * Time.deltaTime, m_rigidbody.velocity.y);

    if (Input.GetKey(KeyCode.Space) && m_grounded == true) {
      m_grounded = false;
      m_can_jump = false;

      //m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_jump_force);
      velocity.x = m_x_wall_force;
      velocity.y = m_jump_force;

      StartCoroutine(ResetJumpRoutine());
    }

    m_rigidbody.velocity = velocity;
  }

  void OnCollisionEnter2D(Collision2D other) {
    if (other.collider.gameObject.layer == 9) {
      m_grounded = true;
    }

    if (other.collider.gameObject.layer == 10) {
      m_x_wall_force = 1.0f;
      if (transform.position.x < other.transform.position.x) {
        m_x_wall_force = -m_x_wall_force;
      }

      m_grounded = true;
    }
  }

  void OnCollisionExit2D(Collision2D other) {
    if (other.collider.gameObject.layer == 9) {
      m_grounded = false;
    }

    if (other.collider.gameObject.layer == 10) {
      m_grounded = false;
      m_x_wall_force = 0.0f;
    }
  }

  IEnumerator ResetJumpRoutine() {
    yield return new WaitForSeconds(0.2f);
    m_can_jump = true;
  }
}
