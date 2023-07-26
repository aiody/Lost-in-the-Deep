using Google.Protobuf.Protocol;
using Action = Google.Protobuf.Protocol.Action;

namespace Client
{
    internal class TextRPG
    {
        List<Event> _events;
        Player _myPlayer = null;
        UIRenderer renderer = new UIRenderer();

        public void Start()
        {
            NetworkManager.Instance.Init();
            renderer.DrawMain();
            ShowGameStory();
            
            // 초기 설정
            while (_myPlayer == null)
            {
                _myPlayer = PlayerManager.Instance.MyPlayer;
                LoadEvents();
            }
            SelectCharacter();
            InputName();

            renderer.DrawUIFrame();
            renderer.DrawStatusBar();
        }

        public void Update()
        {
            //Console.Clear();
            //Console.WriteLine("Gaming..");
            OccurEvent();
            //checkGameOver();
        }

        void ShowGameStory()
        {
            Console.Clear();
            Console.WriteLine("당신은 심해에 있는 유적을 탐험하여 유물을 해수면으로 가져오는 것이 목표인 프로젝트 팀에 속해있다.");
            Console.WriteLine("임무 수행중에 본체 잠수함에 문제가 생겨 탈출해야 한다.");
            Console.WriteLine("다행히 개인용 구명선이 있지만 산소와 연료가 제한되어 있다.");
            Console.WriteLine("당신은 지상으로 올라갔을 때 유물을 가져간다면 얻게 될 부와 명예를 떠올리며 유물을 최대한 챙기기로 한다.");
            Console.WriteLine();
            Console.WriteLine("계속 하려면 아무 키나 누르고 Enter를 누르시오.");
            Console.ReadKey();
        }

        void SelectCharacter()
        {
            int selectedNumber = 0;
            while (_myPlayer.Character == null)
            {
                Console.Clear();
                Console.WriteLine("난이도가 어려운 프로젝트인 만큼 팀에는 여러 인재들이 존재했다.");
                Console.WriteLine("플레이 하고 싶은 캐릭터를 고르세요.");
                Console.WriteLine();
                Console.WriteLine("1. 다이버 : 물리적으로 튼튼, 기동력 좋음. 산소를 덜 소모함.");
                Console.WriteLine("2. 해양생물학자 : 지식 빵빵, 알고 있는 것이 많아서 상황 대처력이 좋음.");
                Console.WriteLine("3. 고고학자 : 유물에 대한 전문 지식으로 유물을 더 많이 챙길 수 있음.");
                
                int.TryParse(Console.ReadLine(), out selectedNumber);

                if (selectedNumber != 1 && selectedNumber != 2 && selectedNumber != 3)
                {
                    Console.WriteLine("다시 골라주세요.");
                    Thread.Sleep(1000);
                    continue;
                }

                CharacterType type = (CharacterType)(selectedNumber - 1);
                _myPlayer.Character = type;

                C_SelectCharacter selectPacket = new C_SelectCharacter { Character = type };
                NetworkManager.Instance.Send(selectPacket);

                Console.WriteLine($"당신은 {_myPlayer.CharacterName}를 고르셨습니다.");
                Thread.Sleep(1000);
            }
        }

        void InputName()
        {
            Console.Clear();
            Console.WriteLine("당신의 이름을 알려주세요.");
            _myPlayer.name = Console.ReadLine();
            Console.WriteLine($"안녕하세요 {_myPlayer.name}님");

            C_SetPlayerName namePacket = new C_SetPlayerName { Name = _myPlayer.name };
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
            int stage = ((_myPlayer.Depth - 1) / 1000) + 1;
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

            _myPlayer.Fuel += curAction.Fuel;
            _myPlayer.Food += curAction.Food;
            _myPlayer.Oxygen += curAction.Oxygen;
            _myPlayer.Relic += curAction.Relic;
            _myPlayer.Depth -= curAction.Surge;

            renderer.DrawActionResult(curAction, _myPlayer.Depth);
        }

        void checkGameOver()
        {
            Thread.Sleep(1000);
            Console.Clear();

            if (_myPlayer.Depth <= 0)
                Ending();

            if (_myPlayer.Fuel <= 0 || _myPlayer.Food <= 0 || _myPlayer.Oxygen <= 0)
                GameOver();
        }

        void Ending()
        {
            Console.WriteLine("당신은 탈출에 성공하였습니다~!");
        }

        void GameOver()
        {
            Console.WriteLine("당신은 탈출에 실패하였습니다...");
        }
    }
}
