using Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapolavoroGestioneMagazzino
{
    struct Admin
    {
        public string Nome;
        public string Password;
    }
    public partial class AD : Form
    {
        private static Dictionary<string, Admin> Admin = new Dictionary<string, Admin>();
        public AD()
        {
            InitializeComponent();
        }

        private void AD_Load(object sender, EventArgs e)
        {
            CaricaAdmin();
            Label Titolo = new Label()
            {
                Text = "Zona Amministrazione",
                ForeColor = Color.Red,
                Font = new Font("Arial", 30, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50,
                Margin = new Padding(0, 10, 0, 10)
            };
            this.Controls.Add(Titolo);

            Label Aggiungi = new Label()
            {
                Text = "Aggiungi Persone",
                AutoSize = true,
                Location = new Point(10, 70),
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.Magenta,
            };
            this.Controls.Add(Aggiungi);

            Label Nome = new Label()
            {
                Text = "Nome:",
                AutoSize = true,
                Location = new Point(10, 120),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue,
            };
            this.Controls.Add(Nome);

            TextBox NomeTextBox = new TextBox()
            {
                Location = new Point(10, 140),
                Width = 200,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Height = 30,

            };
            this.Controls.Add(NomeTextBox);

            Label Password = new Label()
            {
                Text = "Password:",
                AutoSize = true,
                Location = new Point(250, 120),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue,
            };
            this.Controls.Add(Password);

            TextBox PasswordTextBox = new TextBox()
            {
                Location = new Point(250, 140),
                Width = 200,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Height = 30,
                PasswordChar = '*'
            };

            this.Controls.Add(PasswordTextBox);

            Button Registra = new Button()
            {
                Location = new Point(10, 200),
                Text = "Registra",
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Green,
            };
            Registra.Click += (s, args) =>
            {
                string NomeKey = NomeTextBox.Text;
                Admin.Add(NomeKey, new Admin
                {
                    Nome = NomeTextBox.Text,
                    Password = PasswordTextBox.Text,
                });

                try
                {
                    using (StreamWriter writer = new StreamWriter("Access.txt"))
                    {
                        foreach (var item in Admin)
                        {
                            Admin Admin = item.Value;
                            writer.WriteLine($"{Admin.Nome},{Admin.Password}");
                        }
                    }
                    MessageBox.Show("Credenziali salvato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante il salvataggio: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                NomeTextBox.Clear();
                PasswordTextBox.Clear();
            };
            this.Controls.Add(Registra);

            Label UtenteEliminare = new Label()
            {
                Text = "Utente Da Eliminare:",
                AutoSize = true,
                Location = new Point(10, 250),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue,
            };
            this.Controls.Add(UtenteEliminare);

            TextBox TabEliminare = new TextBox()
            {
                Location = new Point(200, 250),
                Width = 200,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Height = 30,
            };
            this.Controls.Add(TabEliminare);

            Button Elimina = new Button()
            {
                Location = new Point(440, 250),
                Text = "Elimina",
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Red,
            };
            Elimina.Click += (s, args) =>
            {
                string NomeKey = TabEliminare.Text;
                if (Admin.ContainsKey(NomeKey))
                {
                    Admin.Remove(NomeKey);
                    using (StreamWriter writer = new StreamWriter("Access.txt"))
                    {
                        foreach (var item in Admin)
                        {
                            Admin Admin = item.Value;
                            writer.WriteLine($"{Admin.Nome},{Admin.Password}");
                        }
                    }
                    MessageBox.Show("Utente eliminato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Utente non trovato!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                TabEliminare.Clear();
            };
            this.Controls.Add(Elimina);

            Button MenuIniziale = new Button()
            {
                Location = new Point(10, 300),
                Text = "Menu",
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Gray,
            };
            MenuIniziale.Click += (s, args) =>
            {
                this.Hide();
                Form2 form2 = new Form2();
                form2.Show();
            };
            this.Controls.Add(MenuIniziale);
        }

        private void CaricaAdmin()
        {
            try
            {
                using (StreamReader reader = new StreamReader("Access.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            Admin[parts[0]] = new Admin()
                            {
                                Nome = parts[1],
                                Password = parts[2]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
