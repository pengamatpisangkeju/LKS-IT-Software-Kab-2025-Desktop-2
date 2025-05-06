using System.Data;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;

namespace ec
{
    public partial class Form1 : Form
    {
        private DatabaseHelper dbHelper = DatabaseHelper.Instance;

        private DataTable currencyDt = new();

        private int selectedPeriodId = -1;
        private int selectedOriginCurrencyId = -1;
        private int selectedTargetCurrencyId = -1;

        public Form1()
        {
            InitializeComponent();
            LoadPeriodData();
            LoadCurrencyData();
        }

        private void LoadPeriodData()
        {
            try
            {
                string query = "SELECT id, name FROM Period";

                DataTable dt = dbHelper.ExecuteQuery(query);

                if (dt.Rows.Count > 0)
                {
                    BindComboBox(cbPeriod, dt, "name", "id", 0);
                    selectedPeriodId = GetSelectedValueMember(cbPeriod);
                }
                else
                {
                    MessageBox.Show("No period data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading period data: " + ex.Message);
            }
        }

        private void LoadCurrencyData()
        {
            try
            {
                string query = "SELECT id, name, abbreviation FROM Currency";

                currencyDt = dbHelper.ExecuteQuery(query);

                if (currencyDt.Rows.Count > 0)
                {
                    DataTable originDt = FilterCurrencyDt(currencyDt, 1);
                    DataTable targetDt = FilterCurrencyDt(currencyDt, 2);

                    BindComboBox(cbOrigin, originDt, "abbreviation", "id", 0);
                    lblOrigin.Text = GetSelectedDisplayMember(cbOrigin, "name");
                    selectedOriginCurrencyId = GetSelectedValueMember(cbOrigin);

                    BindComboBox(cbTarget, targetDt, "abbreviation", "id", 0);
                    lblTarget.Text = GetSelectedDisplayMember(cbTarget, "name");
                    selectedTargetCurrencyId = GetSelectedValueMember(cbTarget);
                }
                else
                {
                    MessageBox.Show("No currency data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading currency data: " + ex.Message);
            }
        }

        private void ConvertExchange()
        {
            if (string.IsNullOrWhiteSpace(tbOrigin.Text) || !decimal.TryParse(tbOrigin.Text, out decimal originAmount) || originAmount <= 0)
            {
                tbTarget.Text = string.Empty;
                return;
            }

            decimal originExchangeRate = 1;
            decimal targetExchangeRate = 1;

            bool isOriginUSD = GetSelectedDisplayMember(cbOrigin, "abbreviation") == "USD";
            bool isTargetUSD = GetSelectedDisplayMember(cbTarget, "abbreviation") == "USD";

            if (!isOriginUSD)
            {
                originExchangeRate = GetExchangeRate(selectedOriginCurrencyId, selectedPeriodId);
            }

            if (!isTargetUSD)
            {
                targetExchangeRate = GetExchangeRate(selectedTargetCurrencyId, selectedPeriodId);
            }

            decimal crossExchangeRate = Math.Round(originExchangeRate / targetExchangeRate, 10);
            decimal exchangeResult = Math.Round(originAmount / crossExchangeRate, 3);

            tbTarget.Text = exchangeResult.ToString();
        }

        private decimal GetExchangeRate(int currencyId, int periodId)
        {
            try
            {
                string query = "SELECT rate FROM USDExchangeRate " +
                    "WHERE currency_id = @currencyId AND period_id = @periodId";

                SqlParameter[] sp = [
                    new("@currencyId", currencyId),
                    new("@periodId", periodId),
                ];

                DataTable dt = dbHelper.ExecuteQuery(query, sp);

                if (dt.Rows.Count > 0)
                {
                    return (decimal)dt.Rows[0]["rate"];
                }
                else
                {
                    MessageBox.Show("No rate data");
                    return 0m;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting exchange rate: " + ex.Message);
                return 0m;
            }
        }

        private DataTable FilterCurrencyDt(DataTable dt, int excludedId)
        {
            DataView dv = new(dt)
            {
                RowFilter = "id <> " + excludedId
            };
            return dv.ToTable();
        }

        private string GetSelectedDisplayMember(ComboBox cb, string memberName)
        {
            if (cb.SelectedItem is DataRowView drv && drv[memberName] is string member)
            {
                return member;
            }
            return string.Empty;
        }

        private int GetSelectedValueMember(ComboBox cb)
        {
            if (cb.SelectedValue is int id && id >= 1)
            {
                return id;
            }
            return -1;
        }

        private void BindComboBox(ComboBox cb, DataTable dt, string displayMember, string valueMember, int selectedIndex)
        {
            cb.DataSource = dt;
            cb.DisplayMember = displayMember;
            cb.ValueMember = valueMember;
            cb.SelectedIndex = selectedIndex;
        }

        private void cbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPeriodId = GetSelectedValueMember(cbPeriod);
            ConvertExchange();
        }

        private void cbOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedOriginCurrencyId = GetSelectedValueMember(cbOrigin);
            lblOrigin.Text = GetSelectedDisplayMember(cbOrigin, "name");

            DataTable updatedTargetDt = FilterCurrencyDt(currencyDt, selectedOriginCurrencyId);
            cbTarget.SelectedIndexChanged -= cbTarget_SelectedIndexChanged;
            BindComboBox(cbTarget, updatedTargetDt, "abbreviation", "id", 0);
            cbTarget.SelectedValue = selectedTargetCurrencyId;
            cbTarget.SelectedIndexChanged += cbTarget_SelectedIndexChanged;

            ConvertExchange();
        }

        private void cbTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTargetCurrencyId = GetSelectedValueMember(cbTarget);
            lblTarget.Text = GetSelectedDisplayMember(cbTarget, "name");

            DataTable updatedOriginDt = FilterCurrencyDt(currencyDt, selectedTargetCurrencyId);
            cbOrigin.SelectedIndexChanged -= cbOrigin_SelectedIndexChanged;
            BindComboBox(cbOrigin, updatedOriginDt, "abbreviation", "id", 0);
            cbOrigin.SelectedValue = selectedOriginCurrencyId;
            cbOrigin.SelectedIndexChanged += cbOrigin_SelectedIndexChanged;

            ConvertExchange();
        }

        private void tbOrigin_TextChanged(object sender, EventArgs e)
        {
            ConvertExchange();
        }

        private void tbOrigin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == '.'))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '.' && (string.IsNullOrEmpty(tbOrigin.Text) || tbOrigin.Text.Contains('.')))
            {
                e.Handled = true;
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbOrigin.Items.Count == 0 || cbTarget.Items.Count == 0)
            {
                return;
            }

            tbOrigin.Text = tbTarget.Text;

            DataTable originDt = FilterCurrencyDt(currencyDt, selectedTargetCurrencyId);
            DataTable targetDt = FilterCurrencyDt(currencyDt, selectedOriginCurrencyId);

            int tempOriginId = GetSelectedValueMember(cbOrigin);
            int tempTargetid = GetSelectedValueMember(cbTarget);

            BindComboBox(cbOrigin, targetDt, "abbreviation", "id", 0);
            BindComboBox(cbTarget, originDt, "abbreviation", "id", 0);

            cbOrigin.SelectedValue = tempTargetid;
            cbTarget.SelectedValue = tempOriginId;
        }
    }
}
