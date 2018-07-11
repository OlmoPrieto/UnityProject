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
  public bool m_touching_right = false;
  public bool m_touching_left = false;
  public bool m_was_walljumping = false;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
    float movement = Input.GetAxisRaw("Horizontal");

    Vector2 velocity = m_rigidbody.velocity;
    float speed = m_speed;

    if (Input.GetKey(KeyCode.Space) && m_grounded == true) {
      m_grounded = false;
      m_can_jump = false;
      
      if (m_touching_wall == true) {
        m_was_walljumping = true;
        velocity.x = m_x_wall_force;
      }

      velocity.y = m_jump_force;

      StartCoroutine(ResetJumpRoutine());
    }

    if (m_was_walljumping == false) {
      velocity.x = movement * speed * Time.deltaTime;
    }
    if (movement < 0 && m_touching_left || movement > 0 && m_touching_right) {
      if (m_was_walljumping == false) {
        velocity.x = 0;
      }
    }

    m_rigidbody.velocity = velocity;
  }

  private void setWallsFlags(Collision2D other) {
    m_touching_wall = true;

    // Check in which side of a wall you are
    if (transform.position.x < other.transform.position.x) {
      m_touching_right = true;
      m_touching_left = false;
      if (m_x_wall_force > 0) {
        m_x_wall_force = -m_x_wall_force;
      }
    }
    else {
      m_touching_right = false;
      m_touching_left = true;
      if (m_x_wall_force < 0) {
        m_x_wall_force = -m_x_wall_force;
      }
    }
  }

  void OnCollisionEnter2D(Collision2D other) {
    ContactPoint2D[] contacts = new ContactPoint2D[4];
    int num_contacts = other.GetContacts(contacts);

    m_was_walljumping = false;

    // Floors
    if (other.collider.gameObject.layer == 9) {
      m_grounded = true;

      if (contacts[0].point.x == contacts[1].point.x) {
        // Touched the left/right part of a floor,
        // behave like in a wall
        setWallsFlags(other);
      }
    }

    // Walls
    if (other.collider.gameObject.layer == 10) {
      setWallsFlags(other);

      m_grounded = true;

      if (contacts[0].point.y == contacts[1].point.y) {
        // Touched the upper/lower part of a wall,
        // behave like in the floor
        m_touching_wall = false;
        m_touching_right = false;
        m_touching_left = false;
      }
    }

    // for (int i = 0; i < num_contacts; ++i) {
    //   Debug.Log("x: " + contacts[i].point.x + "  y: " + contacts[i].point.y);
    // }
  }

  void OnCollisionExit2D(Collision2D other) {
    // Floors
    if (other.collider.gameObject.layer == 9) {
      m_grounded = false;

      m_touching_wall = false;
      m_touching_right = false;
      m_touching_left = false;
    }

    // Walls
    if (other.collider.gameObject.layer == 10) {
      m_touching_wall = false;
      m_touching_right = false;
      m_touching_left = false;
    }
  }

  IEnumerator ResetJumpRoutine() {
    yield return new WaitForSeconds(0.2f);
    //m_can_jump = true;
    m_was_walljumping = false;
    m_rigidbody.velocity = new Vector2(0.0f /*m_rigidbody.velocity.x * 0.2f*/, 
      m_rigidbody.velocity.y);
  }
}
