using UnityEngine;

public class Controller : MonoBehaviour {
	[Header("Constants")]
	//unity controls and constants input
	public float accelerationMod;

	public float xAxisSensitivity;
	public float yAxisSensitivity;
	public float decelerationMod;

	[Space] [Range(0, 89)] public float MaxXAngle = 60f;

	[Space] public float maximumMovementSpeed = 1f;

	[Header("Controls")] public KeyCode forwards = KeyCode.W;
	public KeyCode backwards = KeyCode.S;
	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	public KeyCode up = KeyCode.Q;
	public KeyCode down = KeyCode.E;

	private Vector3 _moveSpeed;
	private float _rotationX;

	private void Start() {
		_moveSpeed = Vector3.zero;
	}

	// Update is called once per frame
	private void Update() {
		//Press the space bar to apply no locking to the Cursor
		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
		
		if (Cursor.lockState == CursorLockMode.None) return;
			
		HandleMouseRotation();

		var acceleration = HandleKeyInput();

		_moveSpeed += acceleration;

		HandleDeceleration(acceleration);

		// clamp the move speed
		if (_moveSpeed.magnitude > maximumMovementSpeed) {
			_moveSpeed = _moveSpeed.normalized * maximumMovementSpeed;
		}

		transform.Translate(_moveSpeed);
	}

	private Vector3 HandleKeyInput() {
		var acceleration = Vector3.zero;

		//key input detection
		if (Input.GetKey(forwards)) {
			acceleration.z += 1;
		}

		if (Input.GetKey(backwards)) {
			acceleration.z -= 1;
		}

		if (Input.GetKey(left)) {
			acceleration.x -= 1;
		}

		if (Input.GetKey(right)) {
			acceleration.x += 1;
		}

		if (Input.GetKey(up)) {
			acceleration.y += 1;
		}

		if (Input.GetKey(down)) {
			acceleration.y -= 1;
		}

		return acceleration.normalized * accelerationMod;
	}

	/**
	 * Handles the mouse input and applies a rotation to the camera.
	 */
	private void HandleMouseRotation() {
		// mouse input
		var rotationHorizontal = xAxisSensitivity * Input.GetAxis("Mouse X");
		var rotationVertical = yAxisSensitivity * Input.GetAxis("Mouse Y");

		// applying mouse rotation
		// always rotate Y in global world space to avoid gimbal lock
		Transform transformTemp;
		(transformTemp = transform).Rotate(Vector3.up * rotationHorizontal, Space.World);

		var rotationY = transformTemp.localEulerAngles.y;

		_rotationX += rotationVertical;
		_rotationX = Mathf.Clamp(_rotationX, -MaxXAngle, MaxXAngle);

		transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
	}

	private void HandleDeceleration(Vector3 acceleration) {
		//deceleration functionality
		if (Mathf.Approximately(Mathf.Abs(acceleration.x), 0)) {
			if (Mathf.Abs(_moveSpeed.x) < decelerationMod) {
				_moveSpeed.x = 0;
			}
			else {
				_moveSpeed.x -= decelerationMod * Mathf.Sign(_moveSpeed.x);
			}
		}

		if (Mathf.Approximately(Mathf.Abs(acceleration.y), 0)) {
			if (Mathf.Abs(_moveSpeed.y) < decelerationMod) {
				_moveSpeed.y = 0;
			}
			else {
				_moveSpeed.y -= decelerationMod * Mathf.Sign(_moveSpeed.y);
			}
		}

		if (Mathf.Approximately(Mathf.Abs(acceleration.z), 0)) {
			if (Mathf.Abs(_moveSpeed.z) < decelerationMod) {
				_moveSpeed.z = 0;
			}
			else {
				_moveSpeed.z -= decelerationMod * Mathf.Sign(_moveSpeed.z);
			}
		}
	}
	
	void OnGUI() {
		//Press this button to lock the Cursor
		if (GUI.Button(new Rect(0, 0, 100, 50), "Lock Cursor")) {
			Cursor.lockState = CursorLockMode.Locked;
		}

		//Press this button to confine the Cursor within the screen
		if (GUI.Button(new Rect(0, 50, 100, 50), "Confine Cursor")) {
			Cursor.lockState = CursorLockMode.Confined;
		}
	}
}