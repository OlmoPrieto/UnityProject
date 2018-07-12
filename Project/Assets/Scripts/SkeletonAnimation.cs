using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimation : MonoBehaviour {

  public Animator m_animator;
  public Transform m_bones_transform;

  public float m_change_animation_time = 5.0f;
  public float m_acc_time = 0.0f;

  public bool m_flag = false;

	// Use this for initialization
	void Start () {
    m_acc_time = Random.value * 2.0f;
		m_animator = GetComponent<Animator>();
    m_bones_transform = transform.root.GetChild(0).GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
    if (m_flag == true) {
      m_acc_time += Time.deltaTime;
      if (m_acc_time >= m_change_animation_time) {
        m_acc_time = Random.value * 1.5f;

        m_animator.SetTrigger("Die");
      }
    }
	}

  public void Land() {
    m_animator.SetBool("Grounded", true);
    m_flag = true;
  }

  public void Die() {
    this.gameObject.SetActive(false);
    m_bones_transform.gameObject.SetActive(true);
  }
}
