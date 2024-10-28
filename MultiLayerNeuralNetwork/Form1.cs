using System.Data;
using System.Numerics;

namespace MultiLayerNeuralNetwork
{
    public partial class Form1 : Form
    {
        private DataTable dataTable;
        private double[,] weightsInputHidden;  // Giri�ten gizli katmana a��rl�klar
        private double[,] weightsHiddenOutput; // Gizli katmandan ��k�� katman�na a��rl�klar
        private double[] hiddenLayer;          // Gizli katmandaki n�ronlar
        private double[] outputLayer;          // ��k�� katmandaki n�ronlar
        private bool networkInitialized = false;

        private double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));
        private double SigmoidDerivative(double x) => x * (1.0 - x);

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Sabit boyut
            this.MaximizeBox = false; // Maksimuma alma d��mesini devre d��� b�rak
            this.MinimizeBox = true;  // �sterseniz minimize �zelli�i aktif kalabilir
            this.MinimumSize = this.Size; // Minimum boyutu �u anki boyuta ayarla
            this.MaximumSize = this.Size; // Maksimum boyutu �u anki boyuta ayarla
            this.StartPosition = FormStartPosition.CenterScreen;
            label6.TextAlign = ContentAlignment.MiddleCenter;

            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
        }

        private void InitializeDataTable(int inputNeurons, int outputNeurons)
        {
            // DataTable olu�tur
            dataTable = new DataTable();

            // �lk �nce giri�im (X) s�tunlar�n� ekle
            for (int j = 0; j < inputNeurons; j++)
            {
                dataTable.Columns.Add("X" + j);
            }

            // Ard�ndan beklenen (Beklenen) s�tunlar�n� ekle
            for (int k = 0; k < outputNeurons; k++)
            {
                dataTable.Columns.Add("Beklenen" + k);
            }
        }

        // Geri yay�l�m algoritmas�n� uygulayan fonksiyon
        private void TrainNetwork(int epochs, double learningRate)
        {
            int inputNeurons = int.Parse(txtInputNeurons.Text);
            int outputNeurons = int.Parse(txtOutputNeurons.Text);
            int hiddenNeurons = 3; // Gizli katman n�ron say�s�
            double totalError = 0;

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                totalError = 0;

                foreach (DataRow row in dataTable.Rows)
                {
                    // Giri�leri ve beklenen ��kt�lar� tablo verisinden al
                    double[] inputs = new double[inputNeurons];
                    double[] expectedOutputs = new double[outputNeurons];

                    for (int i = 0; i < inputNeurons; i++)
                        inputs[i] = Convert.ToDouble(row[i]);

                    for (int i = 0; i < outputNeurons; i++)
                        expectedOutputs[i] = Convert.ToDouble(row[inputNeurons + i]);

                    // �leri yay�l�m
                    for (int i = 0; i < hiddenNeurons; i++)
                    {
                        hiddenLayer[i] = 0; // Bu sat�rda null hatas� al�nmamas� i�in diziyi ba�latt�k
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

                    // Hata hesaplama ve toplam hatay� g�ncelleme
                    double[] outputErrors = new double[outputNeurons];
                    for (int i = 0; i < outputNeurons; i++)
                    {
                        outputErrors[i] = expectedOutputs[i] - outputLayer[i];
                        totalError += Math.Pow(outputErrors[i], 2);
                    }

                    // Geri yay�l�m (backpropagation)
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

                // Her epoch sonras� toplam hatay� ekrana yazd�r
                label6.Text = $"Epoch {epoch + 1}/{epochs}";  // label6'ya epoch say�s� yazd�r�l�yor
                label7.Text = $"Toplam Hata: {totalError:F4}";   // Virg�lden sonra 4 basamak yazd�r�l�yor


                Application.DoEvents(); // Aray�z�n g�ncellenmesini sa�lar
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
            }
            networkInitialized = true;
            // Kullan�c�dan de�erleri al
            int inputNeurons = int.Parse(txtInputNeurons.Text);
            int hiddenLayers = int.Parse(txtHiddenLayers.Text);
            int outputNeurons = int.Parse(txtOutputNeurons.Text);
            double learningRate = double.Parse(txtLearningRate.Text);

            // PictureBox boyutlar�n� yeniden ayarla
            int neuronRadius = 20;
            int layerSpacing = 150;
            int neuronSpacing = 50;

            // Giri� ve ��k�� katmanlar� aras�ndaki toplam geni�li�i hesapla
            int totalWidth = (hiddenLayers + 2) * layerSpacing;

            // En fazla n�ron say�s�na g�re y�kseklik hesapla (giri�, gizli veya ��k��)
            int maxNeurons = Math.Max(inputNeurons, Math.Max(3, outputNeurons));
            int totalHeight = (maxNeurons + 1) * neuronSpacing;

            // PictureBox boyutlar�n� hesapla ve ayarla
            pictureBox1.Width = totalWidth + neuronRadius * 2;
            pictureBox1.Height = totalHeight + neuronRadius * 2;

            // Sinir a��n� �izmek i�in PictureBox'� yeniden �izdir
            pictureBox1.Invalidate();
            InitializeDataTable(inputNeurons, outputNeurons);
            dataGridView1.DataSource = dataTable;

            // ---- YAPAY S�N�R A�I OLU�TURMA VE E��T�M� ----

            // Sinir a��n� olu�turma
            int hiddenNeurons = 3; // Gizli katman n�ron say�s� sabit olarak belirlenmi�tir.
            weightsInputHidden = new double[inputNeurons, hiddenNeurons];
            weightsHiddenOutput = new double[hiddenNeurons, outputNeurons];
            hiddenLayer = new double[hiddenNeurons];
            outputLayer = new double[outputNeurons];

            // Rastgele a��rl�klar� ba�lat
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

        // A��n e�itilmesini sa�layan button2_Click fonksiyonu
        private void button2_Click(object sender, EventArgs e)
        {
            int epochs = 100;
            double learningRate = double.Parse(txtLearningRate.Text); // ��renme oran�
            TrainNetwork(epochs, learningRate);            // A�� e�it
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!networkInitialized)
                return; // E�er a� ba�lat�lmad�ysa, �izim yapma ve fonksiyondan ��k.

            // Graphics nesnesini al ve sinir a��n� �iz
            Graphics g = e.Graphics;

            int inputNeurons = 0;
            int hiddenLayers = 0;
            int outputNeurons = 0;

            if (!int.TryParse(txtInputNeurons.Text, out inputNeurons))
            {
                MessageBox.Show("Ge�erli bir giri� n�ron say�s� girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtHiddenLayers.Text, out hiddenLayers))
            {
                MessageBox.Show("Ge�erli bir gizli katman say�s� girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtOutputNeurons.Text, out outputNeurons))
            {
                MessageBox.Show("Ge�erli bir ��k�� n�ron say�s� girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Giri� n�ronlar�n� �iz
            for (int i = 0; i < inputNeurons; i++)
            {
                g.FillEllipse(Brushes.Green, inputX, inputY + i * neuronSpacing, neuronRadius, neuronRadius);
            }

            // E�er gizli katman yoksa (hiddenLayers == 0), do�rudan giri�ten ��k��a ba�la
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
                // Gizli katmanlar varsa bunlar� �iz
                int hiddenX = inputX + layerSpacing;
                for (int layer = 0; layer < hiddenLayers; layer++)
                {
                    for (int i = 0; i < 3; i++)  // Gizli katman n�ron say�s�n� 3 olarak sabit tutuyoruz
                    {
                        int hiddenY = inputY + i * neuronSpacing;
                        g.FillEllipse(Brushes.Blue, hiddenX, hiddenY, neuronRadius, neuronRadius);
                    }
                    hiddenX += layerSpacing; // Her katman� bir sonraki X konumuna kayd�r
                }

                // ��k�� n�ronlar�n� �iz
                for (int i = 0; i < outputNeurons; i++)
                {
                    g.FillEllipse(Brushes.Red, outputX, outputY + i * neuronSpacing, neuronRadius, neuronRadius);
                }

                // Giri� n�ronlar� ile ilk gizli katman n�ronlar� aras�ndaki ba�lant�lar� �iz
                hiddenX = inputX + layerSpacing;
                for (int i = 0; i < inputNeurons; i++)
                {
                    for (int j = 0; j < 3; j++) // Gizli katman n�ron say�s� 3
                    {
                        g.DrawLine(Pens.Black,
                            inputX + neuronRadius / 2,
                            inputY + i * neuronSpacing + neuronRadius / 2,
                            hiddenX + neuronRadius / 2,
                            inputY + j * neuronSpacing + neuronRadius / 2);
                    }
                }

                // Gizli katmanlar aras�ndaki ba�lant�lar� �iz
                for (int layer = 0; layer < hiddenLayers - 1; layer++)
                {
                    int currentLayerX = inputX + (layer + 1) * layerSpacing;
                    int nextLayerX = inputX + (layer + 2) * layerSpacing;

                    for (int i = 0; i < 3; i++)  // �u anki gizli katman n�ron say�s� 3
                    {
                        for (int j = 0; j < 3; j++)  // Bir sonraki gizli katman n�ron say�s� 3
                        {
                            g.DrawLine(Pens.Black,
                                currentLayerX + neuronRadius / 2,
                                inputY + i * neuronSpacing + neuronRadius / 2,
                                nextLayerX + neuronRadius / 2,
                                inputY + j * neuronSpacing + neuronRadius / 2);
                        }
                    }
                }

                // Son gizli katman ile ��k�� n�ronlar� aras�ndaki ba�lant�lar� �iz
                hiddenX = inputX + hiddenLayers * layerSpacing;
                for (int j = 0; j < 3; j++) // Gizli katman n�ron say�s� 3
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

            // ��k�� n�ronlar�n� �iz
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
