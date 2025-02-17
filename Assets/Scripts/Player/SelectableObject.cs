using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SelectableObject : MonoBehaviour
{
    public bool IsSelected {  get; private set; }

    Rigidbody rigidbody;
    [SerializeField] Camera camera;

    private float x;    //横移動
    private float z;    //前後移動
    private float speed = 5.0f; //動くスピード
    private float jumpPower = 100;  //ジャンプ力
    private float gravityValue = -9.81f;    //重力
    private float cameraSpeed = 10f;    //カメラの移動スピード
    private float progress = 0.0f;  //カメラの進行度合い
    private bool isJumping = true; //ジャンプ中か
    private bool isEnter = false;   //建物内か

    private float distance;         //カメラと目標点の距離
    private Vector3 cameraPosition; //カメラの初期位置
    private Vector3 goalPosition;   //カメラの目標点

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
        EndLink(cameraSpeed);
    }

    public void SetSelected(bool selected)
    {
        IsSelected = selected;
    }

    public virtual void StartLink(float speed)  //MIDI音源と連動始め
    {
        
    }

    public virtual void EndLink(float speed)    //MIDI音源と連動終わり
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
        if (scene.name == "PlayScene")  //シーン切り替え時の初期設定
        {
            transform.position = new Vector3(0, 0, -10);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rigidbody.useGravity = true;
            audioListener = GetComponent<AudioListener>();
            audioListener.enabled = true;

            //カメラ設定
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
                Jump();     //ジャンプ
            }
        }
    }

    void FixedUpdate()
    {

        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            rigidbody.linearVelocity = new Vector3(x * speed, 0, z * speed);    //移動

            if (isJumping)
            {
                AddGravity();   //重力
            }

            //カメラ操作
            if (isEnter)    //建物内
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime * cameraSpeed / distance);
                camera.transform.position = Vector3.Lerp(cameraPosition, goalPosition, progress);
            }
            if (!isEnter)   //建物外
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime * cameraSpeed / distance);
                camera.transform.position = Vector3.Lerp(cameraPosition, goalPosition, progress);
                camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z - 8); //カメラ追従
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
        if (other.gameObject.CompareTag("Entrance"))    //建物に入ったか
        {
            if (!isEnter) //入ったら
            {
                progress = 0.0f;
                cameraPosition = camera.transform.position;
                goalPosition = new Vector3(0, 5, 1);
                distance = Vector3.Distance(cameraPosition, goalPosition);
                isEnter = true;
            }
            else　//出たら
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
