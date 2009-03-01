namespace Reverci.view
{
    internal interface IButtonView : IComponentView
    {
        void SetChecked(bool i_Checked);

        void SetEnabled(bool i_Enabled);
    }
}