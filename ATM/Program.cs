using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Money> money = readInfo();

            Console.WriteLine("Какую сумму требуется выдать?");
            string strInputSumm = Console.ReadLine();
            
            try
            {
                long inputSumm = Convert.ToInt32(strInputSumm);
                if (money.Sum(m => m.getSumm()) < inputSumm)
                {
                    Console.WriteLine("В банкомате недостаточно денег");
                }
                else if(inputSumm == 0)
                {
                    Console.WriteLine("Введено неверное значение");
                }
                else if (money.Sum(m => m.getSumm()) == inputSumm)
                {
                    printOutMoney(money);
                }
                else
                {
                    List<Money> neededSumm = getNeededMoney(inputSumm, money);
                    if (neededSumm.Count == 0)
                    {
                        Console.WriteLine("Невозможно выдать указанную сумму");
                    }
                    else
                    {
                        printOutMoney(neededSumm);
                    }
                }
                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine("Введено неверное значение");
                Console.ReadLine();
            }
        }

        static List<Money> getNeededMoney(long neededSumm, List<Money> ATMMoney)
        {
            List<Money> availableMoney = ATMMoney;
            List<Money> neededMoney = new List<Money>();
            long left = neededSumm;

            foreach(var m in availableMoney.OrderByDescending(m => m.Nominal ))
            {
                if (m.Count > 0)
                {
                    for (int i = m.Count;  i > 0; i--)
                    {
                        if (i * m.Nominal <= left)
                        {
                            neededMoney.Add(new Money(m.Nominal, i));
                            m.Count -= i;
                            left -= i * m.Nominal;

                            break;
                        }
                    }
                }

                if (left == 0)
                {
                    break;
                }
            }

            if (left > 0)
            {
                neededMoney.Clear();
            }

            return neededMoney;
        }


        static List<Money> readInfo()
        {
            List<Money> money = new List<Money>();
            string filePath = @"C:\Temp\ATM.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string tmp = reader.ReadLine();
                    string[] nums = tmp.Split(":");

                    money.Add(new Money(Convert.ToInt32(nums[0]), Convert.ToInt32(nums[1])));
                }
            }

            return money;
        }

        static void printOutMoney (List<Money> money)
        {
            Console.WriteLine("Выдано: ");
            foreach (var m in money)
            {
                Console.WriteLine("Купюра " + m.Nominal + "; Количество: " + m.Count);
            }
        }
    }
}
