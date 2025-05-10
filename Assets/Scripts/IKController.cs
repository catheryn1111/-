using UnityEngine;
public class IKControl : MonoBehaviour
{
    [SerializeField] Transform lookObj; //������ �� ������� ������
    [SerializeField] GameObject enemy;//����
    Animator animator;
    PlayerLook playerlook;
    private bool ikActive = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerlook = GetComponent<PlayerLook>();
    }

    void OnAnimatorIK()
    {
        if (ikActive)
        {
            //���������� �������
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(lookObj.position);
            //���������� ����� �����   
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, lookObj.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, lookObj.rotation);
            //�������� ����� � PlayerLook    
            playerlook.FindEnemy(enemy.gameObject);
            //��������� ���������� � is kinematic �����
            enemy.gameObject.GetComponent<Enemy>().Death(false);
        }
        else
        {
            //�������� ���������� ����� ���������
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            //�������� ���������� ������� ���������
            animator.SetLookAtWeight(0);
            //��������� ���������� ������ � PlayerLook
            playerlook.FindEnemy(null);
            //�������� ����������
            enemy.gameObject.GetComponent<Enemy>().OffTelekinesis();
        }
    }

    public void Watch()
    {
        ikActive = !ikActive;
    }
}