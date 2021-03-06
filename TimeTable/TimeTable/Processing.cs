﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Serialization;

namespace TimeTable
{
    public class Data
    {
        public string version { get; set; }

        public int lastlectid { get; set; }
        public List<List<int>> lectid { get; set; }

        public List<int> taskid { get; set; }

        public List<LectTime> lecttime { get; set; }

        public TimetableSetting setting { get; set; }

        [XmlIgnore]
        public Dictionary<int, Lecture> lectures { get; set; }

        [XmlIgnore]
        public Dictionary<int, Task> tasks { get; set; }

        public List<Lecture> lectures_list { get; set; }

        public List<Task> tasks_list { get; set; }

        public Data()
        {
            var set = new TimetableSetting();
            this.setting = set;
            lecttime = new List<LectTime>();
            lectid = new List<List<int>>();
            taskid = new List<int>();
            lectures = new Dictionary<int, Lecture>();
            tasks = new Dictionary<int, Task>();
            lectures_list = new List<Lecture>();
            tasks_list = new List<Task>();
        }

    }

    public class TimetableSetting
    {
        public int period { get; set; }

        public int day_st { get; set; }

        public int day_en { get; set; }

        public string timer_music { get; set; }

        public bool display_mon { get; set; }

        public bool display_tue { get; set; }

        public bool display_wed { get; set; }
        public bool display_thu { get; set; }

        public bool display_fri { get; set; }

        public bool display_sat { get; set; }

        public TimetableSetting()
        {
            this.period = 8;
            this.day_st = 0;
            this.day_en = 0;
            this.timer_music = "./elise.mp3";
            this.display_mon = true;
            this.display_tue = true;
            this.display_wed = true;
            this.display_thu = true;
            this.display_fri = true;
            this.display_sat = true;
        }
    }

    public class Lecture
    {
        // IDは被らないようにする！方法は未定！ 削除しながらやると思うからLastIDとかで保持する方法にするかも！
        public int id { get; set; }

        public int period { get; set; }

        public int dayoftheweek { get; set; }

        public string name { get; set; }

        public string professor { get; set; }

        public int continuous { get; set; }

        public string syllabus { get; set; }

        public string style { get; set; }

        public List<string> otherurl { get; set; }

        public List<Lecture_Date> dates { get; set; }

    }

    public class Lecture_URLs
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Lecture_Date
    {
        public int count { get; set; }
        public string date { get; set; }
    }

    public class Task
    {
        public int id { get; set; }

        public string lecture { get; set; }

        public string name { get; set; }

        public string time { get; set; }
    }

    public class LectTime
    {

        public int starthour { get; set; }
        public int startminute { get; set; }
        public int endhour { get; set; }
        public int endminute { get; set; }
    }

    public class LectTemp
    {
        public string version { get; set; }
        public string name { get; set; }
        public List<Lecture> lectures { get; set; }
    }

    public class SetViewModel : INotifyPropertyChanged
    {
        #region TmpItems
        private int _period; // 時限
        private int _day_st; // いつから
        private int _day_en; // いつまで。IDで入れとく予定。方法はわからん
        private string _timer_music; // タイマー音
        private bool _display_mon; // げつ
        private bool _display_tue; // かー
        private bool _display_wed; // すい
        private bool _display_thu; // もく
        private bool _display_fri; // きん
        private bool _display_sat; // どー

        public List<Obj_Combobox> _obj_tf;
        public List<Obj_Combobox> _obj_day_st;
        public List<Obj_Combobox> _obj_day_en;

        #endregion

        #region processing
        public int period
        {
            get { return this._period; }
            set
            {
                this._period = value;
                this.OnPropertyChanged(nameof(period));
            }
        }

        public int day_st
        {
            get { return this._day_st; }
            set
            {
                this._day_st = value;
                this.OnPropertyChanged(nameof(day_st));
            }
        }

        public int day_en
        {
            get { return this._day_en; }
            set
            {
                this._day_en = value;
                this.OnPropertyChanged(nameof(day_en));
            }
        }

        public string timer_music
        {
            get { return this._timer_music; }
            set
            {
                this._timer_music = value;
                this.OnPropertyChanged(nameof(timer_music));
            }
        }

        public bool display_mon
        {
            get { return this._display_mon; }
            set
            {
                this._display_mon = value;
                this.OnPropertyChanged(nameof(display_mon));
            }
        }

        public bool display_tue
        {
            get { return this._display_tue; }
            set
            {
                this._display_tue = value;
                this.OnPropertyChanged(nameof(display_tue));
            }
        }

