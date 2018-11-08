﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    class Staff : IStaffElement
    {
        public List<IStaffElement> Bars;

        public Staff()
        {

        }

        /// <summary>
        /// Returns the containing bars
        /// </summary>
        /// <returns>All the bars, including possible repeating parts, that are in this Staff object</returns>
        public List<Bar> GetBars()
        {
            List<Bar> returnBars = new List<Bar>();
            foreach (IStaffElement element in this.Bars)
            {
                if (element.GetType() == typeof(Staff))
                {
                    Staff staff = (Staff)element;
                    returnBars.AddRange(staff.GetBars());
                }
                else
                {
                    Bar bar = (Bar)element;
                    returnBars.Add(bar);
                }
            }
            return returnBars;
        }
    }
}