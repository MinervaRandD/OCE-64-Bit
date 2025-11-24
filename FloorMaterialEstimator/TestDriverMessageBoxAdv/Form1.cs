namespace TestDriverMessageBoxAdv
{
    using Utilities;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLaunchMessageBox_Click(object sender, EventArgs e)
        {
            MessageBoxAdv.Show(
                "Test message is being displayed"
                , "Test Message Box"
                , MessageBoxAdv.Buttons.OKCancel
                , MessageBoxAdv.Icon.Error);
        }
    }
}
