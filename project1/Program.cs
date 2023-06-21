using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1
{
	class Program
	{
		static void Main(string[] args)
		{
            //// 시작
            double holdings = 1000000; // 보유자산 100만원

            Console.WriteLine("블랙잭 게임");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("당신은 100만원을 가지고 딜러와 1:1 블랙잭 대결을 하게 됩니다");   // 게임 규칙 설명
            Console.WriteLine("당신의 보유자산이 200만원이 되면 게임은 자동으로 종료됩니다");
            Console.WriteLine("만약 당신의 보유자산이 0이 되면 게임은 자동으로 종료됩니다");
            Console.WriteLine("------------------------------------------------------------------");

            while (true)
			{
                Console.WriteLine("게임을 계속 진행하겠습니까? 게임을 포기를 원하면 quit, 계속 진행을 원한다면 quit을 제외한 아무 글자를 입력하세요");
                string answer1 = Console.ReadLine();

                if (answer1 == "quit")
				{
                    break;
				}

                First:
                Console.WriteLine("배팅 금액을 입력해주십시오");
                string strbetamount = Console.ReadLine();

                if(strbetamount.All(char.IsDigit) == false)
				{
                    Console.WriteLine("잘못된 답을 입력하였습니다. 숫자로만 이루어진 답변으로 다시 입력해주세요");
                    goto First;
				}
 
                double betamount = double.Parse(strbetamount);
                double balance = holdings - betamount; // 남은 돈
                if (balance < 0)
				{
                    Console.WriteLine("보유 자산보다 많은 금액을 베팅하였습니다. 다시 배팅해주세요");
                    goto First;
				}
                Console.WriteLine($"당신의 배팅금액은 {betamount}입니다. 당신의 남은 보유자산은 {balance}입니다");

                var suffledcards = new DeckOfCards();
                suffledcards.Shuffle(); // 카드를 셔플해준다


                List<string> dealerCard = new List<string>(); // 딜러의 카드 목록
                List<string> playerCard = new List<string>(); // 플레이어의 카드 목록
                dealerCard.Add(suffledcards.Numbercard(0).ToString()); // 딜러의 카드 목록에 셔플된 카드 중 첫번째 카드 추가
                playerCard.Add(suffledcards.Numbercard(1).ToString()); // 플레이어의 카드 목록에 셔플된 카드 중 두번째 카드 추가
                dealerCard.Add(suffledcards.Numbercard(2).ToString()); // 딜러의 카드 목록에 셔플된 카드 중 세번째 카드 추가
                playerCard.Add(suffledcards.Numbercard(3).ToString()); // 플레이어의 카드 목록에 셔플된 카드 중 네번째 카드 추가
                int dealertotal = suffledcards.Numbercard(0).Point + suffledcards.Numbercard(2).Point; // 딜러 카드의 총합 변수 생성
                int playertotal = suffledcards.Numbercard(1).Point + suffledcards.Numbercard(3).Point; // 플레이어 카드의 총합 변수 생성
                string sidebetting = "";
                double sidebets = 0; // 사이드배팅 금액

                Console.WriteLine($"딜러의 카드는 {dealerCard[0]}입니다"); // 딜러의 첫번째 카드만 알려줌
                Console.WriteLine($"당신의 카드는 {playerCard[0]},{playerCard[1]}입니다"); // 플레이어의 카드 두개을 알려줌
                Console.WriteLine($"당신의 점수는 {playertotal}입니다"); // 플레이어 점수 총합을 알려줌


                if (dealerCard[0].Contains("Ace"))   // 만약 딜러의 첫번째 카드가 Ace면 플레이어는 side 베팅을 할 수 있다
                {
                    Console.WriteLine("딜러의 카드가 Ace가 나왔습니다. side 베팅에 참여하시겠습니까?(Y/N)");
                    string answer2 = Console.ReadLine();

                    if (answer2 == "Y")        // 사이드 베팅에 참여할 때
                    {
                        Console.WriteLine("당신은 사이드 배팅에 참여하였습니다");
                    Back:
                        Console.WriteLine("배팅 금액을 입력하세요");
                        string strsidebet = Console.ReadLine();

                        if (strbetamount.All(char.IsDigit) == false)
                        {
                            Console.WriteLine("잘못된 답을 입력하였습니다. 숫자로만 이루어진 답변으로 다시 입력해주세요");
                            goto Back;
                        }
                        double sidebet = double.Parse(strsidebet);
                        balance -= sidebet;

                        if (balance < 0)
						{
                            Console.WriteLine("현재 보유자산보다 높은 금액을 입력하였습니다. 다시 입력해주세요");
                            goto Back;
                        }
                        Console.WriteLine($"당신의 배팅금액은 {sidebet}입니다. 당신의 남은 보유자산은 {balance}입니다");
                        sidebets = sidebet;
                        sidebetting = "True";
                    }

                }

                // 딜러의 첫번째 카드가 Ace가 아닐 때
                if (dealerCard[0].Contains("10")) // 딜러의 첫번째 카드의 포인트가 10인 경우
                {
                    if (dealerCard[1].Contains("Ace")) // 딜러의 두번째 카드가 Ace여서 블랙잭일 경우
                    {
                        if (playerCard.Contains("Ace") && playerCard.Contains("10"))  // 플레이어도 블랙잭인 경우(비김)
                        {
                            balance += betamount;
                            Console.WriteLine("딜러의 첫번째 카드의 점수가 10점이므로 나머지 카드도 확인하겠습니다");
                            Console.WriteLine($"딜러의 두번째 카드는 {dealerCard[1]}이므로 딜러는 블랙잭입니다. 플레이어의 카드도 블랙잭이므로 무승부입니다. 베팅금액이 환수됩니다");
                        }

                        else // 딜러만 블랙잭일 경우
						{
                            Console.WriteLine("딜러의 첫번째 카드의 점수가 10점이므로 나머지 카드도 확인하겠습니다");
                            Console.WriteLine($"딜러의 두번째 카드는 {dealerCard[1]}이므로 딜러는 블랙잭입니다. 패배하였습니다");
						}
                    }

                    else
                    {
                        if (playerCard.Contains("Ace") && playerCard.Contains("10"))  // 플레이어만 블랙잭인 경우(이김)
                        {
                            balance += betamount * 2.5;
                            Console.WriteLine("딜러의 첫번째 카드의 점수가 10점이므로 나머지 카드도 확인하겠습니다");
                            Console.WriteLine($"딜러의 두번째 카드는 {dealerCard[1]}이므로 딜러는 블랙잭이 아닙니다");
                            Console.WriteLine("당신은 블랙잭입니다.베팅금액의 2.5배를 얻습니다");
                        }
                        else
						{
                            Console.WriteLine("딜러의 첫번째 카드의 점수가 10점이므로 나머지 카드도 확인하겠습니다");
                            balance += betamount ;
                            Console.WriteLine($"딜러의 두번째 카드는 {dealerCard[1]}이므로 딜러는 블랙잭이 아닙니다. 베팅금은 받고 게임은 종료됩니다");
                        }
                    }
                }

                else if (playertotal == 21)  // 플레이어만 블랙잭일때 
                {
                    Console.WriteLine("축하합니다!! 당신은 블랙잭입니다. 베팅금액의 2.5배를 얻습니다");
                    balance += 2.5 * betamount;
                }


                else   // 딜러의 첫번째 카드가 Ace나 10류가 아닐 때
                {
                    Second:
                    Console.WriteLine("당신은 Stand, Hit, Double down, Surrender중에 선택할 수 있습니다. 어떤 것을 고르시겠습니까?");
                    string answer3 = Console.ReadLine();

                    if(answer3 == "Surrender")  // Surrender를 입력했을 때 배팅금의 절반을 돌려줌
                    {
                        Console.WriteLine("Surrender를 입력하셨습니다. 베팅금액의 절반이 돌아오고 게임이 종료됩니다");
                        balance += betamount / 2;
                    }

                    else if(answer3 == "Stand")     // Stand를 입력했을 때 
					{
                        Console.WriteLine($"당신의 점수는 {playertotal}입니다. 이제 딜러의 카드를 오픈하겠습니다.");
                        Console.WriteLine($"딜러의 카드 두 장은 {dealerCard[0]},{dealerCard[1]}입니다.");

                        
                        if (dealertotal >= 17) // 딜러 카드 둘의 합이 17이 넘을 때
						{
                            Console.WriteLine($"딜러 점수의 총합이 {dealertotal}으로 17이상 입니다. 카드를 더 받지 않습니다");

                            if (dealertotal > playertotal)
                            {
                                Console.WriteLine($"딜러 점수의 총합:{dealertotal} > 플레이어 점수 총합:{playertotal}입니다. 당신은 패배하였습니다");
                            }
                            else if (dealertotal == playertotal)
                            {
                                Console.WriteLine($"딜러 점수의 총합:{dealertotal} = 플레이어 점수 총합:{playertotal}입니다. 당신은 비겼습니다. 베팅금액을 돌려받습니다");
                                balance += betamount;
                            }
                            else
							{
                                Console.WriteLine($"딜러 점수의 총합:{dealertotal} < 플레이어 점수 총합:{playertotal}입니다. 당신은 이겼습니다. 베팅금액의 2배를 얻습니다");
                                balance += betamount;
                            }
                        }

                        else // 딜러 카드 둘의 합이 17 미만일 때
                        {
                            while (dealertotal < 17)  // 딜러의 카드 총합이 17이상이 될 때까지 카드를 뽑는다
                            {
                                int i = 4;
                                Console.WriteLine($"딜러 카드의 총합이 {dealertotal}으로 17 미만입니다. 카드를 한장 더 받겠습니다");
                                dealertotal += suffledcards.Numbercard(i).Point;
                                Console.WriteLine($"딜러의 카드가 {suffledcards.Numbercard(i)}이 나왔습니다. 따라서 딜러의 카드 총합은 {dealertotal}입니다");
                                i++;
                            }

                            if (dealertotal > 21) // 딜러의 버스트로 플레이어 승리
							{
                                Console.WriteLine($"딜러 점수의 총합이 {dealertotal}으로 21보다 큽니다. 딜러의 Bust로 당신은 이겼습니다. 베팅금의 2배를 얻게 됩니다");
                                balance += betamount * 2;
                            }

                            else // 딜러의 점수 총합이 21이 안될 때
							{
                                if (dealertotal > playertotal)
                                {
                                    Console.WriteLine($"딜러 점수의 총합:{dealertotal} > 플레이어 점수 총합:{playertotal}입니다. 당신은 패배하였습니다");
                                }
                                else if (dealertotal == playertotal)
                                {
                                    Console.WriteLine($"딜러 점수의 총합:{dealertotal} = 플레이어 점수 총합:{playertotal}입니다. 당신은 비겼습니다. 베팅금액을 돌려받습니다");
                                    balance += betamount;
                                }
                                else
                                {
                                    Console.WriteLine($"딜러 점수의 총합:{dealertotal} < 플레이어 점수 총합:{playertotal}입니다. 당신은 이겼습니다. 베팅금액의 2배를 얻습니다");
                                    balance += 2 * betamount;
                                }
                            }
                        } 
					}
                    else if (answer3 == "Double down") // Double down을 선택했을 때
					{
                        if (balance - betamount < 0)  // 남은 돈보다 베팅금액이 많을 때 돌아간다
						{
                            Console.WriteLine("베팅금액이 남은 잔액보다 많습니다. 다시 선택해주세요");
                            goto Second;
						}

                        else  
						{
                            balance -= betamount;
                            Console.WriteLine($"베팅금액이 두 배가 되고 카드 한 장만 더 받게 됩니다. 남은 보유자산은 {balance}입니다");
                            Console.WriteLine($"당신의 다음 카드가 {suffledcards.Numbercard(4)}이 나왔습니다"); 
                            playertotal += suffledcards.Numbercard(4).Point; // 플레이어 점수에 4번째 카드를 더해준다


                            if (playertotal > 21)
							{
                                Console.WriteLine($"당신의 점수는 {playertotal}입니다. 당신은 패배하였습니다");
							}

                            else
							{
                                Console.WriteLine($"당신의 점수는 {playertotal}입니다. 이제 딜러의 카드를 오픈하겠습니다.");
                                Console.WriteLine($"딜러의 카드 두 장은 {dealerCard[0]},{dealerCard[1]}입니다.");


                                if (dealertotal >= 17) // 딜러 카드 둘의 합이 17이 넘을 때
                                {
                                    Console.WriteLine($"딜러 점수의 총합이 {dealertotal}으로 17이상 입니다. 카드를 더 받지 않습니다");

                                    if (dealertotal > playertotal)
                                    {
                                        Console.WriteLine($"딜러 점수의 총합:{dealertotal} > 플레이어 점수 총합:{playertotal}입니다. 당신은 패배하였습니다");
                                    }
                                    else if (dealertotal == playertotal)
                                    {
                                        Console.WriteLine($"딜러 점수의 총합:{dealertotal} = 플레이어 점수 총합:{playertotal}입니다. 당신은 비겼습니다. 베팅금액을 돌려받습니다");
                                        balance += 2 * betamount;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"딜러 점수의 총합:{dealertotal} < 플레이어 점수 총합:{playertotal}입니다. 당신은 이겼습니다!! 베팅금액의 2배를 얻습니다");
                                        balance += 4 * betamount;
                                    }
                                }

                                else // 딜러 카드 둘의 합이 17 미만일 때
                                {
                                    while (dealertotal < 17)  // 딜러의 카드 총합이 17이상이 될 때까지 카드를 뽑는다
                                    {
                                        int i = 5;
                                        Console.WriteLine($"딜러 카드의 총합이 {dealertotal}으로 17 미만입니다. 카드를 한장 더 받겠습니다");
                                        dealertotal += suffledcards.Numbercard(i).Point;
                                        Console.WriteLine($"딜러의 카드가 {suffledcards.Numbercard(i)}이 나왔습니다. 따라서 딜러의 카드 총합은 {dealertotal}입니다");
                                        i++;
                                    }

                                    if (dealertotal > 21) // 딜러의 버스트로 플레이어 승리
                                    {
                                        Console.WriteLine($"딜러 점수의 총합이 {dealertotal}으로 21보다 큽니다. 딜러의 Bust로 당신은 이겼습니다. 베팅금의 2배를 얻게 됩니다");
                                        balance += betamount * 4;
                                    }

                                    else // 딜러의 점수 총합이 21이 안될 때
                                    {
                                        if (dealertotal > playertotal)
                                        {
                                            Console.WriteLine($"딜러 점수의 총합:{dealertotal} > 플레이어 점수 총합:{playertotal}입니다. 당신은 패배하였습니다");
                                        }
                                        else if (dealertotal == playertotal)
                                        {
                                            Console.WriteLine($"딜러 점수의 총합:{dealertotal} = 플레이어 점수 총합:{playertotal}입니다. 당신은 비겼습니다. 베팅금액을 돌려받습니다");
                                            balance += 2 * betamount;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"딜러 점수의 총합:{dealertotal} < 플레이어 점수 총합:{playertotal}입니다. 당신은 이겼습니다. 베팅금액의 2배를 얻습니다");
                                            balance += 4 * betamount;
                                        }
                                    }
                                }
                            }
                        }
					}
                    else if (answer3 == "Hit") // 답변을 Hit라고 입력했을 때
					{
                        Console.WriteLine("당신은 Hit를 선택하였습니다. 카드를 한장 더 받습니다");
                        playertotal += suffledcards.Numbercard(4).Point; // 플레이어 점수에 4번째 카드를 더해준다
                        Console.WriteLine($"당신의 다음 카드가 {suffledcards.Numbercard(4)}이 나왔습니다");
                        int a = 5;


                        while (true)
						{
                            if (playertotal > 21) // 플레이어의 점수가 21점을 넘길 때
                            {
                                Console.WriteLine($"당신의 점수는 {playertotal}로 21을 초과하였습니다.");
                                break;
                            }

                            else
                            {
                                One:
                                Console.WriteLine($"당신의 점수는 {playertotal}입니다. 카드를 한 장 더 뽑으시겠습니까?(Y,N)");
                                string answer5 = Console.ReadLine();
                                
                                if(answer5 == "Y")
								{
                                    
                                    Console.WriteLine($"당신의 다음 카드가 {suffledcards.Numbercard(a)}이 나왔습니다");
                                    playertotal += suffledcards.Numbercard(a).Point; // 플레이어 점수에 a번째 카드를 더해준다
                                    a += 1;
                                }

                                else if(answer5 == "N")
								{
                                    break;
								}

                                else
								{
                                    Console.WriteLine("잘못된 답변을 입력했습니다. 다시 입력해주십시오");
                                    goto One;
                                }

                            }
                        }
                        if (playertotal > 21)
                        {
                            Console.WriteLine("당신은 패배하였습니다");
                        }

                        else
						{
                            Console.WriteLine($"당신의 점수는 {playertotal}입니다. 이제 딜러의 카드를 오픈하겠습니다.");
                            Console.WriteLine($"딜러의 카드 두 장은 {dealerCard[0]},{dealerCard[1]}입니다.");


                            if (dealertotal >= 17) // 딜러 카드 둘의 합이 17이 넘을 때
                            {
                                Console.WriteLine($"딜러 점수의 총합이 {dealertotal}으로 17이상 입니다. 카드를 더 받지 않습니다");

                                if (dealertotal > playertotal)
                                {
                                    Console.WriteLine($"딜러 점수의 총합:{dealertotal} > 플레이어 점수 총합:{playertotal}입니다. 당신은 패배하였습니다");
                                }
                                else if (dealertotal == playertotal)
                                {
                                    Console.WriteLine($"딜러 점수의 총합:{dealertotal} = 플레이어 점수 총합:{playertotal}입니다. 당신은 비겼습니다. 베팅금액을 돌려받습니다");
                                    balance += betamount;
                                }
                                else
                                {
                                    Console.WriteLine($"딜러 점수의 총합:{dealertotal} < 플레이어 점수 총합:{playertotal}입니다. 당신은 이겼습니다!! 베팅금액의 2배를 얻습니다");
                                    balance += 2 * betamount;
                                }
                            }

                            else // 딜러 카드 둘의 합이 17 미만일 때
                            {
                                while (dealertotal < 17)  // 딜러의 카드 총합이 17이상이 될 때까지 카드를 뽑는다
                                {

                                    Console.WriteLine($"딜러 카드의 총합이 {dealertotal}으로 17 미만입니다. 카드를 한장 더 받겠습니다");
                                    dealertotal += suffledcards.Numbercard(a).Point;
                                    Console.WriteLine($"딜러의 카드가 {suffledcards.Numbercard(a)}이 나왔습니다. 따라서 딜러의 카드 총합은 {dealertotal}입니다");
                                    a++;
                                }

                                if (dealertotal > 21) // 딜러의 버스트로 플레이어 승리
                                {
                                    Console.WriteLine($"딜러 점수의 총합이 {dealertotal}으로 21보다 큽니다. 딜러의 Bust로 당신은 이겼습니다. 베팅금의 2배를 얻게 됩니다");
                                    balance += betamount * 2;
                                }

                                else // 딜러의 점수 총합이 21이 안될 때
                                {
                                    if (dealertotal > playertotal)
                                    {
                                        Console.WriteLine($"딜러 점수의 총합:{dealertotal} > 플레이어 점수 총합:{playertotal}입니다. 당신은 패배하였습니다");
                                    }
                                    else if (dealertotal == playertotal)
                                    {
                                        Console.WriteLine($"딜러 점수의 총합:{dealertotal} = 플레이어 점수 총합:{playertotal}입니다. 당신은 비겼습니다. 베팅금액을 돌려받습니다");
                                        balance += betamount;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"딜러 점수의 총합:{dealertotal} < 플레이어 점수 총합:{playertotal}입니다. 당신은 이겼습니다. 베팅금액의 2배를 얻습니다");
                                        balance += 2 * betamount;
                                    }
                                }
                            }
                        }


                    }


                    else
					{
                        Console.WriteLine("잘못된 답변을 입력했습니다. 정확한 스펠링을 입력해주십시오(첫번째 글자 대문자)");
                        goto Second;
					}
                }

                if(sidebetting == "True")
				{
                    Console.WriteLine("사이드 배팅의 결과를 알려드리겠습니다");
                    if (dealerCard[1].Contains("10"))
                    {
                        balance += 2 * sidebets;         // 딜러가 블랙잭이 나올 시 사이드 베팅의 두배를 번다
                        Console.WriteLine($"축하합니다! 딜러는 블랙잭이었습니다. 당신은 사이드 베팅에 성공하였습니다.{sidebets * 2}을 얻습니다");
                    }

                    else
                    {
                        Console.WriteLine("딜러는 블랙잭이 아니였습니다.사이드 베팅에 실패하였습니다"); //달러가 블랙잭이 아니면 돈을 잃고 끝난다
                    }
                }


                holdings = balance;
                Console.WriteLine($"게임이 종료되었습니다. 당신의 보유자산은 {balance}입니다");
                
                if(balance >= 2000000)
				{
                    Console.WriteLine("축하합니다!!! 당신의 보유자금이 200만원이상을 달성하였습니다. 게임에 승리하였습니다");
                    break;
				}

                else if(balance <= 0)
				{
                    Console.WriteLine("당신의 보유자금이 0이 되었습니다. 당신은 게임에서 패배했습니다.");
                    break;
				}
   
            }
        }
    
	}
}
