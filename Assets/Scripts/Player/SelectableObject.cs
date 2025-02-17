using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SelectableObject : MonoBehaviour
{
    public bool IsSelected {  get; private set; }

    Rigidbody rigidbody;
    [SerializeField] Camera camera;

    private float x;    //���ړ�
    private float z;    //�O��ړ�
    private float speed = 5.0f; //�����X�s�[�h
    private float jumpPower = 100;  //�W�����v��
    private float gravityValue = -9.81f;    //�d��
    private float cameraSpeed = 10f;    //�J�����̈ړ��X�s�[�h
    private float progress = 0.0f;  //�J�����̐i�s�x����
    private bool isJumping = true; //�W�����v����
    private bool isEnter = false;   //��������

    private float distance;         //�J�����ƖڕW�_�̋���
    private Vector3 cameraPosition; //�J�����̏����ʒu
    private Vector3 goalPosition;   //�J�����̖ڕW�_

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
        EndLink(cameraSpeed);
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
    }

    public virtual void StartLink(float speed)  //MIDI�����ƘA���n��
    {
        
    }

    public virtual void EndLink(float speed)    //MIDI�����ƘA���I���
    {
        
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
        if (scene.name == "PlayScene")  //�V�[���؂�ւ����̏����ݒ�
        {
            transform.position = new Vector3(0, 0, -10);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rigidbody.useGravity = true;
            audioListener = GetComponent<AudioListener>();
            audioListener.enabled = true;

            //�J�����ݒ�
            camera.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 8);
            cameraPosition = camera.transform.position;
            goalPosition = camera.transform.position;
            distance = Vector3.Distance(cameraPosition, goalPosition);
        }
    }

    void Update()
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

    void FixedUpdate()
    {

        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            rigidbody.linearVelocity = new Vector3(x * speed, 0, z * speed);    //�ړ�

            if (isJumping)
            {
                AddGravity();   //�d��
            }

            //�J��������
            if (isEnter)    //������
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime * cameraSpeed / distance);
                camera.transform.position = Vector3.Lerp(cameraPosition, goalPosition, progress);
            }
            if (!isEnter)   //�����O
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime * cameraSpeed / distance);
                camera.transform.position = Vector3.Lerp(cameraPosition, goalPosition, progress);
                camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z - 8); //�J�����Ǐ]
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
        if (other.gameObject.CompareTag("Entrance"))    //�����ɓ�������
        {
            if (!isEnter) //��������
            {
                progress = 0.0f;
                cameraPosition = camera.transform.position;
                goalPosition = new Vector3(0, 5, 1);
                distance = Vector3.Distance(cameraPosition, goalPosition);
                isEnter = true;
            }
            else�@//�o����
            {
                progress = 0.0f;
                cameraPosition = camera.transform.position;
                goalPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 8);
                distance = Vector3.Distance(cameraPosition, goalPosition);
                isEnter = false;
            }
        }
    }
}