        public bool display_wed
        {
            get { return this._display_wed; }
            set
            {
                this._display_wed = value;
                this.OnPropertyChanged(nameof(display_wed));
            }
        }

        public bool display_thu
        {
            get { return this._display_thu; }
            set
            {
                this._display_thu = value;
                this.OnPropertyChanged(nameof(display_thu));
            }
        }

        public bool display_fri
        {
            get { return this._display_fri; }
            set
            {
                this._display_fri = value;
                this.OnPropertyChanged(nameof(display_fri));
            }
        }

        public bool display_sat
        {
            get { return this._display_sat; }
            set
            {
                this._display_sat = value;
                this.OnPropertyChanged(nameof(display_sat));
            }
        }

        public List<Obj_Combobox> obj_tf
        {
            get { return this._obj_tf; }
            set
            {
                this._obj_tf = value;
                this.OnPropertyChanged(nameof(obj_tf));
            }
        }

        public List<Obj_Combobox> obj_day_st
        {
            get { return this._obj_day_st; }
            set
            {
                this._obj_day_st = value;
                this.OnPropertyChanged(nameof(obj_day_st));
            }
        }

        public List<Obj_Combobox> obj_day_en
        {
            get { return this._obj_day_en; }
            set
            {
                this._obj_day_en = value;
                this.OnPropertyChanged(nameof(obj_day_en));
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string info)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }


    public class Obj_Combobox
    {
        public bool tf { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class SetTimeViewModel : INotifyPropertyChanged
    {
        private int _st_h;
        private int _st_m;
        private int _en_h;
        private int _en_m;

        private List<int> _obj_h;
        private List<int> _obj_m;

        public int st_h
        {
            get { return this._st_h; }
            set
            {
                this._st_h = value;
                this.OnPropertyChanged(nameof(st_h));
            }
        }

        public int st_m
        {
            get { return this._st_m; }
            set
            {
                this._st_m = value;
                this.OnPropertyChanged(nameof(st_m));
            }
        }

        public int en_h
        {
            get { return this._en_h; }
            set
            {
                this._en_h = value;
                this.OnPropertyChanged(nameof(en_h));
            }
        }

        public int en_m
        {
            get { return this._en_m; }
            set
            {
                this._en_m = value;
                this.OnPropertyChanged(nameof(en_m));
            }
        }

        public List<int> obj_h
        {
            get { return this._obj_h; }
            set
            {
                this._obj_h = value;
                this.OnPropertyChanged(nameof(obj_h));
            }
        }

        public List<int> obj_m
        {
            get { return this._obj_m; }
            set
            {
                this._obj_m = value;
                this.OnPropertyChanged(nameof(obj_m));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string info)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }

    public class SetLectViewModel : INotifyPropertyChanged
    {
        private List<Lecture> _lect_tenp;

        private string _name;
        private string _professor;
        private int _continuous;
        private string _syllabus;
        private string _otherurl1;
        private string _otherurl2;
        private string _otherurl3;

        public List<Lecture> lect_tenp
        {
            get { return this._lect_tenp; }
            set
            {
                this._lect_tenp = value;
                this.OnPropertyChanged(nameof(lect_tenp));
            }
        }

        public string name
        {
            get { return this._name; }
            set
            {
                this._name = value;
                this.OnPropertyChanged(nameof(name));
            }
        }

        public string professor
        {
            get { return this._professor; }
            set
            {
                this._professor = value;
                this.OnPropertyChanged(nameof(professor));
            }
        }

        public int continuous
        {
            get { return this._continuous; }
            set
            {
                this._continuous = value;
                this.OnPropertyChanged(nameof(continuous));
            }
        }

        public string syllabus
        {
            get { return this._syllabus; }
            set
            {
                this._syllabus = value;
                this.OnPropertyChanged(nameof(syllabus));
            }
        }

        public string otherurl1
        {
            get { return this._otherurl1; }
            set
            {
                this._otherurl1 = value;
                this.OnPropertyChanged(nameof(otherurl1));
            }
        }

        public string otherurl2
        {
            get { return this._otherurl2; }
            set
            {
                this._otherurl2 = value;
                this.OnPropertyChanged(nameof(otherurl2));
            }
        }

        public string otherurl3
        {
            get { return this._otherurl3; }
            set
            {
                this._otherurl3 = value;
                this.OnPropertyChanged(nameof(otherurl3));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string info)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}