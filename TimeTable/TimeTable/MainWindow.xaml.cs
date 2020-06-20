﻿using System;
using System.IO;
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
using System.Xml.Serialization;
using TimeTable.Properties;

namespace TimeTable
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Data data;

        public MainWindow()
        {
            InitializeComponent();

            RecoverWindowBounds();

            data = new Data();

            try
            {
                using(FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + "Data.xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    data = (Data)serializer.Deserialize(fs);
                }
            } catch (FileNotFoundException e)
            {

            }

            var sw = new SetWindow();
            sw.ShowDialog();
        }

        public void UpdateWindow(object sender, RoutedEventArgs e)
        {
            // 更新ボタン押した時のヤツ。日付変更を想定
        }

        void RecoverWindowBounds()
        {
            var windowset = Settings.Default;
            // 左
            if (windowset.WindowLeft >= 0 &&
                (windowset.WindowLeft + windowset.WindowWidth) < SystemParameters.VirtualScreenWidth)
            { Left = windowset.WindowLeft; }
            // 上
            if (windowset.WindowTop >= 0 &&
                (windowset.WindowTop + windowset.WindowHeight) < SystemParameters.VirtualScreenHeight)
            { Top = windowset.WindowTop; }
            // 幅
            if (windowset.WindowWidth > 0 &&
                windowset.WindowWidth <= SystemParameters.WorkArea.Width)
            { Width = windowset.WindowWidth; }
            // 高さ
            if (windowset.WindowHeight > 0 &&
                windowset.WindowHeight <= SystemParameters.WorkArea.Height)
            { Height = windowset.WindowHeight; }
        }

        void CreateLecturePanel()
        {
            // 日付別で出す奴。for文っすかね
        }

        void CreateTaskPanel()
        {
            // こっちは適当に taskの子要素にButton増やすだけ
        }

        void PushLectureButton(object sender, RoutedEventArgs e)
        {
            // 講義ボタン 左右で飛ばす
        }

        void PushTaskButton(object sender, RoutedEventArgs e)
        {
            // 講義ボタン 左右で飛ばす
        }

        void PushLeftLectureButton(object sender)
        {
            // 講義の方のボタン押されたとき 左クリック
        }

        void PushRightLectureButton(object sender)
        {
            // 講義の方のボタン押されたとき 右クリック
        }

        void PushLeftTaskButton(object sender)
        {
            // 課題の方のボタン押されたとき 左クリック
        }

        void PushRightTaskButton(object sender)
        {
            // 課題の方のボタン押されたとき 右クリック
        }

        void PushLectureFinishButton(object sender, RoutedEventArgs e)
        {
            // 講義終了ボタン その講義の削除からの更新かな
        }

        void PushTaskFinishButton(object sender, RoutedEventArgs e)
        {
            // 課題終了ボタン 上記と同じく
        }

        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Data));

            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + "Data.xml", FileMode.Create))
            {
                serializer.Serialize(fs, data);
            }

            SaveWindowBounds();
        }

        void SaveWindowBounds()
        {
            var windowset = Settings.Default;
            WindowState = WindowState.Normal;
            windowset.WindowLeft = Left;
            windowset.WindowTop = Top;
            windowset.WindowWidth = Width;
            windowset.WindowHeight = Height;
            windowset.Save();
        }
    }
}