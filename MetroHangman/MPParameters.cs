namespace MetroHangman
{
    public class MpParameters
    {
        public bool HideWord { get; }

        public bool RevealWord { get; }

        public MpParameters(bool hideWord, bool revealWord)
        {
            HideWord = hideWord;
            RevealWord = revealWord;
        }
    }
}
