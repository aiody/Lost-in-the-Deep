using Google.Protobuf.Protocol;
using Action = Google.Protobuf.Protocol.Action;

namespace Client
{
    internal class TextRPG
    {
        List<Event> _events;
        Player _myPlayer = null;
        UIRenderer renderer = new UIRenderer();
        bool _isEndGame = false;

        public void Start()
        {
            NetworkManager.Instance.Init();
            InitGame();
        }

        void InitGame()
        {
            renderer.DrawMain();
            renderer.DrawGameStory();

            _myPlayer = PlayerManager.Instance.MyPlayer;
            LoadEvents();

            SelectCharacter();
            InputName();

            renderer.DrawUIFrame();
            renderer.DrawName(_myPlayer.Info.Name, _myPlayer.CharacterName);
            renderer.DrawStatusBar(_myPlayer.Info.Fuel, _myPlayer.Info.Oxygen, _myPlayer.Info.Food, _myPlayer.Info.Relic);
            int[] depthsOfOthers = PlayerManager.Instance.Others.Select(x => x.Value.Info.Depth).ToArray();
            renderer.DrawDepthDashboard(_myPlayer.Info.Depth, depthsOfOthers);

            _isEndGame = false;
        }

        public void Update()
        {
            if (_isEndGame == false)
            {
                OccurEvent();
                CheckGameOver();
            }
        }

        void SelectCharacter()
        {
            int selectedNumber = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("난이도가 어려운 프로젝트인 만큼 팀에는 여러 인재들이 존재했다.");
                Console.WriteLine("플레이 하고 싶은 캐릭터를 고르세요.");
                Console.WriteLine();
                Console.WriteLine("1. 다이버 : 물리적으로 튼튼, 기동력 좋음. 산소를 덜 소모함.");
                Console.WriteLine("2. 해양생물학자 : 지식 빵빵, 알고 있는 것이 많아서 상황 대처력이 좋음.");
                Console.WriteLine("3. 고고학자 : 유물에 대한 전문 지식으로 유물을 더 많이 챙길 수 있음.");
                
                int.TryParse(Console.ReadLine(), out selectedNumber);

                if (selectedNumber == 1 || selectedNumber == 2 || selectedNumber == 3)
                    break;

                Console.WriteLine("다시 골라주세요.");
                Thread.Sleep(1000);
            }

            CharacterType type = (CharacterType)(selectedNumber - 1);
            _myPlayer.Info.Character = type;

            C_SelectCharacter selectPacket = new C_SelectCharacter { Character = type };
            NetworkManager.Instance.Send(selectPacket);

            Console.WriteLine($"당신은 {_myPlayer.CharacterName}를 고르셨습니다.");
            Thread.Sleep(1000);
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
        }

        void LoadEvents()
        {
            _events = DataManager.Instance.Events;
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

            renderer.DrawEventArea(curEvent.Name, curEvent.Description);
            renderer.DrawActionArea(curEvent.Actions.ToList());

            // choose action
            int input = 0;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out input);

                if (input > 0 && input <= curEvent.Actions.Count)
                    break;

                renderer.PleaseTypeAgain();
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

            _myPlayer.Info.Fuel += curAction.Fuel;
            _myPlayer.Info.Food += curAction.Food;
            _myPlayer.Info.Oxygen += curAction.Oxygen;
            _myPlayer.Info.Relic += curAction.Relic;
            _myPlayer.Info.Depth -= curAction.Surge;

            renderer.DrawActionResult(curAction, _myPlayer.Info.Depth);
            renderer.DrawStatusBar(_myPlayer.Info.Fuel, _myPlayer.Info.Oxygen, _myPlayer.Info.Food, _myPlayer.Info.Relic);
            int[] depthsOfOthers = PlayerManager.Instance.Others.Select(x => x.Value.Info.Depth).ToArray();
            renderer.DrawDepthDashboard(_myPlayer.Info.Depth, depthsOfOthers);
            renderer.ContinueWithEnter();
        }

        void CheckGameOver()
        {
            if (_myPlayer.Info.Depth <= 0)
            {
                _isEndGame = true;
                Ending();
            }

            if (_myPlayer.Info.Fuel <= 0 || _myPlayer.Info.Food <= 0 || _myPlayer.Info.Oxygen <= 0)
            {
                _isEndGame = true;
                GameOver();
            }
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

            int input = 0;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out input);

                if (input == 1 || input == 2)
                    break;

                Console.WriteLine("다시 골라주세요.");
                Thread.Sleep(1000);
            }

            if (input == 1)
            {
                C_Retry retryPacket = new C_Retry();
                NetworkManager.Instance.Send(retryPacket);

                InitGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
