using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class MPParameters
    {
        private bool param_hide;
        private bool param_reveal;
        public bool HideWord
        {
            get
            {
                return param_hide;
            }
        }
        public bool RevealWord
        {
            get
            {
                return param_reveal;
            }
        }
        public MPParameters(bool hideWord, bool revealWord)
        {
            this.param_hide = hideWord;
            this.param_reveal = revealWord;
        }
    }
}
