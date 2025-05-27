using CapolavoroGestioneMagazzino;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Database
{
    struct Prodotto
    {
        public string Nome;
        public int Quantità;
        public double Prezzo;
        public DateTime data { get; set; } // Aggiunta della proprietà Data
    }

    public partial class Form1 : Form
    {
        private static Dictionary<string, Prodotto> inventario = new Dictionary<string, Prodotto>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CaricaInventario();
            TextBox Inventario = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(480, 70),
                Width = 350,
                Height = 300,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true
            };
            this.Controls.Add(Inventario);

            Label Nome = new Label
            {
                Text = "Nome: ",
                AutoSize = true,
                Location = new Point(10, 70),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue
            };
            this.Controls.Add(Nome);

            TextBox TabNome = new TextBox
            {
                Location = new Point(100, 70),
                Width = 150,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Height = 25
            };
            this.Controls.Add(TabNome);

            Label Quantità = new Label
            {
                Text = "Quantità: ",
                AutoSize = true,
                Location = new Point(10, 110),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue
            };
            this.Controls.Add(Quantità);

            TextBox TabQuantità = new TextBox
            {
                Location = new Point(100, 110),
                Width = 150,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Height = 25
            };
            this.Controls.Add(TabQuantità);

            Label Prezzo = new Label
            {
                Text = "Prezzo: ",
                AutoSize = true,
                Location = new Point(10, 150),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue
            };
            this.Controls.Add(Prezzo);

            TextBox TabPrezzo = new TextBox
            {
                Location = new Point(100, 150),
                Width = 150,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Height = 25
            };
            this.Controls.Add(TabPrezzo);

            Button btnAggiungi = new Button
            {
                Text = "Aggiungi",
                Location = new Point(10, 200),
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Green
            };

            btnAggiungi.Click += (s, args) =>
            {
                if (int.TryParse(TabQuantità.Text, out int quantita) && double.TryParse(TabPrezzo.Text, out double prezzo))
                {
                    string nome = TabNome.Text;


                    if (!string.IsNullOrWhiteSpace(nome))
                    {
                        if (!inventario.ContainsKey(nome))
                        {
                            inventario.Add(nome, new Prodotto
                            {
                                Nome = nome,
                                Quantità = quantita,
                                Prezzo = prezzo,
                                data = DateTime.Now
                            });
                            MessageBox.Show("Prodotto aggiunto con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Il prodotto esiste già nell'inventario!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Il nome del prodotto non può essere vuoto!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Inserisci valori validi per quantità e prezzo.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                TabNome.Clear();
                TabQuantità.Clear();
                TabPrezzo.Clear();
            };
            this.Controls.Add(btnAggiungi);

            Button btnSalva = new Button
            {
                Text = "Salva",
                Location = new Point(120, 200),
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Blue
            };

            btnSalva.Click += (s, args) =>
            {
                try
                {
                    if (int.TryParse(TabQuantità.Text, out int quantita) && double.TryParse(TabPrezzo.Text, out double prezzo))
                    {
                        inventario.Add("Inventario", new Prodotto
                        {
                            Nome = TabNome.Text,
                            Quantità = quantita,
                            Prezzo = prezzo,
                            data = DateTime.Now
                        });
                    }

                    try
                    {
                        using (StreamWriter writer = new StreamWriter("inventario.txt"))
                        {
                            foreach (var item in inventario)
                            {
                                Prodotto prodotto = item.Value;
                                writer.WriteLine($"{prodotto.Nome},{prodotto.Quantità},{prodotto.Prezzo}");
                            }
                        }
                        MessageBox.Show("Inventario salvato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Errore durante il salvataggio: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante il salvataggio: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            this.Controls.Add(btnSalva);

            Button btnVisualizza = new Button
            {
                Text = "Visualizza",
                Location = new Point(230, 200),
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Orange
            };

            btnVisualizza.Click += (s, args) =>
            {
                Inventario.Clear();

                if (inventario.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var item in inventario)
                    {
                        Prodotto prodotto = item.Value;
                        sb.AppendLine($"Nome: {prodotto.Nome}, Quantità: {prodotto.Quantità}, Prezzo: {prodotto.Prezzo:C}, Data d'aggiunta {prodotto.data}");
                        sb.AppendLine("--------------------------------------------------");
                    }

                    Inventario.Text = sb.ToString();
                }
                else
                {
                    Inventario.Text = "L'inventario è vuoto.";
                }
            };

            this.Controls.Add(btnVisualizza);

            Button btnReset = new Button
            {
                Text = "Reset",
                Location = new Point(340, 200),
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Red
            };
            btnReset.Click += (s, args) =>
            {
                inventario.Clear();
                File.WriteAllText("inventario.txt", string.Empty);
                MessageBox.Show("Inventario eliminato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            this.Controls.Add(btnReset);

            Button btnMenu = new Button
            {
                Text = "Menu",
                Location = new Point(10, 250),
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Gray
            };
            btnMenu.Click += (s, args) =>
            {
                this.Hide();
                Form2 form2 = new Form2();
                form2.Show();
            };
            this.Controls.Add(btnMenu);

            Label CercaRimuovi = new Label
            {
                Text = "Prodotto: ",
                AutoSize = true,
                Location = new Point(10, 300),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue
            };
            this.Controls.Add(CercaRimuovi);

            TextBox TabCerca = new TextBox
            {
                Location = new Point(100, 300),
                Width = 150,
                Font = new Font("Arial", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Height = 25
            };
            this.Controls.Add(TabCerca);

            Button btnCerca = new Button
            {
                Text = "Cerca",
                Location = new Point(260, 300),
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Purple
            };
            btnCerca.Click += (s, args) =>
            {
                if (inventario.ContainsKey(TabCerca.Text))
                {
                    Prodotto prodotto = inventario[TabCerca.Text];
                    MessageBox.Show($"Prodotto trovato: Nome: {prodotto.Nome}, Quantità: {prodotto.Quantità}, Prezzo: {prodotto.Prezzo:C}, Data d'aggiunta {prodotto.data}", "Prodotto Trovato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Prodotto non trovato nell'inventario.", "Prodotto Non Trovato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            this.Controls.Add(btnCerca);

            Button btnRimuovi = new Button
            {
                Text = "Rimuovi",
                Location = new Point(260, 330),
                Width = 100,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Red
            };
            this.Controls.Add(btnRimuovi);
            btnRimuovi.Click += (s, args) =>
            {
                if (inventario.ContainsKey(TabCerca.Text))
                {
                    inventario.Remove(TabCerca.Text);
                    using (StreamWriter writer = new StreamWriter("inventario.txt"))
                    {
                        foreach (var item in inventario)
                        {
                            Prodotto prodotto = item.Value;
                            writer.WriteLine($"{prodotto.Nome},{prodotto.Quantità},{prodotto.Prezzo},{prodotto.data}");
                        }
                    }
                    MessageBox.Show("Prodotto rimosso con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Prodotto non trovato nell'inventario.", "Prodotto Non Trovato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
        }
        private void CaricaInventario()
        {
            if (File.Exists("inventario.txt"))
            {
                try
                {
                    using (StreamReader reader = new StreamReader("inventario.txt"))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 3 &&
                                int.TryParse(parts[1], out int quantita) &&
                                double.TryParse(parts[2], out double prezzo))
                            {
                                inventario[parts[0]] = new Prodotto
                                {
                                    Nome = parts[0],
                                    Quantità = quantita,
                                    Prezzo = prezzo,
                                    data = DateTime.Now // Imposta la data corrente, puoi modificarla se necessario
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante il caricamento dell'inventario: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
