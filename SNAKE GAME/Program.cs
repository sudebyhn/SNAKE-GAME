using System;
using System.Collections.Generic;
using System.Threading;

class SnakeGame
{
    // Oyun alanı boyutu
    static int width = 20;
    static int height = 10;

    // Yılanın parçalarının koordinatlarını tutacak liste
    static List<int[]> snake = new List<int[]>();

    // Yılanın başlangıç konumu ve yönü
    static int[] head = new int[] { width / 2, height / 2 };
    static int[] direction = new int[] { 0, 1 }; // Başlangıçta sağa hareket

    // Oyunun devam edip etmediğini kontrol eden değişken
    static bool isGameOver = false;

    // Yem'in koordinatları
    static int[] food = new int[2];

    static void Main()
    {
        Console.CursorVisible = false;
        Console.SetWindowSize(width + 1, height + 1);

        // Yılanı oluştur
        snake.Add(head);

        // İlk yemi oluştur
        PlaceFood();

        // Oyun döngüsü
        while (!isGameOver)
        {
            Draw();
            Thread.Sleep(200); // Hızı kontrol eder
            Input();
            Logic();
        }

        Console.Clear();
        Console.WriteLine("Oyun bitti! Skor: " + (snake.Count - 1));
        Console.ReadKey();
    }

    static void Draw()
    {
        Console.Clear();

        // Yılanı çiz
        foreach (int[] part in snake)
        {
            Console.SetCursorPosition(part[0], part[1]);
            Console.Write("■");
        }

        // Yemi çiz
        Console.SetCursorPosition(food[0], food[1]);
        Console.Write("O");
    }

    static void Input()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (direction[1] != 1)
                        direction = new int[] { 0, -1 };
                    break;
                case ConsoleKey.DownArrow:
                    if (direction[1] != -1)
                        direction = new int[] { 0, 1 };
                    break;
                case ConsoleKey.LeftArrow:
                    if (direction[0] != 1)
                        direction = new int[] { -1, 0 };
                    break;
                case ConsoleKey.RightArrow:
                    if (direction[0] != -1)
                        direction = new int[] { 1, 0 };
                    break;
            }
        }
    }

    static void Logic()
    {
        // Yılanın yeni başı
        int[] newHead = new int[] { head[0] + direction[0], head[1] + direction[1] };

        // Yılanın kendi kendine çarpması veya oyun alanını terk etmesi durumu
        if (snake.Contains(newHead) || newHead[0] < 0 || newHead[0] >= width || newHead[1] < 0 || newHead[1] >= height)
        {
            isGameOver = true;
            return;
        }

        // Yılanın yeni başını ekleyerek hareketi gerçekleştir
        snake.Insert(0, newHead);
        head = newHead;

        // Yemi yeme durumu
        if (head[0] == food[0] && head[1] == food[1])
        {
            PlaceFood();
        }
        else
        {
            snake.RemoveAt(snake.Count - 1); // Yılanın kuyruğunu kısalt
        }
    }

    static void PlaceFood()
    {
        Random rand = new Random();
        food[0] = rand.Next(0, width);
        food[1] = rand.Next(0, height);

        // Yem yılanın üzerine düşmemeli
        while (snake.Contains(food))
        {
            food[0] = rand.Next(0, width);
            food[1] = rand.Next(0, height);
        }
    }
}
