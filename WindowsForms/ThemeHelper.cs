using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    internal static class ThemeHelper
    {
        public static readonly Color OuterBrown = Color.FromArgb(142, 110, 83);
        public static readonly Color OuterBrownDark = Color.FromArgb(117, 88, 64);
        public static readonly Color Bone = Color.FromArgb(246, 243, 235);
        public static readonly Color BoneBorder = Color.FromArgb(213, 207, 192);
        public static readonly Color MenuBone = Color.FromArgb(235, 230, 218);
        public static readonly Color Parchment = Color.FromArgb(239, 228, 200);
        public static readonly Color ParchmentHeader = Color.FromArgb(228, 212, 176);
        public static readonly Color ParchmentBorder = Color.FromArgb(201, 180, 137);
        public static readonly Color GridSurface = Color.FromArgb(250, 247, 240);
        public static readonly Color TextDark = Color.FromArgb(43, 38, 32);
        public static readonly Color TextBody = Color.FromArgb(61, 49, 36);
        public static readonly Color TextMuted = Color.FromArgb(107, 91, 71);
        public static readonly Color GoldBack = Color.FromArgb(212, 175, 55);
        public static readonly Color GoldBackHover = Color.FromArgb(228, 193, 75);
        public static readonly Color GoldBackBorder = Color.FromArgb(158, 123, 27);

        public static void StyleBackButton(Button btn)
        {
            btn.Text = "<- Volver";
            btn.BackColor = GoldBack;
            btn.ForeColor = Color.Black;
            btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size, FontStyle.Bold);
            btn.UseVisualStyleBackColor = false;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = GoldBackBorder;
            btn.FlatAppearance.MouseOverBackColor = GoldBackHover;
        }

        public static DialogResult ShowSingleWindow(Control current, Form next)
        {
            var hostForm = current.FindForm() ?? current as Form;
            next.StartPosition = FormStartPosition.CenterScreen;
            if (hostForm != null && !hostForm.IsDisposed)
            {
                hostForm.Hide();
            }
            try
            {
                return next.ShowDialog();
            }
            finally
            {
                if (hostForm != null && !hostForm.IsDisposed)
                {
                    hostForm.Show();
                    hostForm.Activate();
                }
            }
        }

        public static void ApplyTheme(Form form)
        {
            form.BackColor = Bone;
            form.ForeColor = TextDark;

            form.Paint -= Form_PaintOuterFrame;
            form.Paint += Form_PaintOuterFrame;
            form.Resize -= Form_ResizeInvalidate;
            form.Resize += Form_ResizeInvalidate;

            ApplyToControls(form.Controls);
        }

        private static void Form_ResizeInvalidate(object? sender, EventArgs e)
        {
            if (sender is Form form)
            {
                form.Invalidate();
            }
        }

        private static void Form_PaintOuterFrame(object? sender, PaintEventArgs e)
        {
            if (sender is not Form form || form.ClientSize.Width <= 0 || form.ClientSize.Height <= 0)
                return;

            using var pen = new Pen(OuterBrown, 6);
            e.Graphics.DrawRectangle(pen, 0, 0, form.ClientSize.Width - 1, form.ClientSize.Height - 1);
        }

        public static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case MenuStrip menuStrip:
                        menuStrip.BackColor = MenuBone;
                        menuStrip.ForeColor = TextDark;
                        foreach (ToolStripItem item in menuStrip.Items)
                        {
                            if (item.ForeColor == Color.Empty || item.ForeColor == SystemColors.ControlText)
                                item.ForeColor = TextDark;
                        }
                        break;

                    case StatusStrip statusStrip:
                        statusStrip.BackColor = OuterBrown;
                        statusStrip.ForeColor = Bone;
                        foreach (ToolStripItem item in statusStrip.Items)
                        {
                            item.ForeColor = Bone;
                        }
                        break;

                    case DataGridView dgv:
                        dgv.BackgroundColor = GridSurface;
                        dgv.GridColor = ParchmentBorder;
                        dgv.EnableHeadersVisualStyles = false;
                        dgv.DefaultCellStyle.BackColor = GridSurface;
                        dgv.DefaultCellStyle.ForeColor = TextDark;
                        dgv.DefaultCellStyle.SelectionBackColor = OuterBrown;
                        dgv.DefaultCellStyle.SelectionForeColor = Bone;
                        dgv.ColumnHeadersDefaultCellStyle.BackColor = ParchmentHeader;
                        dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextDark;
                        dgv.RowHeadersDefaultCellStyle.BackColor = Parchment;
                        dgv.RowHeadersDefaultCellStyle.ForeColor = TextMuted;
                        break;

                    case GroupBox groupBox:
                        groupBox.BackColor = Parchment;
                        groupBox.ForeColor = TextDark;
                        break;

                    case Panel panel:
                        if (IsNeutralBackground(panel.BackColor))
                        {
                            panel.BackColor = Parchment;
                        }
                        break;

                    case TabControl tabControl:
                        foreach (TabPage page in tabControl.TabPages)
                        {
                            page.BackColor = Bone;
                            page.ForeColor = TextDark;
                        }
                        break;

                    case ComboBox comboBox:
                        if (comboBox.DropDownStyle == ComboBoxStyle.DropDownList)
                        {
                            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                        }
                        comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBox.Leave -= ComboBox_ValidateMatchOnLeave;
                        comboBox.Leave += ComboBox_ValidateMatchOnLeave;
                        break;

                    case Button button:
                        if (IsNeutralBackground(button.BackColor))
                        {
                            button.BackColor = Parchment;
                            button.ForeColor = TextDark;
                            button.UseVisualStyleBackColor = false;
                            button.FlatStyle = FlatStyle.Flat;
                            button.FlatAppearance.BorderColor = ParchmentBorder;
                            button.FlatAppearance.MouseOverBackColor = ParchmentHeader;
                        }
                        break;
                }

                if (control.HasChildren)
                {
                    ApplyToControls(control.Controls);
                }
            }
        }

        private static void ComboBox_ValidateMatchOnLeave(object? sender, EventArgs e)
        {
            if (sender is not ComboBox comboBox || comboBox.Items.Count == 0)
                return;

            string text = comboBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(text))
            {
                if (comboBox.SelectedIndex >= 0)
                    comboBox.Text = comboBox.GetItemText(comboBox.SelectedItem);
                return;
            }

            int exactIdx = comboBox.FindStringExact(text);
            if (exactIdx >= 0)
            {
                comboBox.SelectedIndex = exactIdx;
                return;
            }

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                string itemText = comboBox.GetItemText(comboBox.Items[i]) ?? string.Empty;
                if (itemText.Contains(text, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }

            if (comboBox.SelectedIndex >= 0)
            {
                comboBox.Text = comboBox.GetItemText(comboBox.SelectedItem);
            }
        }

        private static bool IsNeutralBackground(Color color)
        {
            if (color == Bone || color == GridSurface || color == MenuBone || color == Parchment || color == ParchmentHeader)
                return false;

            if (color == Color.Empty || color == Color.Transparent)
                return true;

            if (color == Color.White || color == SystemColors.Control || color == SystemColors.Window)
                return true;

            return color.R >= 235 && color.G >= 235 && color.B >= 235 &&
                   Math.Abs(color.R - color.G) <= 10 && Math.Abs(color.G - color.B) <= 10;
        }
    }
}
