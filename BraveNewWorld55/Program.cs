using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BraveNewWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char border = '#';
            char treasure = 'S';
            int playerPositionX;
            int playerPositionY;
            int playerDirectionX = 0;
            int playerDitectionY = 0;
            bool isGame = true;
            Console.CursorVisible = false;
            char[,] map = ReadMap("map1", out playerPositionX, out playerPositionY);
            DrawMap(map);

            while (isGame)
            {

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    ChangeDirecrion(key, ref playerDirectionX, ref playerDitectionY);

                    if (map[playerPositionX + playerDirectionX, playerPositionY + playerDitectionY] != border)
                    {
                        Move(ref playerPositionX, ref playerPositionY, playerDirectionX, playerDitectionY);
                    }

                    if (map[playerPositionX, playerPositionY] == treasure)
                    {
                        Console.SetCursorPosition(0, 20);
                        Console.WriteLine("Вы нашли сокровище!");
                        Console.ReadKey();
                        isGame = false;                        
                    }
                }              
            }
        }

        static void Move(ref int playerPositionX, ref int playerPositionY, int playerDirectionX, int playerDitectionY)
        {
            char player = '@';
            Console.SetCursorPosition(playerPositionY, playerPositionX);
            Console.Write(" ");
            playerPositionX += playerDirectionX;
            playerPositionY += playerDitectionY;
            Console.SetCursorPosition(playerPositionY, playerPositionX);
            Console.Write(player);
        }

        static void ChangeDirecrion(ConsoleKeyInfo key, ref int playerDirectionX, ref int playerDitectionY)
        {
            const ConsoleKey upArrow = ConsoleKey.UpArrow;
            const ConsoleKey downArrow = ConsoleKey.DownArrow;
            const ConsoleKey leftArrow = ConsoleKey.LeftArrow;
            const ConsoleKey rightArrow = ConsoleKey.RightArrow;
            playerDirectionX = 0;
            playerDitectionY = 0;

            switch (key.Key)
            {
                case upArrow:
                    playerDirectionX = -1;
                    playerDitectionY = 0;
                    break;
                case downArrow:
                    playerDirectionX = 1;
                    playerDitectionY = 0;
                    break;
                case leftArrow:
                    playerDirectionX = 0;
                    playerDitectionY = -1;
                    break;
                case rightArrow:
                    playerDirectionX = 0;
                    playerDitectionY = 1;
                    break;

            }
        }


        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }


        static char[,] ReadMap(string mapName, out int playerPositionX, out int playerPositionY)
        {
            char player = '@';
            playerPositionX = 0;
            playerPositionY = 0;
            string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == player)
                    {
                        playerPositionX = i;
                        playerPositionY = j;
                    }
                }
            }
            return map;
        }
    }
}
