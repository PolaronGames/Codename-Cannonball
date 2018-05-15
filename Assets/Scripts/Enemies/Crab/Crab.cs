using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{

	private float waitingTime = 1f;
	private float timer = 0;
	private Animator leftArmAnimator;
	private Animator rightArmAnimator;

    // Use this for initialization
    void Awake()
    {
		leftArmAnimator = this.transform.Find("left_arm").gameObject.GetComponent<Animator>();
		rightArmAnimator = this.transform.Find("right_arm").gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
		if(timer > waitingTime)
		{
            leftArmAttack();
			rightArmAttack();
			timer = -3;
		}
    }

	private void leftArmAttack()
	{
        leftArmAnimator.SetTrigger("Left_Attacking");
    }

	private void rightArmAttack()
	{
        rightArmAnimator.SetTrigger("Right_Attacking");
    }
}
