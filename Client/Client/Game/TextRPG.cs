using Google.Protobuf.Protocol;
using Action = Google.Protobuf.Protocol.Action;

namespace Client
{
    internal class TextRPG
    {
        List<Event> _events;
        Player _myPlayer = null;
        UIRenderer _renderer = new UIRenderer();
        Scene _curScene = Scene.Main;

        enum Scene
        {
            Main,
            Ranking,
            Story,
            SelectCharacter,
            ChooseName,
            Game,
            Ending,
            GameOver
        }

        public void Start()
        {
            NetworkManager.Instance.Init();
            NetworkManager.Instance.Loading();
            InitGame();
        }

        void InitGame()
        {
            _myPlayer = PlayerManager.Instance.MyPlayer;
            LoadEvents();
        }

        void LoadEvents()
        {
            _events = DataManager.Instance.Events;
        }

        public void Update()
        {
            switch (_curScene)
            {
                case Scene.Main:
                    _renderer.DrawMain();
                    InputMain();
                    break;
                case Scene.Ranking:
                    C_ReqRankingList reqRank = new C_ReqRankingList();
                    NetworkManager.Instance.Send(reqRank);
                    NetworkManager.Instance.Loading();

                    _renderer.DrawRanking(DataManager.Instance.Ranking);
                    InputRanking();
                    break;
                case Scene.Story:
                    _renderer.DrawGameStory();
                    InputStory();
                    break;
                case Scene.SelectCharacter:
                    _renderer.DrawSelectCharacter();
                    InputSelectCharacter();
                    break;
                case Scene.ChooseName:
                    InputName();
                    break;
                case Scene.Game:
                    _renderer.DrawUIFrame();
                    _renderer.DrawName(_myPlayer.Info.Name, _myPlayer.CharacterName);
                    _renderer.DrawStatusBar(_myPlayer.Stat.Fuel, _myPlayer.Stat.Oxygen, _myPlayer.Stat.Food, _myPlayer.Stat.Relic);
                    int[] depthsOfOthers = PlayerManager.Instance.Others.Select(x => x.Value.Info.Depth).ToArray();
                    _renderer.DrawDepthDashboard(_myPlayer.Info.Depth, depthsOfOthers);

                    OccurEvent();
                    CheckGameOver();

                    break;
                case Scene.Ending:
                    Ending();
                    break;
                case Scene.GameOver:
                    GameOver();
                    break;
            }
        }

        void InputMain()
        {
            int selectedNumber = 0;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out selectedNumber);

                if (selectedNumber == 1 || selectedNumber == 2)
                    break;

                _renderer.PleaseTypeAgain();
            }

