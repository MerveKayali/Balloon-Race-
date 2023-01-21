using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
	public float fallTime = 0.5f;


	void OnCollisionEnter(Collision collision)
	{
				
			//Debug.DrawRay(contact.point, contact.normal, Color.white);
			if (collision.gameObject.tag == "Runner")
			{
				StartCoroutine(Fall(fallTime));
			}
		
	}

	IEnumerator Fall(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
