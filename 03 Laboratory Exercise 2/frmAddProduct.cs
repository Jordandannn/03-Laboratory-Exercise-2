using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03_Laboratory_Exercise_2
{
    public partial class frmAddProduct : Form
    {
        public BindingSource showProductList;
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;
        public frmAddProduct()
        {
            showProductList = new BindingSource();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
            {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"
            };
            foreach (string category in ListOfProductCategory)
            {
                cbCategory.Items.Add(category);
            }
        }
        public string Product_Name(string Name)
        {
            if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"))
            {
                throw new StringFormatException("Error in Product Name. ");
            }

            return Name;
        }
        public int Quantity(string Qty)
        {
            if (!Regex.IsMatch(Qty, @"^[0-9]"))
            {
                throw new NumberFormatException("Error in Quantity.");
            }
            return Convert.ToInt32(Qty);
        }
        public double SellingPrice(string Price)
        {
            if (!Regex.IsMatch(Price.ToString(), @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException("Error in Selling Price. ");
            }
            return Convert.ToDouble(Price);
        }

        private void btnAddProduc_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                gridViewProductList.DataSource = showProductList;
                

                txtProductName.Clear();
                txtQuantity.Clear();
                txtSellPrice.Clear();
                richTxtDescription.Clear();
            }

            catch (NumberFormatException nfe)
            {
                MessageBox.Show(nfe.Message);
            }

            catch (StringFormatException sfe)
            {
                MessageBox.Show(sfe.Message);
            }

            catch (CurrencyFormatException cfe)
            {
                MessageBox.Show(cfe.Message);
            }

        }
        public class NumberFormatException : Exception
        {
            public NumberFormatException(string exception) : base(exception) { }

        }

        public class StringFormatException : Exception
        {
            public StringFormatException(string exception) : base(exception) { }

        }

        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string exception) : base(exception) { }
        }
    }
    
}
