using UnityEngine;
using UnityEngine.Playables;
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
    bool isEnter = false;

    private AudioListener audioListener;

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
        EndLink(speed);
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
    }

    public virtual void StartLink(float speed)
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, goalScale, speed);
    }

    public virtual void EndLink(float speed)
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1, 1, 1), speed);
    }


    /*----------------------------------*/
    /*            PlayScene             */
    /*----------------------------------*/

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayScene")  //�����ݒ�
        {
            transform.position = new Vector3(0, 0, -10);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rigidbody.useGravity = true;
            audioListener = GetComponent<AudioListener>();
            audioListener.enabled = true;
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
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
        if (!isEnter)   //�����O
        {
            camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z - 8); //�J�����Ǐ]
        }

        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            rigidbody.linearVelocity = new Vector3(x * speed, 0, z * speed);    //�ړ�

            if (isJumping)
            {
                AddGravity();   //�d��
            }
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Entrance"))
        {
            if (!isEnter)
            {
                camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(transform.position.x, camera.transform.position.y, transform.position.z), 3);
                isEnter = true;
            }
            else
            {
                camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(transform.position.x, camera.transform.position.y, transform.position.z - 8), 3);
                isEnter = false;
            }
        }
    }
}
