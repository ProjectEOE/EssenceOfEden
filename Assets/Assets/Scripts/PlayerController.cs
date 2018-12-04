using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Camera _cameraMain;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _jumpSpeed;

    private bool _isOnGround;
    private Vector3 _movement;
    private Rigidbody _rigid;
	void Start ()
    {
        _isOnGround = false;
        _rigid = GetComponent<Rigidbody>();
        ToggleMouseOff();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            _isOnGround = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        Move();
	}

    private void Move()
    {

        if (_isOnGround)
        {
            Vector3 horizontalDir = transform.right * Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
            Vector3 verticalDir = -transform.up * Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
            _movement = horizontalDir + verticalDir;
        }
        _rigid.MovePosition(transform.position + _movement);
        if (Input.GetKey(KeyCode.Space) && _isOnGround == true)
        {
            _isOnGround = false;
            _rigid.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
        }

    }

    private void Look()
    {
        
    }

    static void ToggleMouseOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
