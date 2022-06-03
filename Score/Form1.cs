namespace ScoreSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // �ƥ��b TextBox.Tag ����J��ئW��
            txtChinese.Tag = "���";
            txtEnglish.Tag = "�^��";
            txtMath.Tag = "�ƾ�";

            // WinForm�G���ո�ƪ�l�� - ���`��
            txtChinese.Text = "100";
            txtEnglish.Text = "60";
            txtMath.Text = "0";

            // WinForm�G���ո�ƪ�l�� - ���`��
            //txtChinese.Text = "abc";
            //txtEnglish.Text = "";
            //txtMath.Text = "110";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            // ���а��涷���M����ظ�T
            gbAnalyze.Controls.Clear();

            ScoreService service = new ScoreService(GetUserInput());

            var result = service.RuleValidProcess();
            if (result.isValid == false)
            {
                SubjectDisplay(result.errorMessage);
                return;
            }

            var subjectInfo = service.SubjectProcess();
            SubjectDisplay(subjectInfo.minSubject, subjectInfo.maxSubject);
        }

        private Dictionary<string, string> GetUserInput()
        {
            return plSubject.Controls
                .OfType<TextBox>()
                .ToDictionary(k => k.Tag.ToString(), v => v.Text);
        }

        private void SubjectDisplay(params string[] subjectInfo)
        {
            string text = string.Join(Environment.NewLine , subjectInfo);

            Label lbl = new Label
            {
                AutoSize = true,
                Text = text,
                Location = new Point(10, 30)
            };

            gbAnalyze.Controls.Add(lbl);
        }
    }
}