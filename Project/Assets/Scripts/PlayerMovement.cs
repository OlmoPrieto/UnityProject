using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public Rigidbody2D m_rigidbody;
  public float m_speed = 5.0f;
  public float m_jump_force = 5.0f;

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

    if (Input.GetKey(KeyCode.Space) && m_grounded == true) {
      m_grounded = false;
      m_can_jump = false;

      m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_jump_force);

      StartCoroutine(ResetJumpRoutine());
    }

    Debug.DrawRay(transform.position, Vector2.down * 0.5f, Color.red, 0.0f);
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, ~(1 << 8));
    if (hit.collider != null) {
      if (m_can_jump == true) {
        m_grounded = true;
      }
    }
  }

  IEnumerator ResetJumpRoutine() {
    yield return new WaitForSeconds(0.2f);
    m_can_jump = true;
  }
}
