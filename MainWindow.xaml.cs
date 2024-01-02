﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace game
{
    /// <summary>
    /// Interaction logic for MainWindow.xamls
    /// </summary>
    public partial class MainWindow : Window
    {
        Ellipse ball = new Ellipse();
        // Polygon enemy = new Polygon();  
        Rectangle door = new Rectangle();
        Rectangle enemy = new Rectangle();
        Rectangle enemy1 = new Rectangle();
        Rectangle enemy2 = new Rectangle();
        Rect kutu; //?
        int formW = 1200;  // Form 
        int formH = 600;
        int ballW = 40;
        int ballH = 40;
        private DateTime startTime;
        private DateTime endTime;
        private TextBlock timeTextBlock;

        public MainWindow()
        {
            InitializeComponent();
            Canvas1.Children.Add(ball);
            Canvas1.Children.Add(enemy);
            Canvas1.Children.Add(enemy1);
            Canvas1.Children.Add(enemy2);
            Canvas1.Children.Add(door);

        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            startTime = DateTime.Now;
            Canvas1.Children.RemoveAt(0);
            Canvas1.Children.RemoveAt(0);
            setBall();
            setDoor();
            setEnemy();
            setStaticEnemy();
            setStaticEnemy2();
            enemyMove();
        }
        private void setBall()
        {
            ball.Tag = "ball";
            ball.Width = ballW;
            ball.Height = ballH;
            ball.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"https://www.pngmart.com/files/1/Fire-Flame-PNG-Clipart.png", UriKind.Absolute))
            };
            Canvas.SetLeft(ball, 1);
            Canvas.SetTop(ball, 450);
        }
        private void setDoor()
        {
            door.Width = 150;
            door.Height = 200;
            door.Tag = "door";
            door.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"file:///C:/Users/talha/Downloads/image-PhotoRoom.png-PhotoRoom%20(2).png", UriKind.Absolute))
            };
            Canvas.SetLeft(door, 880);
            Canvas.SetTop(door, 300);
        }
        private void setEnemy()
        {
            enemy.Width = 80;
            enemy.Height = 50;
            enemy.Tag = "enemy";
            enemy.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"file:///C:/Users/talha/Downloads/image-PhotoRoom.png-PhotoRoom%20(3).png", UriKind.Absolute))
            };
            Canvas.SetLeft(enemy, 880);
            Canvas.SetTop(enemy, 450);

        }
        private void setStaticEnemy()
        {

            enemy1.Width = 100;
            enemy1.Height = 50;
            enemy1.Tag = "enemy";
            enemy1.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"file:///C:/Users/talha/Downloads/image-PhotoRoom.png-PhotoRoom%20(1).png", UriKind.Absolute))
            };
            Canvas.SetLeft(enemy1, 400);
            Canvas.SetTop(enemy1, 450);

        }
        private void setStaticEnemy2()
        {

            enemy2.Width = 100;
            enemy2.Height = 50;
            enemy2.Tag = "enemy";
            enemy2.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"file:///C:/Users/talha/Downloads/image-PhotoRoom.png-PhotoRoom%20(1).png", UriKind.Absolute))
            };
            Canvas.SetLeft(enemy2, 700);
            Canvas.SetTop(enemy2, 450);

        }
        private async void enemyMove()
        {
            double y;
            while (true)
            {
                Random rand = new Random();
                int randomNumber1 = rand.Next(100, 170);
                y = Canvas.GetLeft(enemy) - 10;
                // if (x < 0) x = 0;
                Canvas.SetLeft(enemy, y);
                await Task.Delay(5);

                if (Canvas.GetLeft(enemy) < -40)
                {
                    Canvas.SetLeft(enemy, 980);
                    Canvas.SetTop(enemy, randomNumber1);
                }

                kutu = new Rect(Canvas.GetLeft(ball), Canvas.GetTop(ball), ball.Width, ball.Height);

                foreach (var x in Canvas1.Children.OfType<Rectangle>())
                {
                    Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if ((string)x.Tag == "door")
                    {
                        if (kutu.IntersectsWith(hitBox))
                        {
                            Canvas1.Background = new ImageBrush
                            {
                                ImageSource = new BitmapImage(new Uri(@"file:///C:/Users/talha/OneDrive/Masa%C3%BCst%C3%BC/atesss.png", UriKind.Absolute))
                            };
                            endTime = DateTime.Now;
                            TimeSpan elapsedTime = endTime - startTime;
                            int seconds = (int)elapsedTime.TotalSeconds;
                            GameOver("Tebrikler, Ortalığı ateşe vermeyi başardın hemde" + " " + seconds.ToString() + " " + "saniyede yaptın");
                        }
                    }

                    if ((string)x.Tag == "enemy")
                    {
                        if (kutu.IntersectsWith(hitBox))
                        {
                            ball.Fill = Brushes.Gray;
                            GameOver("Ahh Olamaz ! Üstüne su sıçradı ve söndün");
                        }
                    }
                }
            }
        }
        bool isAir = false;
        private async void person_Jump()
        {
            isAir = true;
            double x, y;
            for (int i = 0; i < 40; i++)
            {
                y = Canvas.GetTop(ball) - 10;
                if (y < 0) y = 0;
                Canvas.SetTop(ball, y);
                await Task.Delay(5);
            }

            for (int i = 0; i < 40; i++)
            {
                y = Canvas.GetTop(ball) + 10;
                if (y > formH - ballH) y = formH - ballH;
                Canvas.SetTop(ball, y);
                await Task.Delay(5);
            }
            isAir = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            double x, y;
            switch (e.Key)
            {
                case Key.A:
                    x = Canvas.GetLeft(ball) - 20;
                    if (x < 0) x = 0;
                    Canvas.SetLeft(ball, x);
                    break;

                case Key.D:
                    x = Canvas.GetLeft(ball) + 20;
                    if (x > formW - ballW) x = formW - ballW;
                    Canvas.SetLeft(ball, x);
                    break;

                case Key.Space:
                    if (!isAir)
                        person_Jump();
                    break;
            }


        }
        private void GameOver(string message)
        {
            MessageBox.Show(message, "Ortalığı ateşe ver!");
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

    }
}
