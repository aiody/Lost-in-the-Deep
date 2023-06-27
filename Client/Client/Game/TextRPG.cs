using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class TextRPG
    {
        Character character = null;
        string name;

        public void Start()
        {
            ShowGameStory();
            SelectCharacter();
            InputName();
            LoadEvents();
        }

        public void Update()
        {
            Console.Clear();
            Console.WriteLine("Gaming..");
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
            while (character == null)
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
                character = factory.MakeCharacter(selectedNumber);

                if (character == null)
                {
                    Console.WriteLine("다시 골라주세요.");
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine($"당신은 {character.name}를 고르셨습니다.");
            Thread.Sleep(1000);
        }

        void InputName()
        {
            Console.Clear();
            Console.WriteLine("당신의 이름을 알려주세요.");
            name = Console.ReadLine();
            Console.WriteLine($"안녕하세요 {name}님");
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

        }
    }
}
