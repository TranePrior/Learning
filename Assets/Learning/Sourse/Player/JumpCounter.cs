using UnityEngine;

public class JumpCounter : MonoBehaviour
{
   [SerializeField] private PlayerMove _playerMove;
   
   public int Count {get; private set;} 
   
   public event System.Action<int> OnCountChange;

   void OnEnable()
   {
      if (_playerMove == null)
         _playerMove = GetComponentInParent<PlayerMove>();

      if (_playerMove != null)
         _playerMove.Jumped += OnPlayerJumped;
   }

   void OnDisable()
   {
      if (_playerMove != null)
         _playerMove.Jumped -= OnPlayerJumped;
   }

   void OnPlayerJumped()
   {
      Count++;
      OnCountChange?.Invoke(Count);

   }
}
