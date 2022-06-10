using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTwinSampleApp.Models
    {
    public class iTwin
        {
        #region Constructors
        public iTwin()
            {
            DisplayName = $"iTwin Sample Name {Guid.NewGuid()}";
            Number = $"iTwin Sample Number {Guid.NewGuid()}";
            Class = "Endeavor";
            SubClass = "Project";
            Type = "Construction Project";
            DataCenterLocation = "East US";
            Status = "Active";
            }
        #endregion

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Number { get; set; }
        public string Class { get; set; }
        public string SubClass { get; set; }
        public string Type { get; set; }
        public string CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string DataCenterLocation { get; set; }
        public string Status { get; set; }
        }
    }
