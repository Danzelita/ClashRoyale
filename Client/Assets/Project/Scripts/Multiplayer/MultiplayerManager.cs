using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Colyseus;
namespace Project.Scripts.Multiplayer
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {

        public string ClientId => _room != null ? _room.SessionId : string.Empty;
        
        private ColyseusRoom<State> _room;
        
        private const string StateHandler = "state_handler";
        private const string GetReadyKey = "GetReady";
        private const string StartGameKey = "Start";
        private const string CancelGameKey = "CancelStart";
        
        public event Action GetReady;
        public event Action<string> StartGame;
        public event Action CancelGame;
        protected override void Awake()
        {
            base.Awake();
            
            Instance.InitializeClient();
            DontDestroyOnLoad(gameObject);
        }

        public async Task Connect()
        {
            Dictionary<string, object> data = new()
            {
                ["id"] = UserInfo.Instance.ID,
            };
            _room = await Instance.client.JoinOrCreate<State>(roomName: StateHandler, data);
            
            _room.OnMessage<object>(GetReadyKey, _ => GetReady?.Invoke());
            _room.OnMessage<string>(StartGameKey, jsonDecks => StartGame?.Invoke(jsonDecks));
            _room.OnMessage<object>(CancelGameKey, _ => CancelGame?.Invoke());
        }

        public void Leave()
        {
            _room?.Leave();
            _room = null;
        }
    }
}