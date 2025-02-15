using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectableObject : MonoBehaviour
{
    public bool IsSelected {  get; private set; }

    private Vector3 goalScale = new Vector3(2, 2, 2);
    Rigidbody rigidbody;
    [SerializeField] Camera camera;

    float x;    //�L�[�{�[�h����
    float z;    //�L�[�{�[�h����
    float speed = 5.0f; //�����X�s�[�h
    float jumpPower = 100;  //�W�����v��
    float gravityValue = -9.81f;
    bool isJumping = true; //�W�����v����


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        IsSelected = true;
        GameManager.Instance.HandleObjectSelection(this, IsSelected);   //�}�E�X�������ꂽ�I�u�W�F�N�g��I����Ԃ�
    }

    public virtual void RotateObject()
    {
        transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0)); //�I�u�W�F�N�g�̉�]
    }

    public virtual void ResetObject()
    {
        transform.rotation = Quaternion.identity; // �I�u�W�F�N�g�����ɖ߂�
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
    }

    public virtual void LargeSize(float speed)
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, goalScale, speed);
    }

    public virtual void SmallSize(float speed)
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1, 1, 1), speed);
    }


    /*----------------------------------*/
    /*            PlayScene             */
    /*----------------------------------*/
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rigidbody.useGravity = true;
            camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z -8); //�J�����Ǐ]

            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                Jump();     //�W�����v
            }
        }
    }

    public void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            rigidbody.linearVelocity = new Vector3(x * speed, 0, z * speed);    //�ړ�
        }

        if (isJumping)
        {
            AddGravity();
        }
    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        isJumping = true;
    }

    private void AddGravity()
    {
        rigidbody.AddForce(Vector3.up * gravityValue, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isJumping)
        {
            if (collision.gameObject.CompareTag("Ground"))  //���n����
            {
                isJumping = false;
            }
        }

        if (collision.gameObject.CompareTag("Entrance"))
        {
            camera.transform.position = collision.transform.position;
        }
    }
}