            switch (selectedNumber)
            {
                case 1:
                    _curScene = Scene.Story;
                    break;
                case 2:
                    _curScene = Scene.Ranking;
                    break;
            }
        }

        void InputRanking()
        {
            int selectedNumber = 0;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out selectedNumber);

                if (selectedNumber == 1)
                    break;

                _renderer.PleaseTypeAgain();
            }

            switch (selectedNumber)
            {
                case 1:
                    _curScene = Scene.Main;
                    break;
            }
        }

        void InputStory()
        {
            Console.ReadKey();
            _curScene = Scene.SelectCharacter;
        }

        void InputSelectCharacter()
        {
            int selectedNumber = 0;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out selectedNumber);

                if (selectedNumber == 1 || selectedNumber == 2 || selectedNumber == 3)
                    break;

                _renderer.PleaseTypeAgain();
            }

            CharacterType type = (CharacterType)(selectedNumber - 1);
            _myPlayer.Info.Character = type;

            C_SelectCharacter selectPacket = new C_SelectCharacter { Character = type };
            NetworkManager.Instance.Send(selectPacket);

            _renderer.DrawSelectedCharacter(_myPlayer.CharacterName);
            Thread.Sleep(1000);

            _curScene = Scene.ChooseName;
        }

        void InputName()
        {
            Console.Clear();
            Console.WriteLine("당신의 이름을 알려주세요.");
            _myPlayer.Info.Name = Console.ReadLine();
            Console.WriteLine($"안녕하세요 {_myPlayer.Info.Name}님");

            C_SetPlayerName namePacket = new C_SetPlayerName { Name = _myPlayer.Info.Name };
            NetworkManager.Instance.Send(namePacket);

            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("게임을 시작하겠습니다.");
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("게임을 시작하겠습니다..");
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("게임을 시작하겠습니다...");
            Thread.Sleep(1000);

            _curScene = Scene.Game;
        }

        void OccurEvent()
        {
            // 1 - 1000 : 1 stage
            // 1001 - 2000 : 2 stage
            // 2001 - 3000 : 3 stage
            // 3001 - 4000 : 4 stage
            // 4001 - 5000 : 5 stage
            int stage = ((_myPlayer.Info.Depth - 1) / 1000) + 1;
            List<Event> availableEvents = _events.FindAll(e => e.Stage == stage);
            Random rand = new Random();
            Event curEvent = availableEvents[rand.Next(0, availableEvents.Count)];

            _renderer.DrawEventArea(curEvent.Name, curEvent.Description);
            _renderer.DrawActionArea(curEvent.Actions.ToList());

            // choose action
            int input = 0;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out input);

                if (input > 0 && input <= curEvent.Actions.Count)
                    break;

                _renderer.PleaseTypeAgain();
            }

            // ouput action result
            Action curAction = curEvent.Actions[input - 1];

            // 패킷 보내기
            C_ChooseAction chooseActionPacket = new C_ChooseAction
            {
                ActionId = curAction.Id,
                EventId = curEvent.Id
            };
            NetworkManager.Instance.Send(chooseActionPacket);

            _myPlayer.Stat.Fuel += curAction.Fuel;
            _myPlayer.Stat.Food += curAction.Food;
            _myPlayer.Stat.Oxygen += curAction.Oxygen;
            _myPlayer.Stat.Relic += curAction.Relic;
            _myPlayer.Info.Depth -= curAction.Surge;

            _renderer.DrawActionResult(curAction, _myPlayer.Info.Depth);
            _renderer.DrawStatusBar(_myPlayer.Stat.Fuel, _myPlayer.Stat.Oxygen, _myPlayer.Stat.Food, _myPlayer.Stat.Relic);
            int[] depthsOfOthers = PlayerManager.Instance.Others.Select(x => x.Value.Info.Depth).ToArray();
            _renderer.DrawDepthDashboard(_myPlayer.Info.Depth, depthsOfOthers);
            _renderer.ContinueWithEnter();
        }

        void CheckGameOver()
        {
            if (_myPlayer.Info.Depth <= 0)
                _curScene = Scene.Ending;

            if (_myPlayer.Stat.Fuel <= 0 || _myPlayer.Stat.Food <= 0 || _myPlayer.Stat.Oxygen <= 0)
                _curScene = Scene.GameOver;
        }

        void Ending()
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("당신은 탈출에 성공하였습니다~!");
            Retry();
        }

        void GameOver()
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("당신은 탈출에 실패하였습니다...");
            Retry();
        }

        void Retry()
        {
            Console.WriteLine("다시 시도하겠습니까?");
            Console.WriteLine("1. 다시 시작");
            Console.WriteLine("2. 게임 종료");

            int selectedNumber = 0;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out selectedNumber);

                if (selectedNumber == 1 || selectedNumber == 2)
                    break;

                _renderer.PleaseTypeAgain();
            }

            if (selectedNumber == 1)
            {
                PlayerManager.Instance.Clear();

                C_Retry retryPacket = new C_Retry();
                NetworkManager.Instance.Send(retryPacket);
                NetworkManager.Instance.Loading();

                InitGame();
                _curScene = Scene.Main;
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
