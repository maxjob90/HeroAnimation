using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    private static readonly int Punches = Animator.StringToHash("punches");
    private static readonly int JumpAttack = Animator.StringToHash("jumpAttack");
    private static readonly int SpitAttack = Animator.StringToHash("spitAttack");

    private static readonly int Vertical = Animator.StringToHash("vertical");
    private static readonly int IsMoveBack = Animator.StringToHash("isMoveBack");
    private static readonly int IsMoveForward = Animator.StringToHash("isMoveForward");

    private int[] _attacks;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attacks = new[] { Punches, JumpAttack, SpitAttack };
    }

    private void Update()
    {
        var verticalMove = Input.GetAxis("Vertical");
        switch (verticalMove)
        {
            case > 0:
                _animator.SetBool(IsMoveBack, false);
                _animator.SetBool(IsMoveForward, true);
                MovePlayer(Vector3.forward / 2);
                break;
            case < 0:
                _animator.SetBool(IsMoveBack, true);
                _animator.SetBool(IsMoveForward, false);
                MovePlayer(Vector3.back);
                break;
            default:
                _animator.SetBool(IsMoveBack, false);
                _animator.SetBool(IsMoveForward, false);
                break;
        }

        _animator.SetFloat(Vertical, verticalMove);

        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger(RandomAttack());
        }
    }

    private void MovePlayer(Vector3 direction)
    {
        var currentPosition = transform.position;
        currentPosition.z += direction.z * Time.deltaTime;
        transform.position = new Vector3(0, 0, currentPosition.z);
    }

    private int RandomAttack()
    {
        var random = new Random();
        var randomIndex = random.Next(0, _attacks.Length);
        return _attacks[randomIndex];
    }
}