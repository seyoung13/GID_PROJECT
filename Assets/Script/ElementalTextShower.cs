using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ElementalTextShower : MonoBehaviour
{

	public Text elementalText;
	public Camera camera;
	private Transform target;

	// Use this for initialization

	void Start()
	{
		target = GetComponent<Transform>();
	}

	// Update is called once per frame

	void Update()
	{
		Vector3 screenPos = camera.WorldToScreenPoint(target.position);
		float x = screenPos.x;

		elementalText.transform.position = new Vector3(x, screenPos.y, elementalText.transform.position.z);

	}

}