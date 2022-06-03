namespace ScoreSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 事先在 TextBox.Tag 內塞入科目名稱
            txtChinese.Tag = "國文";
            txtEnglish.Tag = "英文";
            txtMath.Tag = "數學";

            // WinForm：測試資料初始值 - 正常值
            txtChinese.Text = "100";
            txtEnglish.Text = "60";
            txtMath.Text = "0";

            // WinForm：測試資料初始值 - 異常值
            //txtChinese.Text = "abc";
            //txtEnglish.Text = "";
            //txtMath.Text = "110";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            // 重覆執行須先清除科目資訊
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