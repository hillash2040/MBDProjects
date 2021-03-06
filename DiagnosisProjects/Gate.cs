﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagnosisProjects
{
    public abstract class Gate : IComparable<Gate>
    {
        public int Id { get; protected set; }
        public int order { get; set; }
        public double P { get; set; } // pre determined probability for fault
        public double Cost { get; set; }
        public bool IsNotHealthy { get; set; } // needed for the SAT - consistency test
        public enum Type {and, or, xor, nxor, nor, nand, buffer, not, cone}
        public Type type;
        private Wire output;
        public virtual Wire Output {
            get
            {
                return output;
            }
            set 
            {
                output = value;
                output.InputComponent = this; 
            } 
        }

        public Gate()
        {
            P = 0.01;
            Cost = 5;
        }
        public Gate(double cost, double p)
        {
            this.Cost = cost;
            this.P = p;
        }
        public virtual List<Wire> getInput() { return null; }


        public virtual bool GetValue() { return Output.Value; }

        /*virtual - cone */
        public virtual void SetValue() // give the output wire the value that it should have
        {
            Output.Value = GetValue();
        }
        public static int CompareComponents(Gate x, Gate y)
        {
            if (x == null || y == null)
                return 0;
            if (x.order == y.order)
                return 0;
            if (x.order > y.order)
                return 1;
            else
                return -1;
        }

        public int CompareTo(Gate other)
        {
            if (Id == other.Id)
            {
                return 0;
            }
            return 1;
        }

        public abstract void AddConstaint();
    }
}
