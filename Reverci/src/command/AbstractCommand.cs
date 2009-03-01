namespace Reverci.command
{
    public abstract class AbstractCommand : ICommand
    {
        private event StatusChangedDelegate StatusChangedImpl;

        private readonly IGameCommander r_Commander;
        private bool m_Checked;
        private bool m_Enabled;

        protected AbstractCommand(IGameCommander i_Commander)
        {
            r_Commander = i_Commander;
            Checked = false;
            Enabled = true;
            Visible = true;
        }

        protected IGameCommander getCommander()
        {
            return r_Commander;
        }

        public abstract void DoCommand();

        public bool Checked
        {
            get { return m_Checked; }
            set
            {
                m_Checked = value;
                raiseStatusChanged();
            }
        }

        private void raiseStatusChanged()
        {
            if (StatusChangedImpl != null)
            {
                StatusChangedImpl(this);
            }
        }

        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                m_Enabled = value;
                raiseStatusChanged();
            }
        }

        public string Text { get; set; }

        public bool Visible { get; set; }

        public event StatusChangedDelegate StatusChanged
        {
            add { StatusChangedImpl += value; }
            remove { StatusChangedImpl -= value; }
        }
    }
}