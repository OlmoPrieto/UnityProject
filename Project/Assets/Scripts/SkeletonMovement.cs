using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour {

  public Rigidbody2D m_rigidbody;
  public SkeletonAnimation m_animation;

  public bool m_grounded = false;

	// Use this for initialization
	void Start () {
    m_rigidbody = GetComponent<Rigidbody2D>();
    m_animation = GetComponent<SkeletonAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnCollisionEnter2D(Collision2D other) {
    // Floors
    if (other.collider.gameObject.layer == 9) {
      m_grounded = true;
      m_animation.Land();
    }
  }

  void OnCollisionExit2D(Collision2D other) {
    // Floors
    if (other.collider.gameObject.layer == 9) {
      m_grounded = false;
      //m_rigidbody.velocity = new Vector2(0.0f, m_rigidbody.velocity.y);
      m_animation.Fall();
    }
  }
}
