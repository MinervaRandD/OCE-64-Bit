
namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.Finish_Controls;
    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PaletteLib;
    using SettingsLib;
    using Utilities;

    public partial class FloorMaterialEstimatorBaseForm
    {
        public AreaFinishesEditForm FormAreaFinishes;

        private void btnEditAreas_Click(object sender, EventArgs e)
        {
            if (GlobalSettings.ShowAreaEditFormAsModal)
            {
                this.btnEditAreas.Checked = true;

                FormAreaFinishes = new AreaFinishesEditForm(this, areaPalette, AreaFinishBaseList, SeamFinishBaseList, GlobalSettings.ShowAreaEditFormAsModal);
                FormAreaFinishes.ShowDialog(this);

                this.btnEditAreas.Checked = false;

                FormAreaFinishes = null;
            }

            else
            {
                if (this.btnEditAreas.Checked)
                {
                    return;
                }

                if (FormAreaFinishes != null)
                {
                    FormAreaFinishes.WindowState = FormWindowState.Normal;
                }

                else
                {
                    FormAreaFinishes = new AreaFinishesEditForm(this, this.areaPalette, this.AreaFinishBaseList, this.SeamFinishBaseList, GlobalSettings.ShowAreaEditFormAsModal);

                    FormAreaFinishes.Resize += FormAreaFinishes_Resize;
                    FormAreaFinishes.FormClosed += FormAreaFinishes_FormClosed;

                    FormAreaFinishes.Show(this);
                }

                FormAreaFinishes.BringToFront();

                this.btnEditAreas.Checked = true;
            }
        }
        private void FormAreaFinishes_Resize(object sender, EventArgs e)
        {
            if (FormAreaFinishes.WindowState == FormWindowState.Minimized)
            {
                this.btnEditAreas.Checked = false;
            }
        }

        private void FormAreaFinishes_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btnEditAreas.Checked = false;

            FormAreaFinishes = null;
        }

        public LineFinishesEditForm FormLineFinishes;

        private void btnEditLines_Click(object sender, EventArgs e)
        {
            if (GlobalSettings.ShowLineEditFormAsModal)
            {
                this.btnEditLines.Checked = true;

                FormLineFinishes = new LineFinishesEditForm(this, this.linePalette, this.LineFinishBaseList, this.ZeroLineBase, GlobalSettings.ShowLineEditFormAsModal);
                FormLineFinishes.ShowDialog();

                this.btnEditLines.Checked = false;

                this.FormLineFinishes = null;

            }

            else
            {
                if (this.btnEditLines.Checked)
                {
                    return;
                }

                if (FormLineFinishes != null)
                {
                    FormLineFinishes.WindowState = FormWindowState.Normal;
                }

                else
                {
                    FormLineFinishes = new LineFinishesEditForm(this, this.linePalette, this.LineFinishBaseList, this.ZeroLineBase, GlobalSettings.ShowLineEditFormAsModal);

                    FormLineFinishes.Resize += FormLineFinishes_Resize;
                    FormLineFinishes.FormClosed += FormLineFinishes_FormClosed;

                    FormLineFinishes.Show(this);
                }

                FormLineFinishes.BringToFront();

                this.btnEditLines.Checked = true;
            }
        }

        private void FormLineFinishes_Resize(object sender, EventArgs e)
        {
            if (FormLineFinishes.WindowState == FormWindowState.Minimized)
            {
                this.btnEditLines.Checked = false;
            }
        }

        private void FormLineFinishes_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btnEditLines.Checked = false;
            FormLineFinishes = null;
        }

        SeamFinishesEditForm FormSeamFinishes = null;

        private void btnEditSeams_Click(object sender, EventArgs e)
        {
            if (GlobalSettings.ShowSeamEditFormAsModal)
            {
                if (this.tbcPageAreaLine.SelectedIndex != FloorMaterialEstimatorBaseForm.tbcAreaModeIndex)
                {
                    ManagedMessageBox.Show("This form can only be shown when the areas tab is selected.");
                    return;
                }

                this.btnEditSeams.Checked = true;

                FormSeamFinishes = new SeamFinishesEditForm(this, GlobalSettings.ShowSeamEditFormAsModal);
                FormSeamFinishes.ShowDialog();

                this.btnEditSeams.Checked = false;
            }

            else
            {
                if (this.btnEditSeams.Checked)
                {
                    return;
                }

                if (FormSeamFinishes != null)
                {
                    FormSeamFinishes.WindowState = FormWindowState.Normal;
                }

                else
                {
                    FormSeamFinishes = new SeamFinishesEditForm(this, GlobalSettings.ShowSeamEditFormAsModal);

                    FormSeamFinishes.Resize += FormSeamFinishes_Resize;
                    FormSeamFinishes.FormClosed += FormSeamFinishes_FormClosed;

                    FormSeamFinishes.Show(this);
                }

                FormSeamFinishes.BringToFront();

                this.btnEditSeams.Checked = true;
            }
        }

        private void FormSeamFinishes_Resize(object sender, EventArgs e)
        {
            if (FormSeamFinishes.WindowState == FormWindowState.Minimized)
            {
                this.btnEditSeams.Checked = false;
            }
        }

        private void FormSeamFinishes_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btnEditSeams.Checked = false;
            FormSeamFinishes = null;
        }

        FieldGuideEditForm FormFieldGuides = null;

        private void btnEditFieldGuides_Click(object sender, EventArgs e)
        {
            if (GlobalSettings.ShowFieldGuideEditFormAsModal)
            {
                this.btnEditFieldGuides.Checked = true;

                FormFieldGuides = new FieldGuideEditForm(this, this.CanvasManager.FieldGuideController, GlobalSettings.ShowFieldGuideEditFormAsModal);
                FormFieldGuides.ShowDialog();

                this.btnEditFieldGuides.Checked = false;
            }

            else
            {
                if (this.btnEditFieldGuides.Checked)
                {
                    return;
                }

                if (FormFieldGuides != null)
                {
                    FormFieldGuides.WindowState = FormWindowState.Normal;
                }

                else
                {
                    FormFieldGuides = new FieldGuideEditForm(this, this.CanvasManager.FieldGuideController, GlobalSettings.ShowFieldGuideEditFormAsModal);

                    FormFieldGuides.Resize += FormFieldGuides_Resize;
                    FormFieldGuides.FormClosed += FormFieldGuides_FormClosed;

                    FormFieldGuides.Show(this);
                }

                FormFieldGuides.BringToFront();

                this.btnEditFieldGuides.Checked = true;
            }
        }

        private void FormFieldGuides_Resize(object sender, EventArgs e)
        {
            if (FormFieldGuides.WindowState == FormWindowState.Minimized)
            {
                this.btnEditFieldGuides.Checked = false;
            }
        }

        private void FormFieldGuides_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btnEditFieldGuides.Checked = false;
            FormFieldGuides = null;
        }
    }
}
