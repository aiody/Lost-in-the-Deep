using Google.Protobuf.Protocol;

namespace Client
{
    internal class TextRPG
    {
        Character _character = null;
        string _name;
        List<Event> _events;
        int _depth = 5000;
        int _fuel = 100;
        int _food = 100;
        int _oxygen = 1000;
        int _relic = 50;

        public void Start()
        {
            NetworkManager.Instance.Init();

            C_GetEvent getEvent = new C_GetEvent() { Name = "Robinn" };
            NetworkManager.Instance.Send(getEvent);

            //ShowGameStory();
            //SelectCharacter();
            //InputName();
            LoadEvents();
        }

        public void Update()
        {
            //Console.Clear();
            //Console.WriteLine("Gaming..");
            OccurEvent();
            checkGameOver();
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
            Console.ReadLine();
        }

        void SelectCharacter()
        {
            while (_character == null)
            {
                Console.Clear();
                Console.WriteLine("난이도가 어려운 프로젝트인 만큼 팀에는 여러 인재들이 존재했다.");
                Console.WriteLine("플레이 하고 싶은 캐릭터를 고르세요.");
                Console.WriteLine();
                Console.WriteLine("1. 다이버 : 물리적으로 튼튼, 기동력 좋음. 산소를 덜 소모함.");
                Console.WriteLine("2. 해양생물학자 : 지식 빵빵, 알고 있는 것이 많아서 상황 대처력이 좋음.");
                Console.WriteLine("3. 고고학자 : 유물에 대한 전문 지식으로 유물을 더 많이 챙길 수 있음.");
                int selectedNumber = 0;
                int.TryParse(Console.ReadLine(), out selectedNumber);

                CharacterFactory factory = new CharacterFactory();
                _character = factory.MakeCharacter(selectedNumber);

                if (_character == null)
                {
                    Console.WriteLine("다시 골라주세요.");
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine($"당신은 {_character.name}를 고르셨습니다.");
            _fuel = 100;
            _food = 100;
            _oxygen = 1000;
            _relic = 50;
            Thread.Sleep(1000);
        }

        void InputName()
        {
            Console.Clear();
            Console.WriteLine("당신의 이름을 알려주세요.");
            _name = Console.ReadLine();
            Console.WriteLine($"안녕하세요 {_name}님");
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
            EventLoader loader = new EventLoader();
            _events = loader.Load();
        }

        void OccurEvent()
        {
            // 1 - 1000 : 1 stage
            // 1001 - 2000 : 2 stage
            // 2001 - 3000 : 3 stage
            // 3001 - 4000 : 4 stage
            // 4001 - 5000 : 5 stage
            int stage = ((_depth - 1) / 1000) + 1;
            List<Event> availableEvents = _events.FindAll(e => e.Stage == stage);
            Random rand = new Random();
            Event curEvent = availableEvents[rand.Next(0, availableEvents.Count)];
            Console.WriteLine(curEvent.Name);
            Console.WriteLine(curEvent.Description);
            for (int i = 0; i < curEvent.Actions.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {curEvent.Actions[i].name}");
            }

            // choose action
            int input = 0;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out input);

                if (input > 0 && input <= curEvent.Actions.Count)
                    break;

                Console.WriteLine("다시 입력해주세요.");
            }

            // ouput action result
            Action curAction = curEvent.Actions[input - 1];
            Console.WriteLine(curAction.name);
            Console.WriteLine(curAction.description);

            Thread.Sleep(500);
            if (curAction.surge > 0)
                Console.WriteLine($"{Math.Abs(curAction.surge)}만큼 상승하였습니다.");
            else
                Console.WriteLine($"{Math.Abs(curAction.surge)}만큼 하강하였습니다.");

            Thread.Sleep(500);
            if (curAction.fuel > 0)
                Console.WriteLine($"연료를 {Math.Abs(curAction.fuel)}만큼 획득했습니다.");
            else
                Console.WriteLine($"연료를 {Math.Abs(curAction.fuel)}만큼 소모했습니다.");

            Thread.Sleep(500);
            if (curAction.food > 0)
                Console.WriteLine($"식량을 {Math.Abs(curAction.food)}만큼 획득했습니다.");
            else
                Console.WriteLine($"식량을 {Math.Abs(curAction.food)}만큼 소모했습니다.");

            Thread.Sleep(500);
            if (curAction.oxygen > 0)
                Console.WriteLine($"산소를 {Math.Abs(curAction.oxygen)}만큼 획득했습니다.");
            else
                Console.WriteLine($"산소를 {Math.Abs(curAction.oxygen)}만큼 소모했습니다.");

            Thread.Sleep(500);
            if (curAction.relic > 0)
                Console.WriteLine($"유물을 {Math.Abs(curAction.relic)}만큼 얻었습니다.");
            else
                Console.WriteLine($"유물을 {Math.Abs(curAction.relic)}만큼 잃었습니다.");

            _fuel += curAction.fuel;
            _food += curAction.food;
            _oxygen += curAction.oxygen;
            _relic += curAction.relic;

            Thread.Sleep(500);
            _depth -= curAction.surge;
            Console.WriteLine($"현재 깊이 {_depth}");
        }

        void checkGameOver()
        {
            Thread.Sleep(1000);
            Console.Clear();

            if (_depth <= 0)
                Ending();

            if (_fuel <= 0 || _food <= 0 || _oxygen <= 0)
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
