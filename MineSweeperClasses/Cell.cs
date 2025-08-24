using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperClasses
{
    
    public class Cell
    {
        
        public bool Live { get; set; }

        public int LiveNeighbors { get; set; }

        
        public bool HasReward { get; set; }

        
        public bool IsRevealed { get; private set; }

        
        public bool IsFlagged { get; private set; }

        
        public Cell()
        {
            Live = false;
            LiveNeighbors = 0;
            HasReward = false;
            IsRevealed = false;
            IsFlagged = false;
        }

        
        public bool Reveal()
        {
            if (IsRevealed)
            {
                return !Live;
            }
            else
            {
                IsRevealed = true;
                return !Live;
            }
        }


        
        public bool UndoReveal()
        {
            if (IsRevealed)
            {
                IsRevealed = false;
                return true;
            }
            return false;
        }


        
        public void ToggleFlag()
        {
            if (!IsRevealed)
            {
                IsFlagged = !IsFlagged;
            }
        }

        
        public bool CollectReward()
        {
            if (IsRevealed && HasReward)
            {
                HasReward = false;
                return true;
            }
            return false;
        }

        
        public char GetAnswerChar()
        {
            if (Live) return 'B';
            return LiveNeighbors == 0 ? '.' : (char)('0' + Math.Min(LiveNeighbors, 9));
        }

        
        public char GetVisibleChar()
        {
            if (!IsRevealed) return IsFlagged ? 'F' : '.';
            if (Live) return 'B';
            return LiveNeighbors == 0 ? ' ' : (char)('0' + Math.Min(LiveNeighbors, 9));
        }

        public override string ToString() => GetVisibleChar().ToString();
    }
}
