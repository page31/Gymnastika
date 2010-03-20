﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Timers;
using Gymnastika.Sync.Communication;
using Gymnastika.Sync.Models;

namespace Gymnastika.Sync.Schedule
{
    public class TimerMetadata
    {
        public TimerMetadata(Timer timer, ScheduleItem scheduleItem)
        {
            Timer = timer;
            ScheduleItem = scheduleItem;
        }

        public Timer Timer { get; set; }
        public ScheduleItem ScheduleItem { get; set; }
        public Connection Connection { get; set; }
    }
}