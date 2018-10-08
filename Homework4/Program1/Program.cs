using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{
    class Program
    {

        static void ShowAlarm(object sender, AlarmclockEventArgs e)
        {
            Console.WriteLine(e.dt.ToString() + "已经到了");
        }

        static void Main(string[] args)
        {
            string time = "";
            var alarm = new Alarm();
            alarm.Alarming += ShowAlarm;
            Console.WriteLine("请输入闹钟时间（例05/25/2018 20:50）：");
            time = Console.ReadLine();
            try
            {
                alarm.AlarmTime = DateTime.Parse(time);
            }
            catch
            {
                Console.WriteLine("您输入的闹钟格式不正确！");
            }
            alarm.DoAlarm();
            Console.WriteLine("请按任意键继续...");
            Console.ReadKey();
        }

      
    }

    public class AlarmclockEventArgs : EventArgs
    {
        public DateTime dt;
    }

    public delegate void AlarmclockEventHandler(object sender, AlarmclockEventArgs e);

    public class Alarm
    {
        public event AlarmclockEventHandler Alarming;

        private DateTime time = DateTime.Now;
        private TimeSpan ts1 = TimeSpan.FromMinutes(1);
        public DateTime AlarmTime = DateTime.Today;
        public void DoAlarm()
        {
            if (time > AlarmTime + ts1) return;
            else
            {
                while(time < AlarmTime)
                {
                    time = DateTime.Now;
                }

                if(time >= AlarmTime || time <= AlarmTime + ts1)
                {
                    AlarmclockEventArgs alarmclockEventArgs = new AlarmclockEventArgs();
                    alarmclockEventArgs.dt = time;
                    Alarming(this, alarmclockEventArgs);
                }
            }
        }

    }
}
