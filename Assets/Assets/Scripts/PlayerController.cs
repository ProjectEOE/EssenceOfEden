using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Camera _cameraMain;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _sprintSpeed;
    [SerializeField]
    private float _jumpSpeed;
    [SerializeField]
    private float _speedH = 2.0f;
    [SerializeField]
    private float _speedV = 2.0f;

    private float _yaw = 0.0f;
    private float _pitch = 0.0f;
    private bool _isOnGround;
    private Vector3 _movement;
    private Vector3 _mousePos;
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
        Look();
	}

    private void Move()
    {

        if (_isOnGround)
        {
            Vector3 verticalDir;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
            {
                verticalDir = -transform.up * Input.GetAxis("Vertical") * _sprintSpeed * Time.deltaTime;
            }
            else
            {
                verticalDir = -transform.up * Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
            }
            Vector3 horizontalDir = transform.right * Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
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
        _yaw += _speedH * Input.GetAxis("Mouse X");
        _pitch -= _speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
    }

    static void ToggleMouseOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    static void ToggleMouseOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
