using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimation : MonoBehaviour {

  public Animator m_animator;

  public float m_change_animation_time = 5.0f;
  public float m_acc_time = 0.0f;

	// Use this for initialization
	void Start () {
    m_acc_time = Random.value * 2.0f;
		m_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		m_acc_time += Time.deltaTime;
    if (m_acc_time >= m_change_animation_time) {
      m_acc_time = Random.value * 1.5f;

      m_animator.SetTrigger("Attacking");
    }
	}
}
