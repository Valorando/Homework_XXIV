/*Разработайте приложение, позволяющее динамически обновлять список товаров в DataGridView через привязку данных и интерфейс INotifyPropertyChanged.

Создайте класс Product, содержащий свойства Name, Category и Price, реализующий интерфейс INotifyPropertyChanged.
Создайте коллекцию объектов Product и привяжите ее к DataGridView.
Реализуйте возможность добавления, удаления и редактирования товаров через пользовательский интерфейс, обеспечив динамическое обновление DataGridView при изменении коллекции товаров.
Убедитесь, что при изменении свойств объектов Product через код DataGridView также обновляется автоматически.
Расширение: Добавьте возможность фильтрации и сортировки списка товаров через пользовательский интерфейс.*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_22_09
{
    public partial class Form1 : Form
    {
        private Product selectedProduct;

        private BindingList<Product> product_list;
        public Form1()
        {
            InitializeComponent();

            product_list = new BindingList<Product>();


            dataGridView1.DataSource = product_list;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.CellClick += dataGridView1_CellClick;




            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            button4.Visible = false;
            button5.Visible = false;

        }

        public class Product : INotifyPropertyChanged
        {
            private string name;
            private string category;
            private string price;

            public event PropertyChangedEventHandler PropertyChanged;

            public string Name
            {
                get => name; 
                set
                {
                    if (value != name)
                    {
                        name = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string Category
            {
                get => category;
                
                set
                {
                    if (value != category)
                    {
                        category = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string Price
            {
                get => price;
                set
                {
                    if (value != price)
                    {
                        price = value;
                        OnPropertyChanged();
                    }
                }
            }

            public Product(string name_, string category_, string price_)
            {
                Name = name_;
                Category = category_;
                Price = price_;
            }

            private void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (product_list.Count > 0)
            {
                product_list.Last().Name = textBox1.Text;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (product_list.Count > 0)
            {
                product_list.Last().Category = textBox2.Text;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (product_list.Count > 0)
            {
                product_list.Last().Price = textBox3.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            button4.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;

            Product product = new Product("пусто", "пусто", "пусто");

            product_list.Add(product);

            textBox1.DataBindings.Add("Text", product_list.Last(), "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox2.DataBindings.Add("Text", product_list.Last(), "Category", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox3.DataBindings.Add("Text", product_list.Last(), "Price", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            button4.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.RowIndex < product_list.Count)
                {
                    
                    textBox4.DataBindings.Clear();
                    textBox4.DataBindings.Add("Text", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex], "Value", true, DataSourceUpdateMode.OnPropertyChanged);

                    selectedProduct = product_list[e.RowIndex];
                }
            }
        }



        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
                
                dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = textBox4.Text;

                if (selectedProduct != null)
                {
                    if (columnIndex == 0)
                        selectedProduct.Name = textBox4.Text;
                    else if (columnIndex == 1)
                        selectedProduct.Category = textBox4.Text;
                    else if (columnIndex == 2)
                        selectedProduct.Price = textBox4.Text;
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Visible = true;
            button5.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Visible = false;
            button5.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
        }
    }
}
