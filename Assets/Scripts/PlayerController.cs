using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    //[SerializeField]
    //private float crouchSpeed;

    private float applySpeed;

    [SerializeField]
    private float jumpForce;

    // 상태 변수
    private bool isWalk = false;
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;

    // 움직임 체크 변수
    private Vector3 lastPos;

    // 땅 착지 여부 체크를 위한 컴포넌트
    private CapsuleCollider capsuleCollider;

    // 앉았을 때 얼마나 앉을지 결정하는 변수
    //[SerializeField]
    // private float crouchPosY;
    // private float originPosY;
    // private float applyCrouchPosY;

    // 카메라 민감도
    [SerializeField]
    private float lookSensitivity;

    // 카메라 한계
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    // 필요한 컴포넌트
    [SerializeField]
    private Camera theCamera;
    [SerializeField]
    private Animator anim;      // 애니메이션
    private Rigidbody myRigid; //플레이어의 실질적인 몸, Rigidbody가 Collider에 물리학을 입힌다.
    private Vector3 _velocity;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        
        // 초기화
        applySpeed = walkSpeed;
        //originPosY = theCamera.transform.localPosition.y;
        //applyCrouchPosY = originPosY;
    }

    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        //TryCrouch();
        Move();
        MoveCheck();
        CameraRotation();
        CharacterRotation();
    }

    private void Move(){
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed; // 정규화 시키면 벡터의 합이 1이므로 추후 연산에 좋음
        
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void MoveCheck(){
        if(!isRun && !isCrouch && isGround){
            if (_velocity.magnitude >= 0.01f){
                isWalk = true;
                anim.SetBool("Walk", isWalk);
            }
            else{
                isWalk = false;
                anim.SetBool("Walk", isWalk);
            }
            lastPos = transform.position;
        }
    }

    private void CameraRotation(){
        // 상하 카메라 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX,0,0);
    }

    private void CharacterRotation(){
        // 좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    // 점프
    private void TryJump(){
        if(Input.GetKeyDown(KeyCode.Space) && isGround){
            Jump();
        }
    }

    private void Jump(){
        // 앉은 상태에서 점프 시 숙이기 해제
        // if(isCrouch)
        //     Crouch();

        myRigid.velocity = transform.up * jumpForce;
    }

    private void IsGround(){
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.3f);
    }

    // 숙이기
    // private void TryCrouch(){
    //     if(Input.GetKeyDown(KeyCode.LeftControl)){
    //         Crouch();
    //     }
    // }

    // private void Crouch(){
    //     isCrouch = !isCrouch;
    //     isWalk = false;

    //     if(isCrouch){
    //         applySpeed = crouchSpeed;
    //         applyCrouchPosY = crouchPosY;
    //     }
    //     else{
    //         applySpeed = walkSpeed;
    //         applyCrouchPosY = originPosY;
    //     }

    //     StartCoroutine(CrouchCoroutine());
    // }

    // IEnumerator CrouchCoroutine(){
    //     float _posY = theCamera.transform.localPosition.y;
    //     int count = 0;

    //     while(_posY != applyCrouchPosY){
    //         count++;
    //         _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
    //         theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x, _posY, theCamera.transform.localPosition.z);

    //         if(count > 15)
    //             break;

    //         yield return null; // 1프레임 대기
    //     }

    //     theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x, _posY, theCamera.transform.localPosition.z);
    // }

    // 달리기
    private void TryRun(){
        if (Input.GetKey(KeyCode.LeftShift) && _velocity.magnitude >= 0.01f)
        {
            Running();
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || _velocity.magnitude < 0.01f){
            RunningCancle();
        }
    }

    private void Running(){
        // if(isCrouch)
        //     Crouch();

        isRun = true;
        applySpeed = runSpeed;
        anim.SetBool("Run", isRun);
    }

    private void RunningCancle(){
        isRun = false;
        applySpeed = walkSpeed;
        anim.SetBool("Run", isRun);
    }
}
