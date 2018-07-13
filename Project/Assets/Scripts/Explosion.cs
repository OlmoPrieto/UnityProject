using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

  public Rigidbody2D m_rigidbody;
  public Transform m_transform;
  public Vector2 m_direction = new Vector2();
  public float m_impulse = 1.0f;
  public float m_torque = 1.0f;
  public bool m_freeze_gravity = true;
  public bool m_physics_enabled = true;
  public bool m_has_started = false;

	// Use this for initialization
	void Start () {
		if (m_has_started == false) {
      customStart();
    }
	}

  public void customStart() {
    m_rigidbody = GetComponent<Rigidbody2D>();
    m_rigidbody.gravityScale = 0.0f;
    m_transform = GetComponent<Transform>();

    m_has_started = true;
  }
	
	// Update is called once per frame
	void Update () {
    if (m_freeze_gravity == true) {
      m_rigidbody.velocity = new Vector2(0.0f, 0.0f);
    }

    if (m_physics_enabled == true && 
      (m_rigidbody.velocity.x == 0.0f && m_rigidbody.velocity.y == 0.0f)) {
      m_physics_enabled = false;

      m_rigidbody.Sleep();
    }

		// if (Input.GetMouseButtonDown(0)) {
  //     explode();
  //   }
	}

  void OnCollisionEnter2D(Collision2D other) {
    if (other.collider.gameObject.tag == "Player") {
      Destroy(this.gameObject);
    }
  }

  public void explode() {
    m_freeze_gravity = false;
    m_rigidbody.gravityScale = 1.0f;

    float random = Random.value + 0.2f;// * 2.0f - 0.5f;
    m_direction.y = random;

    m_rigidbody.AddForce(m_direction * m_impulse, ForceMode2D.Impulse);
    m_rigidbody.AddTorque(m_torque + Random.value * 0.1f, ForceMode2D.Impulse);
  }
}
