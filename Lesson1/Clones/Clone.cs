namespace Clones
{
    public class Clone
    {
        private StackItem<string> learnedPrograms;
        private StackItem<string> undoHistory;

        public Clone() { }

        public Clone(Clone other)
        {
            learnedPrograms = other.learnedPrograms;
            undoHistory = other.undoHistory;
        }

        public void Learn(string program)
        {
            undoHistory = null;
            learnedPrograms = new StackItem<string>(program, learnedPrograms);
        }

        public void Rollback()
        {
            undoHistory = new StackItem<string>(learnedPrograms.Value, undoHistory);
            learnedPrograms = learnedPrograms.Previous;
        }

        public void Relearn()
        {
            learnedPrograms = new StackItem<string>(undoHistory.Value, learnedPrograms);
            undoHistory = undoHistory.Previous;
        }

        public string Check() => 
            learnedPrograms == null ? "basic" : learnedPrograms.Value;
    }
}
