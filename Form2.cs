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
    struct Access
    {
        public string Name;
        public string Password;

        public object Nome { get; internal set; }
    }
    public partial class Form2 : Form
    {
        private static Dictionary<string, Access> Access = new Dictionary<string, Access>();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            

            CaricaAccess();
            Label Titolo = new Label()
            {
                Text = "Gestione Inventario Magazzino",
                ForeColor = Color.Red,
                Font = new Font("Arial", 30, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50,
                Margin = new Padding(0, 10, 0, 10)
            };
            this.Controls.Add(Titolo);

            Label Nome = new Label()
            {
                Text = "Nome:",
                AutoSize = true,
                Location = new Point(10, 70),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue,
            };
            this.Controls.Add(Nome);

            TextBox NomeTextBox = new TextBox()
            {
                Location = new Point(10, 100),
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
                Location = new Point(250, 70),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue,
            };
            this.Controls.Add(Password);

            TextBox PasswordTextBox = new TextBox()
            {
                Location = new Point(250, 100),
                Width = 200,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Height = 30,
                PasswordChar = '*'
            };

            this.Controls.Add(PasswordTextBox);

            Button LoginButton = new Button()
            {
                Text = "Login",
                Location = new Point(10, 150),
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Green
            };
            LoginButton.Click += (s, args) =>
            {
                if (NomeTextBox.Text == "Admin" && PasswordTextBox.Text == "AdminCapolavoroMagazzino2025")
                {
                    this.Hide();
                    AD AD = new AD();
                    AD.Show();
                }
                else if (string.IsNullOrEmpty(NomeTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text))
                {
                    MessageBox.Show("Compila tutti i campi!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (Access.ContainsKey(NomeTextBox.Text) && Access[NomeTextBox.Text].Password == PasswordTextBox.Text)
                {
                    MessageBox.Show("Login avvenuto con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.Show();
                }
                else
                {
                    MessageBox.Show("Nome utente o password errati!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                NomeTextBox.Clear();
                PasswordTextBox.Clear();
            };
            this.Controls.Add(LoginButton);

            Button ExitButton = new Button()
            {
                Text = "Esci",
                Location = new Point(120, 150),
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Gray,
            };
            ExitButton.Click += (s, args) =>
            {
                Application.Exit();
            };
            this.Controls.Add(ExitButton);
        }

        private void CaricaAccess()
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
                            Access[parts[0]] = new Access()
                            {
                                Name = parts[0],
                                Password = parts[1]
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
