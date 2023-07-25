using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class UIRenderer
    {
        readonly int width = Program.SCREEN_WIDTH;
        readonly int height = Program.SCREEN_HEIGHT;

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
            Console.WriteLine("                                            계속 하려면 아무 키나 누르세요.                                             ");
            Console.ReadKey();
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
                Console.SetCursorPosition(102, 17);
                Console.Write("2500m");
                Console.SetCursorPosition(102, 30);
                Console.Write("5000m");
            }

            SetCursorPositionInputArea();
        }

        public void DrawStatusBar()
        {
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

        public void DrawEventArea()
        {
            SetCursorPositionInputArea();
        }

        public void DrawActionArea()
        {
            SetCursorPositionInputArea();
        }

        public void DrawDepthDashboard()
        {
            SetCursorPositionInputArea();
        }

        void SetCursorPositionInputArea()
        {
            Console.SetCursorPosition(2, height - 2);
        }
    }
}
