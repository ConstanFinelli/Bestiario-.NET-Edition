using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsForms
{
    /// <summary>
    /// Provee los estilos visuales, paleta de colores y componentes estéticos
    /// basados en la versión original de Java (main.css, home.css, navbar.css, login.css).
    /// </summary>
    public static class AppTheme
    {
        // Paleta oficial Java / Bestiario
        public static readonly Color Primary = ColorTranslator.FromHtml("#8E6E53");         // Marrón cuero medieval
        public static readonly Color PrimaryDark = ColorTranslator.FromHtml("#6E533D");     // Marrón oscuro
        public static readonly Color Secondary = ColorTranslator.FromHtml("#F4F1EA");       // Pergamino crema / fondo
        public static readonly Color Surface = ColorTranslator.FromHtml("#FAF8F5");         // Fondo tarjetas / paneles
        public static readonly Color MutedGray = ColorTranslator.FromHtml("#C9C9CA");       // Gris neutro Java
        public static readonly Color TextColor = ColorTranslator.FromHtml("#2C3E50");       // Azul petróleo / grafito
        public static readonly Color TextMuted = ColorTranslator.FromHtml("#6C757D");       // Texto secundario
        public static readonly Color Gold = ColorTranslator.FromHtml("#C9960C");            // Botón primario dorado
        public static readonly Color GoldLight = ColorTranslator.FromHtml("#D9BD62");       // Dorado suave
        public static readonly Color Success = ColorTranslator.FromHtml("#4CAF50");         // Verde aceptar / aprobar
        public static readonly Color Danger = ColorTranslator.FromHtml("#E53935");          // Rojo rechazar / eliminar
        public static readonly Color BorderColor = ColorTranslator.FromHtml("#D2B48C");      // Borde pergamino / tan

        public static readonly Font TitleFont = new Font("Georgia", 13F, FontStyle.Bold);
        public static readonly Font SubtitleFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        public static readonly Font HeaderFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        public static readonly Font BodyFont = new Font("Segoe UI", 9F, FontStyle.Regular);
        public static readonly Font ButtonFont = new Font("Segoe UI", 9F, FontStyle.Bold);

        /// <summary>
        /// Aplica el tema medieval uniforme a cualquier formulario y a todos sus controles hijos.
        /// </summary>
        public static void ApplyFormTheme(Form form)
        {
            form.BackColor = Secondary;
            form.ForeColor = TextColor;
            form.Font = BodyFont;

            if (form.MainMenuStrip != null)
            {
                ApplyMenuStripTheme(form.MainMenuStrip);
            }

            ApplyControlStyles(form);
        }

        /// <summary>
        /// Aplica el estilo navbar medieval a la barra de menú superior.
        /// </summary>
        public static void ApplyMenuStripTheme(MenuStrip menuStrip)
        {
            menuStrip.BackColor = Primary;
            menuStrip.ForeColor = Color.White;
            menuStrip.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            menuStrip.Renderer = new MedievalToolStripRenderer();

            foreach (ToolStripItem item in menuStrip.Items)
            {
                item.ForeColor = Color.White;
                if (item is ToolStripMenuItem subItem)
                {
                    StyleSubItems(subItem);
                }
            }
        }

        private static void StyleSubItems(ToolStripMenuItem item)
        {
            foreach (ToolStripItem dropDownItem in item.DropDownItems)
            {
                dropDownItem.ForeColor = TextColor;
                dropDownItem.BackColor = Surface;
                if (dropDownItem is ToolStripMenuItem sub)
                {
                    StyleSubItems(sub);
                }
            }
        }

        /// <summary>
        /// Recorre recursivamente los controles y aplica los estilos según su tipo.
        /// </summary>
        public static void ApplyControlStyles(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DataGridView grid)
                {
                    ApplyDataGridViewTheme(grid);
                }
                else if (c is GroupBox gb)
                {
                    gb.ForeColor = Primary;
                    gb.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    ApplyControlStyles(gb);
                }
                else if (c is Panel p)
                {
                    // Si es un panel de filtro o contenedor, dar fondo de superficie
                    if (p.Name.Contains("filtro", StringComparison.OrdinalIgnoreCase))
                    {
                        p.BackColor = Surface;
                        p.BorderStyle = BorderStyle.FixedSingle;
                    }
                    ApplyControlStyles(p);
                }
                else if (c is Button btn)
                {
                    ApplyButtonTheme(btn);
                }
                else if (c is LinkLabel link)
                {
                    link.LinkColor = Primary;
                    link.ActiveLinkColor = Gold;
                    link.VisitedLinkColor = PrimaryDark;
                }
                else if (c is TextBox txt && !txt.ReadOnly)
                {
                    txt.BackColor = Color.White;
                    txt.ForeColor = TextColor;
                }
                else if (c is ComboBox cb)
                {
                    cb.BackColor = Color.White;
                    cb.ForeColor = TextColor;
                }
                else if (c is CheckedListBox clb)
                {
                    clb.BackColor = Color.White;
                    clb.ForeColor = TextColor;
                    clb.BorderStyle = BorderStyle.FixedSingle;
                }

                // Recursión para sub-paneles si hay controles anidados
                if (c.HasChildren && !(c is DataGridView))
                {
                    ApplyControlStyles(c);
                }
            }
        }

        /// <summary>
        /// Estiliza botones con estilos acordes a su acción:
        /// Primarios/Dorado (Agregar, Login), Éxito (Aprobar, Aceptar), Peligro (Eliminar), Marrón (Cancelar, Ver registros).
        /// </summary>
        public static void ApplyButtonTheme(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.Cursor = Cursors.Hand;
            btn.Font = ButtonFont;

            string name = btn.Name.ToLowerInvariant();
            string text = btn.Text.ToLowerInvariant();

            if (name.Contains("eliminar") || name.Contains("rechazar") || name.Contains("quitar") ||
                text.Contains("eliminar") || text.Contains("rechazar") || text.Contains("quitar"))
            {
                btn.BackColor = Danger;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#C62828");
            }
            else if (name.Contains("aprobar") || text.Contains("aprobar") ||
                     name.Contains("aceptar") || text.Contains("aceptar") ||
                     name.Contains("guardar") || text.Contains("guardar"))
            {
                btn.BackColor = Success;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#2E7D32");
            }
            else if (name.Contains("login") || name.Contains("agregar") || name.Contains("register") ||
                     text.Contains("iniciar") || text.Contains("agregar") || text.Contains("registrar") || text.Contains("nuevo"))
            {
                btn.BackColor = GoldLight;
                btn.ForeColor = TextColor;
                btn.FlatAppearance.BorderColor = Gold;
            }
            else if (name.Contains("cancelar") || name.Contains("cancel") || text.Contains("cancelar") || text.Contains("volver"))
            {
                btn.BackColor = Primary;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = PrimaryDark;
            }
            else if (name.Contains("actualizar") || name.Contains("editar") || text.Contains("actualizar") || text.Contains("editar"))
            {
                btn.BackColor = Gold;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = PrimaryDark;
            }
            else
            {
                btn.BackColor = Primary;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = PrimaryDark;
            }
        }

        /// <summary>
        /// Aplica el tema medieval a las grillas DataGridView:
        /// Encabezados de cuero marrón (#8E6E53), filas pergamino alternadas, y selección dorada.
        /// </summary>
        public static void ApplyDataGridViewTheme(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = Secondary;
            grid.GridColor = BorderColor;
            grid.BorderStyle = BorderStyle.FixedSingle;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersHeight = 32;

            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = TextColor;
            grid.DefaultCellStyle.SelectionBackColor = GoldLight;
            grid.DefaultCellStyle.SelectionForeColor = TextColor;
            grid.DefaultCellStyle.Font = BodyFont;

            grid.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F9F7F2");
            grid.AlternatingRowsDefaultCellStyle.ForeColor = TextColor;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = GoldLight;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = TextColor;

            grid.RowHeadersVisible = false;
        }

        /// <summary>
        /// Crea un banner de encabezado superior con el estilo de navbar / header de Java.
        /// </summary>
        public static Panel CreateHeaderBanner(string title, int width)
        {
            Panel header = new Panel
            {
                Name = "headerPanel",
                Dock = DockStyle.Top,
                Height = 44,
                BackColor = Primary,
                Width = width
            };

            Label titleLbl = new Label
            {
                Text = title,
                ForeColor = Color.White,
                Font = new Font("Georgia", 11F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            header.Controls.Add(titleLbl);
            return header;
        }

        private class MedievalToolStripRenderer : ToolStripProfessionalRenderer
        {
            public MedievalToolStripRenderer() : base(new MedievalColorTable()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.Item.Selected || e.Item.Pressed)
                {
                    using var brush = new SolidBrush(GoldLight);
                    e.Graphics.FillRectangle(brush, new Rectangle(Point.Empty, e.Item.Size));
                    e.Item.ForeColor = TextColor;
                }
                else
                {
                    using var brush = new SolidBrush(Primary);
                    e.Graphics.FillRectangle(brush, new Rectangle(Point.Empty, e.Item.Size));
                    e.Item.ForeColor = Color.White;
                }
            }
        }

        private class MedievalColorTable : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Primary;
            public override Color MenuStripGradientEnd => Primary;
            public override Color MenuItemSelected => GoldLight;
            public override Color MenuItemSelectedGradientBegin => GoldLight;
            public override Color MenuItemSelectedGradientEnd => GoldLight;
            public override Color MenuItemPressedGradientBegin => Gold;
            public override Color MenuItemPressedGradientEnd => Gold;
            public override Color ToolStripDropDownBackground => Surface;
            public override Color ImageMarginGradientBegin => Surface;
            public override Color ImageMarginGradientMiddle => Surface;
            public override Color ImageMarginGradientEnd => Surface;
        }
    }
}
