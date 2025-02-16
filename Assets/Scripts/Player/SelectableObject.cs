using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SelectableObject : MonoBehaviour
{
    public bool IsSelected {  get; private set; }

    private Vector3 goalScale = new Vector3(2, 2, 2);
    Rigidbody rigidbody;
    [SerializeField] Camera camera;

    float x;    //キーボード入力
    float z;    //キーボード入力
    float speed = 5.0f; //動くスピード
    float jumpPower = 100;  //ジャンプ力
    float gravityValue = -9.81f;
    bool isJumping = true; //ジャンプ中か
    bool isEnter = false;

    private AudioListener audioListener;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        IsSelected = true;
        GameManager.Instance.HandleObjectSelection(this, IsSelected);   //マウスが押されたオブジェクトを選択状態に
    }

    public virtual void RotateObject()
    {
        transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0)); //オブジェクトの回転
    }

    public virtual void ResetObject()
    {
        transform.rotation = Quaternion.identity; // オブジェクトを元に戻す
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
        if (scene.name == "PlayScene")  //初期設定
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
                Jump();     //ジャンプ
            }
        }
    }

    public void FixedUpdate()
    {
        if (!isEnter)   //建物外
        {
            camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z - 8); //カメラ追従
        }

        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            rigidbody.linearVelocity = new Vector3(x * speed, 0, z * speed);    //移動

            if (isJumping)
            {
                AddGravity();   //重力
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
            if (collision.gameObject.CompareTag("Ground"))  //着地判定
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
