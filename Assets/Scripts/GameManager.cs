using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }    //�I����Ԃ̊Ǘ�

    [SerializeField] SignalReceiver signalReceiver;
    [SerializeField] GameObject spotLight;
    [SerializeField] Camera camera;
    private SelectableObject selectableObject;  //�I����Ԃ̃I�u�W�F�N�g

    private Vector3 cameraPosition; //�J�����̏����ʒu
    private Vector3 goalPosition;   //�J�����̏I���_
    private float distance;         //�����ʒu�ƏI���_�̋���
    private float speed = 10f;      //�J�����̈ړ��X�s�[�h
    private float progress = 0.0f;  //�J�����̐i�s�x����

    [SerializeField] SEManager seManager;

    private void Awake()
    {
        Instance = this;    //�V���O���g���̃C���X�^���X�ݒ�
    }

    void Start()
    {
        cameraPosition = camera.transform.position;
        goalPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 5);
        distance = Vector3.Distance(cameraPosition, goalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectObject();   //Escape�L�[�őI������
        }

        if(selectableObject != null)    //�I����Ԃł����
        {
            spotLight.transform.position = new Vector3(selectableObject.transform.position.x, spotLight.transform.position.y, spotLight.transform.position.z);
            spotLight.SetActive(true);  //�X�|�b�g���C�g�̓_��

            selectableObject.RotateObject();    //�I����Ԃ̃I�u�W�F�N�g����]

            signalReceiver.GetObject(selectableObject);

            progress = Mathf.Clamp01(progress + Time.deltaTime * speed / distance);
        }
        else  //�I����ԂɂȂ����
        {
            spotLight.SetActive(false); //�X�|�b�g���C�g�̏���

            progress = Mathf.Clamp01(progress - Time.deltaTime * speed / distance);
        }

        //�J�����ړ�
        camera.transform.position = Vector3.Lerp(cameraPosition, goalPosition, progress);

        // �J�����̉�]
        float rotationAngle = Mathf.Lerp(0, 15, progress);
        camera.transform.rotation = Quaternion.Euler(rotationAngle, 0, 0);

        if(selectableObject != null && Input.GetKeyDown(KeyCode.Return))    //�I�u�W�F�N�g�����肵����V�[���ړ�
        {
            DontDestroyOnLoad(seManager);
            seManager.EnterSound(); //SE

            DontDestroyOnLoad(camera);
            DontDestroyOnLoad(selectableObject);
            SceneManager.LoadScene("PlayScene");
        }
    }

    public void HandleObjectSelection(SelectableObject objectToSelect, bool IsSelected)
    {
        if (IsSelected) //�I�����ꂽ��
        {
            if(selectableObject != null && !selectableObject.IsSelected)    //���ɑI���ς݂̂��Ȃ���
            {
                selectableObject.SetSelected(false);
            }

            seManager.SelectSound();    //SE
            selectableObject = objectToSelect;
            goalPosition = new Vector3(objectToSelect.transform.position.x, objectToSelect.transform.position.y + 2, objectToSelect.transform.position.z - 5);
            distance = Vector3.Distance(cameraPosition, goalPosition);
        }
        else  //�I�������̂Ƃ�
        {
            DeselectObject();
        }
    }

    private void DeselectObject()   //�I������
    {
        if(selectableObject != null)
        {
            selectableObject.ResetObject();
        }
        selectableObject = null;
        signalReceiver.GetObject(selectableObject);
    }
}
