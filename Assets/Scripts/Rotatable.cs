using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Rotatable : MonoBehaviour
{
	[SerializeField] private InputAction pressed, axis;
	public bool rotateAllowed;
	private Transform cam;
	[SerializeField] private float speed = 1;
	[SerializeField] private bool inverted;
	private Vector2 rotation;
	private bool inRotation;
	private void Awake()
	{
		cam = Camera.main.transform;
		pressed.Enable();
		axis.Enable();
		pressed.performed += _ => { StartCoroutine(Rotate()); };
		pressed.canceled += _ => { inRotation = false; };
		axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
	}

	private IEnumerator Rotate()
	{
		if (!rotateAllowed) yield break;
		inRotation = true;
		while (inRotation)
		{
			// apply rotation
			rotation *= speed;
			transform.Rotate(Vector3.up * (inverted ? 1 : -1), rotation.x, Space.World);
			transform.Rotate(cam.right * (inverted ? -1 : 1), rotation.y, Space.World);
			yield return null;
		}
	}
}
