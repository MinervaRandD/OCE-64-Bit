

namespace TestDriverKeyboardTests
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Input;

    using Utilities;

    public partial class KeyboardTestForm : Form, IMessageFilter
    {
        private int lastChar = 0;
        private int repeatCount = 0;

        public KeyboardTestForm()
        {
            InitializeComponent();
        }


        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // install message filter when form activates
            Application.AddMessageFilter(this);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // remove message filter when form deactivates
            Application.RemoveMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            this.lblGetKeyPressed.Text = KeyboardUtils.GetKeyPressed().ToString();

            if (m.Msg == (int)WindowsMessage.WM_KEYDOWN)
            {
                int keyVal = m.WParam.ToInt32();
                long keyValL = m.LParam.ToInt64();

                this.lblKeyVal.Text = keyVal.ToString();

                if (keyVal == lastChar)
                {
                    repeatCount++;
                }

                else
                {
                    repeatCount = 0;
                }

                this.lblRepeatCount.Text = repeatCount.ToString();

                lastChar = keyVal;

                return true;

            }

            if (m.Msg == (int)WindowsMessage.WM_KEYUP)
            {
                int keyVal = m.WParam.ToInt32();
                long keyValL = m.LParam.ToInt64();

                this.lblKeyUp.Text = keyVal.ToString();
                return true;
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lblF1Pressed.Text = KeyboardUtils.F1KeyPressed.ToString();
        }
    }
}
