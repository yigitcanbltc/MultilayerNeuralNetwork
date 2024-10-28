using System.Data;
using System.Numerics;

namespace MultiLayerNeuralNetwork
{
    public partial class Form1 : Form
    {
        private DataTable dataTable;
        private double[,] weightsInputHidden;  // Giriþten gizli katmana aðýrlýklar
        private double[,] weightsHiddenOutput; // Gizli katmandan çýkýþ katmanýna aðýrlýklar
        private double[] hiddenLayer;          // Gizli katmandaki nöronlar
        private double[] outputLayer;          // Çýkýþ katmandaki nöronlar
        private bool networkInitialized = false;

        private double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));
        private double SigmoidDerivative(double x) => x * (1.0 - x);

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Sabit boyut
            this.MaximizeBox = false; // Maksimuma alma düðmesini devre dýþý býrak
            this.MinimizeBox = true;  // Ýsterseniz minimize özelliði aktif kalabilir
            this.MinimumSize = this.Size; // Minimum boyutu þu anki boyuta ayarla
            this.MaximumSize = this.Size; // Maksimum boyutu þu anki boyuta ayarla
            this.StartPosition = FormStartPosition.CenterScreen;
            label6.TextAlign = ContentAlignment.MiddleCenter;

            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
        }

        private void InitializeDataTable(int inputNeurons, int outputNeurons)
        {
            // DataTable oluþtur
            dataTable = new DataTable();

            // Ýlk önce giriþim (X) sütunlarýný ekle
            for (int j = 0; j < inputNeurons; j++)
            {
                dataTable.Columns.Add("X" + j);
            }

            // Ardýndan beklenen (Beklenen) sütunlarýný ekle
            for (int k = 0; k < outputNeurons; k++)
            {
                dataTable.Columns.Add("Beklenen" + k);
            }
        }

        // Geri yayýlým algoritmasýný uygulayan fonksiyon
        private void TrainNetwork(int epochs, double learningRate)
        {
            int inputNeurons = int.Parse(txtInputNeurons.Text);
            int outputNeurons = int.Parse(txtOutputNeurons.Text);
            int hiddenNeurons = 3; // Gizli katman nöron sayýsý
            double totalError = 0;

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                totalError = 0;

                foreach (DataRow row in dataTable.Rows)
                {
                    // Giriþleri ve beklenen çýktýlarý tablo verisinden al
                    double[] inputs = new double[inputNeurons];
                    double[] expectedOutputs = new double[outputNeurons];

                    for (int i = 0; i < inputNeurons; i++)
                        inputs[i] = Convert.ToDouble(row[i]);

                    for (int i = 0; i < outputNeurons; i++)
                        expectedOutputs[i] = Convert.ToDouble(row[inputNeurons + i]);

                    // Ýleri yayýlým
                    for (int i = 0; i < hiddenNeurons; i++)
                    {
                        hiddenLayer[i] = 0; // Bu satýrda null hatasý alýnmamasý için diziyi baþlattýk
                        for (int j = 0; j < inputNeurons; j++)
                            hiddenLayer[i] += inputs[j] * weightsInputHidden[j, i];
                        hiddenLayer[i] = Sigmoid(hiddenLayer[i]);
                    }

                    for (int i = 0; i < outputNeurons; i++)
                    {
                        outputLayer[i] = 0;
                        for (int j = 0; j < hiddenNeurons; j++)
                            outputLayer[i] += hiddenLayer[j] * weightsHiddenOutput[j, i];
                        outputLayer[i] = Sigmoid(outputLayer[i]);
                    }

                    // Hata hesaplama ve toplam hatayý güncelleme
                    double[] outputErrors = new double[outputNeurons];
                    for (int i = 0; i < outputNeurons; i++)
                    {
                        outputErrors[i] = expectedOutputs[i] - outputLayer[i];
                        totalError += Math.Pow(outputErrors[i], 2);
                    }

                    // Geri yayýlým (backpropagation)
                    double[] hiddenErrors = new double[hiddenNeurons];
                    for (int i = 0; i < hiddenNeurons; i++)
                    {
                        hiddenErrors[i] = 0;
                        for (int j = 0; j < outputNeurons; j++)
                        {
                            hiddenErrors[i] += outputErrors[j] * weightsHiddenOutput[i, j];
                            weightsHiddenOutput[i, j] += learningRate * outputErrors[j] * SigmoidDerivative(outputLayer[j]) * hiddenLayer[i];
                        }
                    }

                    for (int i = 0; i < inputNeurons; i++)
                    {
                        for (int j = 0; j < hiddenNeurons; j++)
                        {
                            weightsInputHidden[i, j] += learningRate * hiddenErrors[j] * SigmoidDerivative(hiddenLayer[j]) * inputs[i];
                        }
                    }
                }

                // Her epoch sonrasý toplam hatayý ekrana yazdýr
                label6.Text = $"Epoch {epoch + 1}/{epochs}";  // label6'ya epoch sayýsý yazdýrýlýyor
                label7.Text = $"Toplam Hata: {totalError:F4}";   // Virgülden sonra 4 basamak yazdýrýlýyor


                Application.DoEvents(); // Arayüzün güncellenmesini saðlar
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
            }
            networkInitialized = true;
            // Kullanýcýdan deðerleri al
            int inputNeurons = int.Parse(txtInputNeurons.Text);
            int hiddenLayers = int.Parse(txtHiddenLayers.Text);
            int outputNeurons = int.Parse(txtOutputNeurons.Text);
            double learningRate = double.Parse(txtLearningRate.Text);

            // PictureBox boyutlarýný yeniden ayarla
            int neuronRadius = 20;
            int layerSpacing = 150;
            int neuronSpacing = 50;

            // Giriþ ve çýkýþ katmanlarý arasýndaki toplam geniþliði hesapla
            int totalWidth = (hiddenLayers + 2) * layerSpacing;

            // En fazla nöron sayýsýna göre yükseklik hesapla (giriþ, gizli veya çýkýþ)
            int maxNeurons = Math.Max(inputNeurons, Math.Max(3, outputNeurons));
            int totalHeight = (maxNeurons + 1) * neuronSpacing;

            // PictureBox boyutlarýný hesapla ve ayarla
            pictureBox1.Width = totalWidth + neuronRadius * 2;
            pictureBox1.Height = totalHeight + neuronRadius * 2;

            // Sinir aðýný çizmek için PictureBox'ý yeniden çizdir
            pictureBox1.Invalidate();
            InitializeDataTable(inputNeurons, outputNeurons);
            dataGridView1.DataSource = dataTable;

            // ---- YAPAY SÝNÝR AÐI OLUÞTURMA VE EÐÝTÝMÝ ----

            // Sinir aðýný oluþturma
            int hiddenNeurons = 3; // Gizli katman nöron sayýsý sabit olarak belirlenmiþtir.
            weightsInputHidden = new double[inputNeurons, hiddenNeurons];
            weightsHiddenOutput = new double[hiddenNeurons, outputNeurons];
            hiddenLayer = new double[hiddenNeurons];
            outputLayer = new double[outputNeurons];

            // Rastgele aðýrlýklarý baþlat
            Random rnd = new Random();
            for (int i = 0; i < inputNeurons; i++)
            {
                for (int j = 0; j < hiddenNeurons; j++)
                {
                    weightsInputHidden[i, j] = rnd.NextDouble();
                }
            }

            for (int i = 0; i < hiddenNeurons; i++)
            {
                for (int j = 0; j < outputNeurons; j++)
                {
                    weightsHiddenOutput[i, j] = rnd.NextDouble();
                }
            }
        }

        // Aðýn eðitilmesini saðlayan button2_Click fonksiyonu
        private void button2_Click(object sender, EventArgs e)
        {
            int epochs = 100;
            double learningRate = double.Parse(txtLearningRate.Text); // Öðrenme oraný
            TrainNetwork(epochs, learningRate);            // Aðý eðit
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!networkInitialized)
                return; // Eðer að baþlatýlmadýysa, çizim yapma ve fonksiyondan çýk.

            // Graphics nesnesini al ve sinir aðýný çiz
            Graphics g = e.Graphics;

            int inputNeurons = 0;
            int hiddenLayers = 0;
            int outputNeurons = 0;

            if (!int.TryParse(txtInputNeurons.Text, out inputNeurons))
            {
                MessageBox.Show("Geçerli bir giriþ nöron sayýsý girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtHiddenLayers.Text, out hiddenLayers))
            {
                MessageBox.Show("Geçerli bir gizli katman sayýsý girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtOutputNeurons.Text, out outputNeurons))
            {
                MessageBox.Show("Geçerli bir çýkýþ nöron sayýsý girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DrawNeuralNetwork(g, inputNeurons, hiddenLayers, outputNeurons);
        }

        private void DrawNeuralNetwork(Graphics g, int inputNeurons, int hiddenLayers, int outputNeurons)
        {
            int neuronRadius = 20;
            int layerSpacing = 150;
            int neuronSpacing = 50;

            int inputX = 50;
            int outputX = inputX + (hiddenLayers + 1) * layerSpacing;

            int inputY = 50;
            int outputY = 50;

            // Giriþ nöronlarýný çiz
            for (int i = 0; i < inputNeurons; i++)
            {
                g.FillEllipse(Brushes.Green, inputX, inputY + i * neuronSpacing, neuronRadius, neuronRadius);
            }

            // Eðer gizli katman yoksa (hiddenLayers == 0), doðrudan giriþten çýkýþa baðla
            if (hiddenLayers == 0)
            {
                for (int i = 0; i < inputNeurons; i++)
                {
                    for (int j = 0; j < outputNeurons; j++)
                    {
                        g.DrawLine(Pens.Black,
                            inputX + neuronRadius / 2,
                            inputY + i * neuronSpacing + neuronRadius / 2,
                            outputX + neuronRadius / 2,
                            outputY + j * neuronSpacing + neuronRadius / 2);
                    }
                }
            }
            else
            {
                // Gizli katmanlar varsa bunlarý çiz
                int hiddenX = inputX + layerSpacing;
                for (int layer = 0; layer < hiddenLayers; layer++)
                {
                    for (int i = 0; i < 3; i++)  // Gizli katman nöron sayýsýný 3 olarak sabit tutuyoruz
                    {
                        int hiddenY = inputY + i * neuronSpacing;
                        g.FillEllipse(Brushes.Blue, hiddenX, hiddenY, neuronRadius, neuronRadius);
                    }
                    hiddenX += layerSpacing; // Her katmaný bir sonraki X konumuna kaydýr
                }

                // Çýkýþ nöronlarýný çiz
                for (int i = 0; i < outputNeurons; i++)
                {
                    g.FillEllipse(Brushes.Red, outputX, outputY + i * neuronSpacing, neuronRadius, neuronRadius);
                }

                // Giriþ nöronlarý ile ilk gizli katman nöronlarý arasýndaki baðlantýlarý çiz
                hiddenX = inputX + layerSpacing;
                for (int i = 0; i < inputNeurons; i++)
                {
                    for (int j = 0; j < 3; j++) // Gizli katman nöron sayýsý 3
                    {
                        g.DrawLine(Pens.Black,
                            inputX + neuronRadius / 2,
                            inputY + i * neuronSpacing + neuronRadius / 2,
                            hiddenX + neuronRadius / 2,
                            inputY + j * neuronSpacing + neuronRadius / 2);
                    }
                }

                // Gizli katmanlar arasýndaki baðlantýlarý çiz
                for (int layer = 0; layer < hiddenLayers - 1; layer++)
                {
                    int currentLayerX = inputX + (layer + 1) * layerSpacing;
                    int nextLayerX = inputX + (layer + 2) * layerSpacing;

                    for (int i = 0; i < 3; i++)  // Þu anki gizli katman nöron sayýsý 3
                    {
                        for (int j = 0; j < 3; j++)  // Bir sonraki gizli katman nöron sayýsý 3
                        {
                            g.DrawLine(Pens.Black,
                                currentLayerX + neuronRadius / 2,
                                inputY + i * neuronSpacing + neuronRadius / 2,
                                nextLayerX + neuronRadius / 2,
                                inputY + j * neuronSpacing + neuronRadius / 2);
                        }
                    }
                }

                // Son gizli katman ile çýkýþ nöronlarý arasýndaki baðlantýlarý çiz
                hiddenX = inputX + hiddenLayers * layerSpacing;
                for (int j = 0; j < 3; j++) // Gizli katman nöron sayýsý 3
                {
                    for (int i = 0; i < outputNeurons; i++)
                    {
                        g.DrawLine(Pens.Black,
                            hiddenX + neuronRadius / 2,
                            inputY + j * neuronSpacing + neuronRadius / 2,
                            outputX + neuronRadius / 2,
                            outputY + i * neuronSpacing + neuronRadius / 2);
                    }
                }
            }

            // Çýkýþ nöronlarýný çiz
            for (int i = 0; i < outputNeurons; i++)
            {
                g.FillEllipse(Brushes.Red, outputX, outputY + i * neuronSpacing, neuronRadius, neuronRadius);
            }
        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
