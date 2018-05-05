using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dievinity.Entities {
    public class Stats : IComparable<Stats> {

        protected int initiative;
        public int Initiative {
            get { return initiative; }
            set { initiative = value; }
        }

        protected int maxActionPoints;
        public int MaxActionPoints {
            get { return maxActionPoints; }
            set { maxActionPoints = value; }
        }

        protected int actionPoints;
        public int ActionPoints {
            get { return actionPoints; }
            set { actionPoints = value; }
        }

        public Stats() {
            this.initiative = 1;
            this.maxActionPoints = 4;
            this.actionPoints = 4;
        }

        public int CompareTo(Stats other) {
            return other.Initiative.CompareTo(Initiative);
        }
    }
}
