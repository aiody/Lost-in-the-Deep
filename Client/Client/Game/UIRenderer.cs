using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Google.Protobuf.Protocol.Action;

namespace Client
{
    internal class UIRenderer
    {
        readonly static int width = Program.SCREEN_WIDTH;
        readonly static int height = Program.SCREEN_HEIGHT;

        public void DrawMain()
        {
            Console.Clear();
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                      ,,ir                                                              ");
            Console.WriteLine("                                                     Bs:hr                                                              ");
            Console.WriteLine("                                                     M,::                                                               ");
            Console.WriteLine("                                                     hri                                                                ");
            Console.WriteLine("                                                     Xr:                                                                ");
            Console.WriteLine("                                                     9:                                                                 ");
            Console.WriteLine("                                                   ,,2issh2ss                                                           ");
            Console.WriteLine("                                                  G:, ,:,, ,r:                                                          ");
            Console.WriteLine("                                                 irssi::s,r2sri:,:i:ii                                                  ");
            Console.WriteLine("                                                 s GGG99G9sG:rs::r:,iMiri:sr:r:,                                        ");
            Console.WriteLine("                                                ri:ssssss2s i9i:,ir::r:ii :i:hs:s,:,,                                   ");
            Console.WriteLine("                                    :  :   ,:rrrr2M9h9GGhsi,,       ,r,,  :,,ir,ii,:Ms::                                ");
            Console.WriteLine("                                 GM,M  Bi:rr:,  s,i::rsr: Mrr:r25ssrsi  ,,, , s  , ,2s,,r:                              ");
            Console.WriteLine("                                 s9:2,,9s,  ,r2Gi       ,:9:   :,,  sBr, ,,:, s,rr  ir,: r5                             ");
            Console.WriteLine("                                  BX9s2X: ,rBM9GBs    ,MBhSGs     ,MB2MBs  , ,MGXGB2si,i, s,                            ");
            Console.WriteLine("                                 :srhirGi, GBi  MB5ssrBB:  GBr ,, BB,  BB, , 9Bi  MB5i  ,,:5                            ");
            Console.WriteLine("                                  BsG2S9s  iBBXGBs ,:,rBB9MBX ,,, sBB2GBs ,, 9BBGMBrss  ,:Xi                            ");
            Console.WriteLine("                                 ,BsM::B9:, ,rB5,  ,,, ,sB9, ,,,,, ,BBMsrsssrM,s2s, ss,shhi                             ");
            Console.WriteLine("                                 BB,B  B99Gsi,5 ,,:,,    s: ,,,,,,  h  ,     h,   ,iBh9Gs                               ");
            Console.WriteLine("                                 rr B  B  iXhB9srrsr:ri::G  ,,,::, r5  ,::::rMrs2hGMBs,                                 ");
            Console.WriteLine("                                             rB2BB9MBMG2BGsssrsssrrM2s225299BM99sr:                                     ");
            Console.WriteLine("                                              ,ir      ,r::rrsrsrrssii:::                                               ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("               M                   M       :B                 ,B                ,rssr                                   ");
            Console.WriteLine("              ,Br     s5S:  ,s52 :BBBi      ,    ,i       BB, :B,,,    ,:,      GBrrGBs   ::    ,:,     i,              ");
            Console.WriteLine("               B:    BB,:BS BB,   BB,      rB  BMshB:    :BBs ,BssBB  B9rMB     9B   2B rBrsB: BGrGB sBssBB             ");
            Console.WriteLine("              ,B,   rB   GB :h99  BM       sB  Br  BG     B5  :B   B iBi:sB,    9B   iB B9::B9,Br:sB, B   B:            ");
            Console.WriteLine("              ,Bhrs: BM,iB2  ,:B, BB,      2B  Bs  B9     BB  :B  :B :Bi ,      MB  iB9 BM    ,Br    :B  hB             ");
            Console.WriteLine("               ss29i  sXS,  2X2i   29:     ,B  Bi  Bs     rBM,,B  ,B  sMGGs     sBMMMi  ,GGGG  rM9Gs iBhMM,             ");
            Console.WriteLine("                                                                                                     sB                 ");
            Console.WriteLine("                                                                                                      :                 ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                    1. 게임 시작                                                        ");
            Console.WriteLine("                                                    2. 랭킹 보기                                                        ");
            SetCursorPositionInputArea();
        }

        public void DrawRanking()
        {
            Console.Clear();
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                iss22r                     rB      ,BX                                                  ");
            Console.WriteLine("                                BB,,,BB:                   rB                                                           ");
            Console.WriteLine("                                9B    Bs ,BG9BB  :BssXBBi  iB  ,Bs  B: ,B9sSGBs   2G22BBh                               ");
            Console.WriteLine("                                2BrriBh       Bs  BB   GB  iB :B:   Br  MB   rB, sB    B,                               ");
            Console.WriteLine("                                SB,,BB   ,2ssrBr  BM   rB  :BsB     Bi  GB   ,B,  Bi ,XB                                ");
            Console.WriteLine("                                GB   Bs  BB   Bs  BB   sB  rB :B:   Bi  MB   :B,  Bs:i                                  ");
            Console.WriteLine("                                9B   ,Bs rBG2sBB  BB   sB  rB  :Bs  BM  MB   iB,  B9,sssr                               ");
            Console.WriteLine("                                                                                 rBr  rB9                               ");
            Console.WriteLine("                                                                                   issr                                 ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                    ----------------------------------------------------------------------------------                  ");
            Console.WriteLine("                    |      순위      |          이름          |              유물 개수               |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       1        |                        |                                      |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       2        |                        |                                      |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       3        |                        |                                      |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       4        |                        |                                      |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       5        |                        |                                      |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       6        |                        |                                      |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       7        |                        |                                      |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       8        |                        |                                      |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       9        |                        |                                      |                  ");
            Console.WriteLine("                    |--------------------------------------------------------------------------------|                  ");
            Console.WriteLine("                    |       10       |                        |                                      |                  ");
            Console.WriteLine("                    ----------------------------------------------------------------------------------                  ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                    1. 뒤로 가기                                                        ");
            Console.WriteLine("                                                                                                                        ");
            SetCursorPositionInputArea();
        }
        
        public void DrawGameStory()
        {
            Console.Clear();
            Console.WriteLine("당신은 심해에 있는 유적을 탐험하여 유물을 해수면으로 가져오는 것이 목표인 프로젝트 팀에 속해있다.");
            Console.WriteLine("임무 수행중에 본체 잠수함에 문제가 생겨 탈출해야 한다.");
            Console.WriteLine("다행히 개인용 구명선이 있지만 산소와 연료가 제한되어 있다.");
            Console.WriteLine("당신은 지상으로 올라갔을 때 유물을 가져간다면 얻게 될 부와 명예를 떠올리며 유물을 최대한 챙기기로 한다.");
            Console.WriteLine();
            Console.WriteLine("계속 하려면 아무 키나 누르고 Enter를 누르시오.");
        }

        public void DrawSelectCharacter()
        {
            Console.Clear();
            Console.WriteLine("난이도가 어려운 프로젝트인 만큼 팀에는 여러 인재들이 존재했다.");
            Console.WriteLine("플레이 하고 싶은 캐릭터를 고르세요.");
            Console.WriteLine();
            Console.WriteLine("1. 다이버 : 물리적으로 튼튼, 기동력 좋음. 산소를 덜 소모함.");
            Console.WriteLine("2. 해양생물학자 : 지식 빵빵, 알고 있는 것이 많아서 상황 대처력이 좋음.");
            Console.WriteLine("3. 고고학자 : 유물에 대한 전문 지식으로 유물을 더 많이 챙길 수 있음.");
            SetCursorPositionInputArea();
        }

        public void DrawSelectedCharacter(string name)
        {
            SetCursorPositionInputArea();
            Console.WriteLine($"당신은 {name}를 고르셨습니다.");
        }

        public void DrawUIFrame()
        {
            Console.Clear();
            // 가로줄
            {
                for (int i = 0; i < width; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("-");
                    Console.SetCursorPosition(i, 4);
                    Console.Write("-");
                    Console.SetCursorPosition(i, height - 9);
                    Console.Write("-");
                    Console.SetCursorPosition(i, height - 3);
                    Console.Write("-");
                    Console.SetCursorPosition(i, height - 1);
                    Console.Write("-");
                }
            }

            // 세로줄
            {
                for (int i = 1; i < 4; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("|");
                    Console.SetCursorPosition(31, i);
                    Console.Write("|");
                    Console.SetCursorPosition(31 + 22, i);
                    Console.Write("|");
                    Console.SetCursorPosition(31 + 22 + 22, i);
                    Console.Write("|");
                    Console.SetCursorPosition(31 + 22 + 22 + 22, i);
                    Console.Write("|");
                    Console.SetCursorPosition(width - 1, i);
                    Console.Write("|");
                }

                for (int i = 5; i < height - 9; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("|");
                    Console.SetCursorPosition(width - 12, i);
                    Console.Write("|");
                    Console.SetCursorPosition(width - 1, i);
                    Console.Write("|");
                }

                for (int i = height - 8; i < height - 3; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("|");
                    Console.SetCursorPosition(width - 1, i);
                    Console.Write("|");
                }

                Console.SetCursorPosition(0, height - 2);
                Console.Write("|");
                Console.SetCursorPosition(width - 1, height - 2);
                Console.Write("|");
            }

            // 미터표시
            {
                Console.SetCursorPosition(105, 5);
                Console.Write("0m");
                Console.SetCursorPosition(102, 18);
                Console.Write("2500m");
                Console.SetCursorPosition(102, 30);
                Console.Write("5000m");
            }

            // 이름
            {
                Console.SetCursorPosition(4, 2);
                Console.Write("이름 : ");
            }

            // 연료
            {
                Console.SetCursorPosition(35, 2);
                Console.Write("연료 : ");
            }

            // 산소
            {
                Console.SetCursorPosition(57, 2);
                Console.Write("산소 : ");
            }

            // 음식
            {
                Console.SetCursorPosition(79, 2);
                Console.Write("음식 : ");
            }

            // 유물
            {
                Console.SetCursorPosition(101, 2);
                Console.Write("유물 : ");
            }

            SetCursorPositionInputArea();
        }

        public void DrawName(string name, string job)
        {
            EraseArea(11, 2, 30, 2);

            Console.SetCursorPosition(11, 2);
            Console.Write($"{name}({job})");

            SetCursorPositionInputArea();
        }

        public void DrawStatusBar(int fuel, int oxygen, int food, int relic)
        {
            EraseArea(42, 2, 52, 2);
            Console.SetCursorPosition(42, 2);
            Console.Write(fuel);

            EraseArea(64, 2, 74, 2);
            Console.SetCursorPosition(64, 2);
            Console.Write(oxygen);

            EraseArea(86, 2, 96, 2);
            Console.SetCursorPosition(86, 2);
            Console.Write(food);

            EraseArea(108, 2, 118, 2);
            Console.SetCursorPosition(108, 2);
            Console.Write(relic);

            SetCursorPositionInputArea();
        }

        static int START_EVENT_AREA_X = 1;
        static int START_EVENT_AREA_Y = 5;
        static int END_EVENT_AREA_X = 100;
        static int END_EVENT_AREA_Y = 30;
        public void DrawEventArea(string name, string description)
        {
            // 화면 지우기
            EraseArea(START_EVENT_AREA_X, START_EVENT_AREA_Y, END_EVENT_AREA_X, END_EVENT_AREA_Y);

            // 이벤트 이름 출력
            {
                Console.SetCursorPosition(START_EVENT_AREA_X, START_EVENT_AREA_Y);
                Console.WriteLine(name);
            }
            
            // 이벤트 설명 출력
            {
                List<string> results = ChunkString(description);

                for (int i = 0; i < results.Count; i++)
                {
                    Console.SetCursorPosition(START_EVENT_AREA_X, i + START_EVENT_AREA_Y + 2);
                    Console.WriteLine(results[i]);
                }
            }

            SetCursorPositionInputArea();
        }

        public void DrawActionResult(Action action, int curDepth)
        {
            // 화면 지우기
            EraseArea(START_EVENT_AREA_X, START_EVENT_AREA_Y, END_EVENT_AREA_X, END_EVENT_AREA_Y);

            // 결과 이름 출력
            {
                Console.SetCursorPosition(START_EVENT_AREA_X, START_EVENT_AREA_Y);
                Console.WriteLine(action.Name);
            }

            // 결과 설명 출력
            {
                List<string> results = ChunkString(action.Description);

                for (int i = 0; i < results.Count; i++)
                {
                    Console.SetCursorPosition(START_EVENT_AREA_X, i + START_EVENT_AREA_Y + 2);
                    Console.WriteLine(results[i]);
                }

                Thread.Sleep(500);
                Console.SetCursorPosition(START_EVENT_AREA_X, START_EVENT_AREA_Y + results.Count + 3);
                if (action.Surge > 0)
                    Console.WriteLine($"{Math.Abs(action.Surge)}만큼 상승하였습니다.");
                else
                    Console.WriteLine($"{Math.Abs(action.Surge)}만큼 하강하였습니다.");

                Thread.Sleep(500);
                Console.SetCursorPosition(START_EVENT_AREA_X, START_EVENT_AREA_Y + results.Count + 4);
                if (action.Fuel > 0)
                    Console.WriteLine($"연료를 {Math.Abs(action.Fuel)}만큼 획득했습니다.");
                else
                    Console.WriteLine($"연료를 {Math.Abs(action.Fuel)}만큼 소모했습니다.");

                Thread.Sleep(500);
                Console.SetCursorPosition(START_EVENT_AREA_X, START_EVENT_AREA_Y + results.Count + 5);
                if (action.Food > 0)
                    Console.WriteLine($"식량을 {Math.Abs(action.Food)}만큼 획득했습니다.");
                else
                    Console.WriteLine($"식량을 {Math.Abs(action.Food)}만큼 소모했습니다.");

                Thread.Sleep(500);
                Console.SetCursorPosition(START_EVENT_AREA_X, START_EVENT_AREA_Y + results.Count + 6);
                if (action.Oxygen > 0)
                    Console.WriteLine($"산소를 {Math.Abs(action.Oxygen)}만큼 획득했습니다.");
                else
                    Console.WriteLine($"산소를 {Math.Abs(action.Oxygen)}만큼 소모했습니다.");

                Thread.Sleep(500);
                Console.SetCursorPosition(START_EVENT_AREA_X, START_EVENT_AREA_Y + results.Count + 7);
                if (action.Relic > 0)
                    Console.WriteLine($"유물을 {Math.Abs(action.Relic)}만큼 얻었습니다.");
                else
                    Console.WriteLine($"유물을 {Math.Abs(action.Relic)}만큼 잃었습니다.");

                Thread.Sleep(500);
                Console.SetCursorPosition(START_EVENT_AREA_X, START_EVENT_AREA_Y + results.Count + 8);
                Console.WriteLine($"현재 깊이 {curDepth}");
            }
        }

        static int START_ACTION_AREA_X = 1;
        static int START_ACTION_AREA_Y = 32;
        static int END_ACTION_AREA_X = width - 2;
        static int END_ACTION_AREA_Y = 36;
        public void DrawActionArea(List<Action> actions)
        {
            // 화면 지우기
            EraseArea(START_ACTION_AREA_X, START_ACTION_AREA_Y, END_ACTION_AREA_X, END_ACTION_AREA_Y);

            // 액션 출력
            for (int i = 0; i < actions.Count; i++)
            {
                Console.SetCursorPosition(START_ACTION_AREA_X, i + START_ACTION_AREA_Y);
                Console.WriteLine($"{i + 1}: {actions[i].Name}");
            }

            SetCursorPositionInputArea();
        }

        static int START_DEPTH_DASHBOARD_X = 109;
        static int START_DEPTH_DASHBOARD_Y = 5;
        static int END_DEPTH_DASHBOARD_X = width - 2;
        static int END_DEPTH_DASHBOARD_Y = 30;
        public void DrawDepthDashboard(int myDepth, int[] otherDepths)
        {
            // 화면 지우기
            EraseArea(START_DEPTH_DASHBOARD_X, START_DEPTH_DASHBOARD_Y, END_DEPTH_DASHBOARD_X, END_DEPTH_DASHBOARD_Y);

            int iconPosY = (myDepth - 1) / 200 + 6;
            Console.SetCursorPosition(START_DEPTH_DASHBOARD_X, iconPosY);
            Console.Write("*");

            DrawDepthOfOtherPlayers(otherDepths);

            SetCursorPositionInputArea();
        }

        public void PleaseTypeAgain()
        {
            SetCursorPositionInputArea();
            Console.Write("다시 입력해주세요.");
            Thread.Sleep(1000);
            SetCursorPositionInputArea();
        }

        public void ContinueWithEnter()
        {
            SetCursorPositionInputArea();
            Console.Write("Enter를 누르고 계속 진행하기.");
            Console.ReadLine();
        }

        void DrawDepthOfOtherPlayers(int[] depths)
        {
            for (int i = 0; i < depths.Length; i++)
            {
                int iconPosY = (depths[i] - 1) / 200 + 6;
                Console.SetCursorPosition(START_DEPTH_DASHBOARD_X + 1 + i, iconPosY);
                Console.Write("&");
            }
        }

        static int START_INPUT_AREA_X = 2;
        static int START_INPUT_AREA_Y = height - 2;
        static int END_INPUT_AREA_X = width - 2;
        static int END_INPUT_AREA_Y = height - 2;
        void SetCursorPositionInputArea()
        {
            // 화면 지우기
            EraseArea(START_INPUT_AREA_X, START_INPUT_AREA_Y, END_INPUT_AREA_X, END_INPUT_AREA_Y);

            // 커서 옮기기
            Console.SetCursorPosition(START_INPUT_AREA_X, START_INPUT_AREA_Y);
        }

        void EraseArea(int startX, int startY, int endX, int endY)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

        List<string> ChunkString(string str)
        {
            // 탭 제거
            string s = str.Replace("\t", string.Empty);
            // 줄바꿈이 있으면 문자열 나눔
            string[] lines = s.Split("\n");

            List<string> results = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    results.Add("");
                else
                {
                    var chunkedStr = lines[i].Chunk(56).Select(x => new string(x));
                    results.AddRange(chunkedStr);
                }
            }

            return results;
        }
    }
}
