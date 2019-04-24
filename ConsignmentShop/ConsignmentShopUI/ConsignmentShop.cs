using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        BindingSource itemsBinding = new BindingSource();

        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();

            itemsBinding.DataSource = store.Items;
            itemsListbox.DataSource = itemsBinding;
            //display 1 field. need to add new property to items to display title + price
            itemsListbox.DisplayMember = "Display"; 
            itemsListbox.ValueMember = "Display";
        }

        private void SetupData()
        {
            //add vendors
            store.Vendors.Add(new Vendor
            {
                FirstName = "Bill",
                LastName = "Smith",
            });

            store.Vendors.Add(new Vendor
            {
                FirstName = "Sue",
                LastName = "Jones",
            });

            //add items
            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "A book about whale",
                Price = 4.50M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "A Tale of Two Cities",
                Description = "A book about a revolution",
                Price = 3.80M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Harry Potter 1",
                Description = "A book about wizards",
                Price = 5.20M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Jane Eyre",
                Description = "A book about a girl",
                Price = 1.50M,
                Owner = store.Vendors[0]
            });

            //set store
            store.Name = "Seconds are Better";
        }

    }
}
