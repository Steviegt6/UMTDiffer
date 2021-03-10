﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace UndertaleModLib.Models
{
    public class UndertaleTimeline : UndertaleNamedResource, INotifyPropertyChanged
    {
        public class UndertaleTimelineMoment : UndertaleObject, INotifyPropertyChanged
        {
            private uint _Step { get; set; }
            public uint Step { get => _Step; set { _Step = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Step")); } }

            private UndertalePointerList<UndertaleGameObject.EventAction> _Event { get; set; }
            public UndertalePointerList<UndertaleGameObject.EventAction> Event { get => _Event; set { _Event = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Event")); } }

            public event PropertyChangedEventHandler PropertyChanged;

            public UndertaleTimelineMoment()
            {
                /*
                Step = 0;
                Event = new UndertalePointerList<UndertaleGameObject.EventAction>();
                */
            }

            public UndertaleTimelineMoment(uint step, UndertalePointerList<UndertaleGameObject.EventAction> ev)
            {
                Step = step;
                Event = ev;
            }

            public void Serialize(UndertaleWriter writer)
            {
                // Since GM:S stores Steps first, and then Events, we can't serialize a single entry in a single function :(
            }

            public void Unserialize(UndertaleReader reader)
            {
                // Same goes for unserializing.
            }
        }

        private UndertaleString _Name;
        private ObservableCollection<UndertaleTimelineMoment> _Moments = new ObservableCollection<UndertaleTimelineMoment>();

        public UndertaleString Name { get => _Name; set { _Name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); } }
        public ObservableCollection<UndertaleTimelineMoment> Moments { get => _Moments; set { _Moments = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Moments")); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Name.Content + " (" + GetType().Name + ")";
        }

        public void Serialize(UndertaleWriter writer)
        {
            writer.WriteUndertaleString(Name);

            writer.Write(Moments.Count);
            for (int i = 0; i < Moments.Count; i++)
            {
                // Write the time point
                writer.Write(Moments[i].Step);

                // Unnecessary pointer to next array
                writer.WriteUndertaleObjectPointer(Moments[i].Event);
            }

            for (int i = 0; i < Moments.Count; i++)
            {
                // Write the actions for this moment
                writer.WriteUndertaleObject(Moments[i].Event);
            }
        }

        public void Unserialize(UndertaleReader reader)
        {
            Name = reader.ReadUndertaleString();

            int momentCount = reader.ReadInt32();

            // Read the time points for each moment
            uint[] timePoints = new uint[momentCount];
            int[] unnecessaryPointers = new int[momentCount];
            for (int i = 0; i < momentCount; i++)
            {
                timePoints[i] = reader.ReadUInt32();
                unnecessaryPointers[i] = reader.ReadInt32();
            }

            // Read the actions for each moment
            for (int i = 0; i < momentCount; i++)
            {
                if (reader.Position != unnecessaryPointers[i])
                    throw new UndertaleSerializationException("Invalid action list pointer");

                // Read action list and assign time point (put into list)
                var timeEvent = reader.ReadUndertaleObject<UndertalePointerList<UndertaleGameObject.EventAction>>();
                Moments.Add(new UndertaleTimelineMoment(timePoints[i], timeEvent));
            }
        }
    }
}
